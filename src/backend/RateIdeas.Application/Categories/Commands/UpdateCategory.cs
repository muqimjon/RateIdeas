namespace RateIdeas.Application.Categories.Commands;

public record UpdateCategoryCommand : IRequest<CategoryResultDto>
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
    IRequestHandler<UpdateCategoryCommand, CategoryResultDto>
{
    public async Task<CategoryResultDto> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new NotFoundException($"{nameof(Category)} is not found with ID={request.Id}");

        mapper.Map(request, entity);

        if (request.FormFile is not null)
        {
            await mediator.Send(new DeleteAssetCommand(entity.Id), cancellationToken);
            var uploadedImage = await mediator.Send(new UploadAssetCommand(request.FormFile), cancellationToken);
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

        return mapper.Map<CategoryResultDto>(entity);
    }
}