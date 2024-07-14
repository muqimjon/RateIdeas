using RateIdeas.Application.Assets.Commands;

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
        var entity = await repository.SelectAsync(entity => entity.Email.Equals(request.Email, StringComparison.OrdinalIgnoreCase));
        if (entity is not null)
            throw new AlreadyExistException($"Bunday email bilan avval ro'yxatdan o'tilgan{request.Email}");

        entity = await repository.SelectAsync(entity => entity.UserName.Equals(request.UserName, StringComparison.OrdinalIgnoreCase));
        if (entity is not null)
            throw new AlreadyExistException($"Bunday UserName bilan avval ro'yxatdan o'tilgan: {request.UserName}");

        entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new NotFoundException($"This User is not found by id: {request.Id} | User update");

        mapper.Map(request, entity);

        if (request.FormFile is not null)
        {
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