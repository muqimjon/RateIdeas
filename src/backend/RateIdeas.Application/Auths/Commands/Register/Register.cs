using Microsoft.Extensions.Caching.Memory;
using RateIdeas.Application.Auths.DTOs;

namespace RateIdeas.Application.Auths.Commands.Register;

public record RegisterCommand : IRequest<string>
{
    public RegisterCommand(RegisterCommand command)
    {
        Email = command.Email;
        FormFile = command.FormFile;
        LastName = command.LastName;
        Password = command.Password;
        UserName = command.UserName;
        FirstName = command.FirstName;
        DateOfBirth = command.DateOfBirth;
        ConfirmPassword = command.ConfirmPassword;
    }

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
    public DateTimeOffset DateOfBirth { get; set; }
    public IFormFile? FormFile { get; set; }
}

public class RegisterCommandHandler(IRepository<User> repository,
    IMemoryCache memory,
    IMediator mediator,
    IMapper mapper) : IRequestHandler<RegisterCommand, string>
{
    private readonly TimeSpan DurationTime = TimeSpan.FromMinutes(1);
    public async Task<string> Handle(RegisterCommand request,
        CancellationToken cancellationToken)
    {
        if (memory.TryGetValue(request.Email, out _))
            throw new AlreadyExistException(
                "Please confirm your email address before attempting to register again.");

        var entity = await repository.SelectAsync(entity
            => entity.Email.ToLower().Equals(request.Email.ToLower()));

        if (entity is not null)
            throw new AlreadyExistException(
                $"{nameof(User)} already exists with Email: {request.Email}");

        entity = await repository.SelectAsync(entity
            => entity.UserName.ToLower().Equals(request.UserName.ToLower()));

        if (entity is not null)
            throw new AlreadyExistException(
                $"{nameof(User)} already exists with Username: {request.UserName}");

        var user = mapper.Map<UserRegistrationDto>(request);
        user.VerificationCode = SecurityHelper.GenerateCode();
        user.LifeTime = DateTimeOffset.UtcNow.Add(DurationTime);

        await mediator.Send(new SendEmailCommand()
        {
            To = request.Email,
            Subject = "Please Confirm Your Email",
            Body = $"Your verification code is: {user.VerificationCode}\n" +
                   $"This code will expire in {DurationTime} minute(s). " +
                   $"Please use it within the given time to complete the registration process.",
        }, cancellationToken);

        memory.Set(user.Email, user, user.LifeTime);

        return $"A verification code has been sent to {request.Email}. " +
               $"Please check your email and use the code within " +
               $"{DurationTime} minute(s) to complete the registration process.";
    }

}
