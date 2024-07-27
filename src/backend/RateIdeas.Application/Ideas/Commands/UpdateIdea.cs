namespace RateIdeas.Application.Ideas.Commands;

public record UpdateIdeaCommand : IRequest<IdeaResultDto>
{
    public UpdateIdeaCommand(UpdateIdeaCommand command)
    {
        Id = command.Id;
        Title = command.Title;
        FormFile = command.FormFile;
        CategoryId = command.CategoryId;
        Description = command.Description;
    }

    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public long CategoryId { get; set; }
    public IFormFile? FormFile { get; set; } = default!;
}

public class UpdateIdeaCommandHandler(IMapper mapper,
    IRepository<Idea> repository,
    IRepository<User> userRepository,
    IRepository<Category> categoryRepository,
    IMediator mediator) : IRequestHandler<UpdateIdeaCommand, IdeaResultDto>
{
    public async Task<IdeaResultDto> Handle(UpdateIdeaCommand request,
        CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id.Equals(request.Id))
            ?? throw new NotFoundException($"{nameof(Idea)} is not found by ID: {request.Id}");

        entity.Category = await categoryRepository.SelectAsync(i => i.Id.Equals(request.CategoryId))
            ?? throw new NotFoundException($"{nameof(Category)} is not found by ID: {request.CategoryId}");

        if (HttpContextHelper.ResponseHeaders is null || (entity.User = await userRepository
            .SelectAsync(entity => entity.Id.Equals(HttpContextHelper.GetUserId ?? 0))) is null)
            throw new AuthenticationException("Authentication has not been completed");

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

        return mapper.Map<IdeaResultDto>(entity);
    }
}