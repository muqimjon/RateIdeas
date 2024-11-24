namespace RateIdeas.Application.Users.Commands.UpdateUser;

public record UpdateUserCommand : IRequest<UserResultDto>
{
    public UpdateUserCommand(UpdateUserCommand command)
    {
        Email = command.Email;
        UserName = command.UserName;
        FormFile = command.FormFile;
        LastName = command.LastName;
        FirstName = command.FirstName;
        DateOfBirth = command.DateOfBirth;
    }

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public DateTimeOffset DateOfBirth { get; set; }
    public IFormFile? FormFile { get; set; } = default!;
}

public class UpdateUserCommandHandler(IMapper mapper,
    IRepository<User> repository,
    IMediator mediator) :
    IRequestHandler<UpdateUserCommand, UserResultDto>
{
    public async Task<UserResultDto> Handle(
        UpdateUserCommand request, CancellationToken cancellationToken)
    {
        if (HttpContextHelper.ResponseHeaders is null || HttpContextHelper.GetUserId is null)
            throw new AuthenticationException("Authentication has not been completed");

        var entity = await repository.SelectAsync(entity
            => entity.Id.Equals(HttpContextHelper.GetUserId))
            ?? throw new NotFoundException(
                $"{nameof(User)} is not found with ID: {HttpContextHelper.GetUserId}");

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