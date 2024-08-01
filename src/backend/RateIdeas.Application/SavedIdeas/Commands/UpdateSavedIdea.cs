namespace RateIdeas.Application.SavedIdeas.Commands;

public record UpdateSavedIdeaCommand : IRequest<SavedIdeaResultDto>
{
    public UpdateSavedIdeaCommand(UpdateSavedIdeaCommand command)
    {
        Id = command.Id;
        IdeaId = command.IdeaId;
        Content = command.Content;
    }

    public long Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public long IdeaId { get; set; }
}

public class UpdateSavedIdeaCommandHandler(IMapper mapper,
    IRepository<Idea> ideaRepository,
    IRepository<User> userRepository,
    IRepository<SavedIdea> repository) : IRequestHandler<UpdateSavedIdeaCommand, SavedIdeaResultDto>
{
    public async Task<SavedIdeaResultDto> Handle(UpdateSavedIdeaCommand request,
        CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new NotFoundException($"{nameof(SavedIdea)} is not found by ID: {request.Id}");

        entity.Idea = await ideaRepository.SelectAsync(i => i.Id.Equals(request.IdeaId))
            ?? throw new NotFoundException($"{nameof(Idea)} is not found by ID: {request.IdeaId}");

        if (HttpContextHelper.ResponseHeaders is null || await userRepository
            .SelectAsync(entity => entity.Id.Equals(HttpContextHelper.GetUserId ?? 0)) is null)
            throw new AuthenticationException("Authentication has not been completed");

        if (!HttpContextHelper.GetUserId.Equals(entity.UserId))
            throw new AuthorizationException("Permission denied");

        mapper.Map(request, entity);
        entity.UserId = (long)HttpContextHelper.GetUserId!;
        repository.Update(entity);
        await repository.SaveAsync();

        return mapper.Map<SavedIdeaResultDto>(entity);
    }
}