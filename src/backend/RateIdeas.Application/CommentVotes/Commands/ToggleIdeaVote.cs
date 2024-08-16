namespace RateIdeas.Application.CommentVotes.Commands;

public record ToggleCommentVoteCommand : IRequest<CommentVoteResultDto>
{
    public ToggleCommentVoteCommand(ToggleCommentVoteCommand command)
    {
        IsUpvote = command.IsUpvote;
        CommentId = command.CommentId;
    }

    public bool IsUpvote { get; set; }
    public long CommentId { get; set; }
}

public class ToggleCommentVoteCommandHandler(IMapper mapper,
    IRepository<CommentVote> repository,
    IRepository<Comment> ideaRepository,
    IRepository<User> userRepository)
    : IRequestHandler<ToggleCommentVoteCommand, CommentVoteResultDto>
{
    public async Task<CommentVoteResultDto> Handle(
        ToggleCommentVoteCommand request, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<CommentVote>(request);

        if (HttpContextHelper.ResponseHeaders is null || (entity.User = await userRepository
            .SelectAsync(entity => entity.Id.Equals(HttpContextHelper.GetUserId ?? 0))) is null)
            throw new AuthenticationException("Authentication has not been completed");

        entity.UserId = entity.User.Id;

        entity.Comment = await ideaRepository.SelectAsync(entity => entity.Id.Equals(request.CommentId))
            ?? throw new NotFoundException(
                $"{nameof(Comment)} is not found exception with ID: {request.CommentId}");

        var existVote = await repository.SelectAsync(item
            => item.UserId.Equals(entity.UserId)
            && item.CommentId.Equals(entity.CommentId));

        if (existVote is null)
        {
            await repository.InsertAsync(entity);
            await repository.SaveAsync();
            return mapper.Map<CommentVoteResultDto>(entity);
        }
        else if (existVote.IsUpvote.Equals(entity.IsUpvote))
        {
            repository.Delete(existVote);
            await repository.SaveAsync();
            return mapper.Map<CommentVoteResultDto>(default);
        }

        mapper.Map(request, existVote);
        repository.Update(existVote);
        await repository.SaveAsync();
        return mapper.Map<CommentVoteResultDto>(existVote);
    }
}
