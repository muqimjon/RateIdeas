namespace RateIdeas.Application.Categories.Commands;

public record UpdateCategoryCommand : IRequest<UserResultDto>
{
    public UpdateCategoryCommand(UpdateCategoryCommand command)
    {
        Id = command.Id;
        Name = command.Name;
        FormFile = command.FormFile;
        Description = command.Description;
    }

    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public IFormFile? FormFile { get; set; } = default!;
}

public class UpdateCategoryCommandHandler(IMapper mapper,
    IRepository<Category> repository,
    IMediator mediator) :
    IRequestHandler<UpdateCategoryCommand, UserResultDto>
{
    public async Task<UserResultDto> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new NotFoundException($"This Category is not found by id: {request.Id} | Category update");

        mapper.Map(request, entity);

        if (request.FormFile is not null)
        {
            _ = await mediator.Send(new DeleteAssetCommand(entity.Id), cancellationToken);
            var uploadedImage = await mediator.Send(new UploadAssetCommand(request.FormFile), cancellationToken);
            var createdImage = new Asset
            {
                FileName = uploadedImage.FileName,
                FilePath = uploadedImage.FilePath,
            };

            entity.ImageId = uploadedImage.Id;
            entity.Image = createdImage;
        }

        await repository.InsertAsync(entity);
        await repository.SaveAsync();

        return mapper.Map<UserResultDto>(entity);
    }
}