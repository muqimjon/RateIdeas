namespace RateIdeas.Application.IdeaVotes.Commands;

public record ToggleIdeaVoteCommand : IRequest<IdeaVoteResultDto>
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
    : IRequestHandler<ToggleIdeaVoteCommand, IdeaVoteResultDto>
{
    public async Task<IdeaVoteResultDto> Handle(
        ToggleIdeaVoteCommand request, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<IdeaVote>(request);

        if (HttpContextHelper.ResponseHeaders is null || (entity.User = await userRepository
            .SelectAsync(entity => entity.Id.Equals(HttpContextHelper.GetUserId ?? 0),
            includes: [
                "Image"
                ])
            ) is null)
            throw new AuthenticationException("Authentication has not been completed");

        entity.UserId = entity.User.Id;

        _ = await ideaRepository.SelectAsync(entity => entity.Id.Equals(request.IdeaId))
            ?? throw new NotFoundException(
                $"{nameof(Idea)} is not found exception with ID: {request.IdeaId}");

        var existVote = await repository.SelectAsync(item
            => item.UserId.Equals(entity.UserId)
            && item.IdeaId.Equals(entity.IdeaId));

        if (existVote is null)
        {
            await repository.InsertAsync(entity);
            await repository.SaveAsync();
            return mapper.Map<IdeaVoteResultDto>(entity);
        }
        else if (existVote.IsUpvote.Equals(entity.IsUpvote))
        {
            repository.Delete(existVote);
            await repository.SaveAsync();
            return mapper.Map<IdeaVoteResultDto>(default);
        }

        mapper.Map(request, existVote);
        repository.Update(existVote);
        await repository.SaveAsync();
        return mapper.Map<IdeaVoteResultDto>(existVote);
    }
}
