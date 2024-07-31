using Microsoft.Extensions.Caching.Memory;
using RateIdeas.Application.Auths.Commands.LogIn;
using RateIdeas.Application.Auths.DTOs;

namespace RateIdeas.Application.Auths.Commands.MailVerification;

public record VerifyEmailCommand : IRequest<UserResponseDto>
{
    public VerifyEmailCommand(VerifyEmailCommand command)
    {
        Email = command.Email;
        VerificationCode = command.VerificationCode;
    }

    public string Email { get; set; } = string.Empty;
    public string VerificationCode { get; set; } = string.Empty;
}

public class VerifyEmailCommandHandler(IMapper mapper,
    IRepository<User> repository,
    IMemoryCache memory,
    IMediator mediator) : IRequestHandler<VerifyEmailCommand, UserResponseDto>
{
    private const int MaxAttempts = 3;
    private readonly TimeSpan BlockDuration = TimeSpan.FromMinutes(5);

    public async Task<UserResponseDto> Handle(
        VerifyEmailCommand request,
        CancellationToken cancellationToken)
    {
        if (memory.TryGetValue(request.Email, out UserRegistrationDto? data))
        {
            ArgumentNullException.ThrowIfNull(data);

            if (data.IsBlocked)
                throw new TooManyAttemptsException(
                    "Too many attempts. Please try again later");

            memory.Remove(data.Email);

            if (data.AttemptCount >= MaxAttempts)
            {
                data.IsBlocked = true;
                data.LifeTime = DateTimeOffset.UtcNow.Add(BlockDuration);
                memory.Set(data.Email, data, data.LifeTime);

                throw new TooManyAttemptsException(
                    "Too many attempts. Please try again later");
            }

            if (!data.VerificationCode.Equals(request.VerificationCode.ToString()))
            {
                data.AttemptCount++;
                memory.Set(data.Email, data, data.LifeTime);

                throw new CustomException(
                    "Incorrect verification code", StatusCodes.Status400BadRequest);
            }

            var entity = await repository.SelectAsync(entity
                    => entity.Email.ToLower().Equals(request.Email.ToLower()));

            if (entity is not null)
                throw new AlreadyExistException(
                    $"{nameof(User)} already exists with Email: {request.Email}");

            entity = await repository.SelectAsync(entity
                => entity.UserName.ToLower().Equals(data.UserName.ToLower()));

            if (entity is not null)
                throw new AlreadyExistException(
                    $"{nameof(User)} already exists with UserName: {data.UserName}");

            entity = mapper.Map<User>(data);

            if (!repository.SelectAll().Any())
                entity.Role = Roles.SuperAdmin;

            if (data.FormFile is not null)
            {
                var uploadedImage = await mediator.Send(
                    new UploadAssetCommand(data.FormFile), cancellationToken);

                var createdImage = new Asset
                {
                    FileName = uploadedImage.FileName,
                    FilePath = uploadedImage.FilePath,
                };

                entity.ImageId = uploadedImage.Id;
                entity.Image = createdImage;
            }

            entity.DateOfBirth = entity.DateOfBirth.UtcDateTime;
            entity.PasswordHash = SecurityHelper.Encrypt(data.Password);

            await repository.InsertAsync(entity);
            await repository.SaveAsync();

            return await mediator.Send(new LogInCommand
            {
                EmailOrUserName = data.Email,
                Password = data.Password,
            }, cancellationToken);
        }

        throw new NotFoundException($"{nameof(User)} not found with Email: {request.Email}");
    }
}
