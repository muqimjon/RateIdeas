namespace RateIdeas.Application.Ideas.Commands;

public record UpdateIdeaCommand : IRequest<UserResultDto>
{
    public UpdateIdeaCommand(UpdateIdeaCommand command)
    {
        Id = command.Id;
        FormFile = command.FormFile;
        Title = command.Title;
        Description = command.Description;
        CategoryId = command.CategoryId;
        UserId = command.UserId;
    }

    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public long CategoryId { get; set; }
    public long UserId { get; set; }
    public IFormFile? FormFile { get; set; } = default!;
}

public class UpdateIdeaCommandHandler(IMapper mapper,
    IRepository<Idea> repository,
    IMediator mediator) :
    IRequestHandler<UpdateIdeaCommand, UserResultDto>
{
    public async Task<UserResultDto> Handle(UpdateIdeaCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new NotFoundException($"This Idea is not found by id: {request.Id} | Idea update");

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