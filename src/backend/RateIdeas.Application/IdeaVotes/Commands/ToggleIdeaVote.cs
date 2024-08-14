namespace RateIdeas.Application.IdeaVotes.Commands;

public record ToggleIdeaVoteCommand : IRequest<bool>
{
    public ToggleIdeaVoteCommand(ToggleIdeaVoteCommand command)
    {
        IsUpvote = command.IsUpvote;
        IdeaId = command.IdeaId;
    }

    public bool IsUpvote { get; set; }
    public long IdeaId { get; set; }
}

public class ToggleIdeaVoteCommandHandler(IMapper mapper,
    IRepository<IdeaVote> repository,
    IRepository<Idea> ideaRepository,
    IRepository<User> userRepository)
    : IRequestHandler<ToggleIdeaVoteCommand, bool>
{
    public async Task<bool> Handle(
        ToggleIdeaVoteCommand request, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<IdeaVote>(request);

        if (HttpContextHelper.ResponseHeaders is null || (entity.User = await userRepository
            .SelectAsync(entity => entity.Id.Equals(HttpContextHelper.GetUserId ?? 0))) is null)
            throw new AuthenticationException("Authentication has not been completed");

        entity.UserId = entity.User.Id;

        entity.Idea = await ideaRepository.SelectAsync(entity => entity.Id.Equals(request.IdeaId))
            ?? throw new NotFoundException(
                $"{nameof(Idea)} is not found exception with ID: {request.IdeaId}");

        var existVote = await repository.SelectAsync(item
            => item.UserId.Equals(entity.UserId)
            && item.IdeaId.Equals(entity.IdeaId));

        if (existVote is null)
            await repository.InsertAsync(entity);
        else if (existVote.IsUpvote.Equals(entity.IsUpvote))
            repository.Delete(existVote);
        else
        {
            existVote.IsUpvote = request.IsUpvote;
            repository.Update(existVote);
        }

        return await repository.SaveAsync() > 0;
    }
}
