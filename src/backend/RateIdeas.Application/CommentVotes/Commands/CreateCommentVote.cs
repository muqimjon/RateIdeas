namespace RateIdeas.Application.CommentVotes.Commands;

public record CreateCommentVoteCommand : IRequest<CommentVoteResultDto>
{
    public CreateCommentVoteCommand(CreateCommentVoteCommand command)
    {
        IsUpvote = command.IsUpvote;
        CommentId = command.CommentId;
    }

    public bool IsUpvote { get; set; }
    public long CommentId { get; set; }
}

public class CreateCommentVoteCommandHandler(IMapper mapper,
    IRepository<User> userRepository,
    IRepository<Comment> commentRepository,
    IRepository<CommentVote> repository) : IRequestHandler<CreateCommentVoteCommand, CommentVoteResultDto>
{
    public async Task<CommentVoteResultDto> Handle(CreateCommentVoteCommand request,
        CancellationToken cancellationToken)
    {
        var entity = mapper.Map<CommentVote>(request);

        if (HttpContextHelper.ResponseHeaders is null || (entity.User = await userRepository
            .SelectAsync(entity => entity.Id.Equals(HttpContextHelper.GetUserId ?? 0))) is null)
            throw new AuthenticationException("Authentication has not been completed");

        entity.Comment = await commentRepository.SelectAsync(i => i.Id.Equals(request.CommentId))
            ?? throw new NotFoundException($"{nameof(Comment)} is not found by ID: {request.CommentId}");

        entity.UserId = (long)HttpContextHelper.GetUserId!;
        await repository.InsertAsync(entity);
        await repository.SaveAsync();

        return mapper.Map<CommentVoteResultDto>(entity);
    }
}