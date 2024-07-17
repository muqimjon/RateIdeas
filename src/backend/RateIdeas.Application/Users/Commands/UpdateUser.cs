namespace RateIdeas.Application.Users.Commands;

public record UpdateUserCommand : IRequest<UserResultDto>
{
    public UpdateUserCommand(UpdateUserCommand command)
    {
        Id = command.Id;
        FormFile = command.FormFile;
        Email = command.Email;
        LastName = command.LastName;
        Password = command.Password;
        FirstName = command.FirstName;
        DateOfBirth = command.DateOfBirth;
        UserName = command.UserName;
    }

    public long Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public DateTimeOffset DateOfBirth { get; set; }
    public IFormFile? FormFile { get; set; } = default!;
}

public class UpdateUserCommandHandler(IMapper mapper,
    IRepository<User> repository,
    IMediator mediator) :
    IRequestHandler<UpdateUserCommand, UserResultDto>
{
    public async Task<UserResultDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity
            => entity.Email.ToLower().Equals(request.Email.ToLower()));
        if (entity is not null)
            throw new AlreadyExistException($"{typeof(User)} is already exist with Email: {request.Email}");

        entity = await repository.SelectAsync(entity
            => entity.UserName.ToLower().Equals(request.UserName.ToLower()));
        if (entity is not null)
            throw new AlreadyExistException($"{typeof(User)} is already exist with UserName: {request.UserName}");

        entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new NotFoundException($"{typeof(User)} is not found by ID: {request.Id}");

        mapper.Map(request, entity);

        if (request.FormFile is not null)
        {
            if (entity.Image is not null)
                await mediator.Send(new DeleteAssetCommand(request.Id), cancellationToken);

            var uploadedImage = await mediator.Send(new UploadAssetCommand(request.FormFile), cancellationToken);
            var createdImage = new Asset
            {
                FileName = uploadedImage.FileName,
                FilePath = uploadedImage.FilePath,
            };

            entity.ImageId = uploadedImage.Id;
            entity.Image = createdImage;
        }

        entity.DateOfBirth = TimeHelper.ToLocalize(entity.DateOfBirth);

        repository.Update(entity);
        await repository.SaveAsync();

        return mapper.Map<UserResultDto>(entity);
    }
}