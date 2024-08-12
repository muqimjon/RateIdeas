namespace RateIdeas.Application.Users.Commands;

public record UpdateUserByIdCommand : IRequest<UserResultDto>
{
    public UpdateUserByIdCommand(UpdateUserByIdCommand command)
    {
        Id = command.Id;
        Email = command.Email;
        UserName = command.UserName;
        FormFile = command.FormFile;
        LastName = command.LastName;
        Password = command.Password;
        FirstName = command.FirstName;
        DateOfBirth = command.DateOfBirth;
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

public class UpdateUserByIdCommandHandler(IMapper mapper,
    IRepository<User> repository,
    IMediator mediator) :
    IRequestHandler<UpdateUserByIdCommand, UserResultDto>
{
    public async Task<UserResultDto> Handle(
        UpdateUserByIdCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id.Equals(request.Id))
            ?? throw new NotFoundException($"{nameof(User)} is not found with ID: {request.Id}");

        mapper.Map(request, entity);

        if (request.FormFile is not null)
        {
            if (entity.Image is not null)
                await mediator.Send(new DeleteAssetCommand(entity.ImageId), cancellationToken);

            var uploadedImage = await mediator.Send(
                new UploadAssetCommand(request.FormFile), cancellationToken);

            var createdImage = new Asset
            {
                FileName = uploadedImage.FileName,
                FilePath = uploadedImage.FilePath,
            };

            entity.ImageId = uploadedImage.Id;
            entity.Image = createdImage;
        }

        repository.Update(entity);
        await repository.SaveAsync();

        return mapper.Map<UserResultDto>(entity);
    }
}