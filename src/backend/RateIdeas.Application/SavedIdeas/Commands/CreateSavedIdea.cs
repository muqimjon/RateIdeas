namespace RateIdeas.Application.SavedIdeas.Commands;

public record CreateSavedIdeaCommand : IRequest<SavedIdeaResultDto>
{
    public CreateSavedIdeaCommand(CreateSavedIdeaCommand command)
    {
        IdeaId = command.IdeaId;
    }
    public long IdeaId { get; set; }
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

        if (HttpContextHelper.ResponseHeaders is null || (entity.User = await userRepository
            .SelectAsync(entity => entity.Id.Equals(HttpContextHelper.GetUserId ?? 0))) is null)
            throw new AuthenticationException("Authentication has not been completed");

        entity.Idea = await ideaRepository.SelectAsync(i => i.Id.Equals(request.IdeaId))
            ?? throw new NotFoundException($"{nameof(Idea)} is not found by ID: {request.IdeaId}");

        entity.UserId = (long)HttpContextHelper.GetUserId!;
        await repository.InsertAsync(entity);
        await repository.SaveAsync();

        return mapper.Map<SavedIdeaResultDto>(entity);
    }
}