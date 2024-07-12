using RateIdeas.Application.Assets.Commands;

namespace RateIdeas.Application.Users.Commands;

public record CreateUserCommand : IRequest<UserResultDto>
{
    public CreateUserCommand(CreateUserCommand command)
    {
        Image = command.Image;
        Email = command.Email;
        LastName = command.LastName;
        Password = command.Password;
        FirstName = command.FirstName;
        DateOfBirth = command.DateOfBirth;
    }

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public DateTimeOffset DateOfBirth { get; set; }
    public IFormFile Image { get; set; } = default!;
}

public class CreateUserCommandHandler(IMapper mapper,
    IRepository<User> repository,
    IMediator mediator) : IRequestHandler<CreateUserCommand, UserResultDto>
{
    public async Task<UserResultDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Email == request.Email);
        if (entity is not null)
            throw new AlreadyExistException($"User Already exist user command create with email: {request.Email} | create user");

        entity = mapper.Map<User>(request);

        if (request.Image is not null)
        {
            var uploadedImage = await mediator.Send(new UploadAssetCommand(request.Image), cancellationToken);
            var createdImage = new Asset
            {
                FileName = uploadedImage.FileName,
                FilePath = uploadedImage.FilePath,
            };

            entity.ImageId = uploadedImage.Id;
            entity.Image = createdImage;
        }

        entity.DateOfBirth = TimeHelper.ToLocalize(request.DateOfBirth);
        entity.PasswordHash = PasswordHasher.Encrypt(request.Password);
        entity.CreatedAt = TimeHelper.GetDateTime();

        await repository.InsertAsync(entity);
        await repository.SaveAsync();

        return mapper.Map<UserResultDto>(entity);
    }
}