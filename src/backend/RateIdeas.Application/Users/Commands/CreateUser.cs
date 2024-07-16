namespace RateIdeas.Application.Users.Commands;

public record CreateUserCommand : IRequest<UserResultDto>
{
    public CreateUserCommand(CreateUserCommand command)
    {
        FormFile = command.FormFile;
        Email = command.Email;
        LastName = command.LastName;
        Password = command.Password;
        FirstName = command.FirstName;
        DateOfBirth = command.DateOfBirth;
        UserName = command.UserName;
    }

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public DateTimeOffset DateOfBirth { get; set; }
    public IFormFile? FormFile { get; set; }
}

public class CreateUserCommandHandler(IMapper mapper,
    IRepository<User> repository,
    IMediator mediator) : IRequestHandler<CreateUserCommand, UserResultDto>
{
    public async Task<UserResultDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Email.ToLower().Equals(request.Email.ToLower()));
        if (entity is not null)
            throw new AlreadyExistException($"User is already exist with email: {request.Email}");

        entity = await repository.SelectAsync(entity => entity.UserName.ToLower().Equals(request.UserName.ToLower()));
        if (entity is not null)
            throw new AlreadyExistException($"User is already exist with UserName: {request.UserName}");

        entity = mapper.Map<User>(request);

        if (request.FormFile is not null)
        {
            var uploadedImage = await mediator.Send(new UploadAssetCommand(request.FormFile), cancellationToken);
            var createdImage = new Asset
            {
                FileName = uploadedImage.FileName,
                FilePath = uploadedImage.FilePath,
            };

            entity.ImageId = uploadedImage.Id;
            entity.Image = createdImage;
        }

        entity.DateOfBirth = entity.DateOfBirth.UtcDateTime;
        entity.PasswordHash = PasswordHasher.Encrypt(request.Password);

        await repository.InsertAsync(entity);
        await repository.SaveAsync();

        return mapper.Map<UserResultDto>(entity);
    }
}