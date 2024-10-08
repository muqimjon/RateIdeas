﻿namespace RateIdeas.Application.Categories.Commands;

public record CreateCategoryCommand : IRequest<CategoryResultDto>
{
    public CreateCategoryCommand(CreateCategoryCommand command)
    {
        Name = command.Name;
        FormFile = command.FormFile;
        Description = command.Description;
    }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public IFormFile? FormFile { get; set; } = default!;
}

public class CreateCategoryCommandHandler(IMapper mapper,
    IRepository<Category> repository,
    IMediator mediator) : IRequestHandler<CreateCategoryCommand, CategoryResultDto>
{
    public async Task<CategoryResultDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<Category>(request);

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

        await repository.InsertAsync(entity);
        await repository.SaveAsync();

        return mapper.Map<CategoryResultDto>(entity);
    }
}