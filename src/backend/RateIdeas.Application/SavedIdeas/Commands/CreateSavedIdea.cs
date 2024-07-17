namespace RateIdeas.Application.SavedIdeas.Commands;

public record CreateSavedIdeaCommand : IRequest<SavedIdeaResultDto>
{
    public CreateSavedIdeaCommand(CreateSavedIdeaCommand command)
    {
        IdeaId = command.IdeaId;
        UserId = command.UserId;
        Content = command.Content;
    }
    public string Content { get; set; } = string.Empty;
    public long IdeaId { get; set; }
    public long UserId { get; set; }
}

public class CreateSavedIdeaCommandHandler(IMapper mapper,
    IRepository<Idea> ideaRepository,
    IRepository<User> userRepository,
    IRepository<SavedIdea> repository) : IRequestHandler<CreateSavedIdeaCommand, SavedIdeaResultDto>
{
    public async Task<SavedIdeaResultDto> Handle(CreateSavedIdeaCommand request,
        CancellationToken cancellationToken)
    {
        var entity = mapper.Map<SavedIdea>(request);

        entity.Idea = await ideaRepository.SelectAsync(i => i.Id.Equals(request.IdeaId))
            ?? throw new NotFoundException($"{nameof(Idea)} is not found by ID: {request.IdeaId}");

        entity.User = await userRepository.SelectAsync(i => i.Id.Equals(request.UserId))
            ?? throw new NotFoundException($"{nameof(User)} is not found by ID: {request.UserId}");

        await repository.InsertAsync(entity);
        await repository.SaveAsync();

        return mapper.Map<SavedIdeaResultDto>(entity);
    }
}