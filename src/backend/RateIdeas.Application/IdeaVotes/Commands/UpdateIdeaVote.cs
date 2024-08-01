namespace RateIdeas.Application.IdeaVotes.Commands;

public record UpdateIdeaVoteCommand : IRequest<IdeaVoteResultDto>
{
    public UpdateIdeaVoteCommand(UpdateIdeaVoteCommand command)
    {
        Id = command.Id;
        IdeaId = command.IdeaId;
        Content = command.Content;
    }

    public long Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public long IdeaId { get; set; }
}

public class UpdateIdeaVoteCommandHandler(IMapper mapper,
    IRepository<Idea> ideaRepository,
    IRepository<User> userRepository,
    IRepository<IdeaVote> repository) : IRequestHandler<UpdateIdeaVoteCommand, IdeaVoteResultDto>
{
    public async Task<IdeaVoteResultDto> Handle(UpdateIdeaVoteCommand request,
        CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new NotFoundException($"{nameof(IdeaVote)} is not found by id={request.Id}");

        entity.Idea = await ideaRepository.SelectAsync(i => i.Id.Equals(request.IdeaId))
            ?? throw new NotFoundException($"{nameof(Idea)} is not found by ID: {request.IdeaId}");

        if (HttpContextHelper.ResponseHeaders is null || (entity.User = await userRepository
            .SelectAsync(entity => entity.Id.Equals(HttpContextHelper.GetUserId ?? 0))) is null)
            throw new AuthenticationException("Authentication has not been completed");

        mapper.Map(request, entity);
        entity.UserId = (long)HttpContextHelper.GetUserId!;
        repository.Update(entity);
        await repository.SaveAsync();

        return mapper.Map<IdeaVoteResultDto>(entity);
    }
}