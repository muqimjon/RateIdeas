namespace RateIdeas.Application.Ideas.Commands;

public record CreateIdeaCommand : IRequest<IdeaResultDto>
{
    public CreateIdeaCommand(CreateIdeaCommand command)
    {
        Title = command.Title;
        FormFile = command.FormFile;
        CategoryId = command.CategoryId;
        Description = command.Description;
    }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public long CategoryId { get; set; }
    public IFormFile? FormFile { get; set; } = default!;
}

public class CreateIdeaCommandHandler(IMapper mapper,
    IRepository<Idea> repository,
    IRepository<User> userRepository,
    IRepository<Category> categoryRepository,
    IMediator mediator) : IRequestHandler<CreateIdeaCommand, IdeaResultDto>
{
    public async Task<IdeaResultDto> Handle(CreateIdeaCommand request,
        CancellationToken cancellationToken)
    {
        var entity = mapper.Map<Idea>(request);

        if (HttpContextHelper.ResponseHeaders is null || (entity.User = await userRepository
            .SelectAsync(entity => entity.Id.Equals(HttpContextHelper.GetUserId ?? 0))) is null)
            throw new AuthenticationException("Authentication has not been completed");

        entity.Category = await categoryRepository
            .SelectAsync(i => i.Id.Equals(request.CategoryId))
            ?? throw new NotFoundException(
                $"{nameof(Category)} is not found by ID: {request.CategoryId}");

        if (request.FormFile is not null)
        {
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

        entity.UserId = (long)HttpContextHelper.GetUserId!;
        await repository.InsertAsync(entity);
        await repository.SaveAsync();

        return mapper.Map<IdeaResultDto>(entity);
    }
}