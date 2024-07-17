namespace RateIdeas.Application.CommentVotes.Commands;

public record CreateCommentVoteCommand : IRequest<CommentVoteResultDto>
{
    public CreateCommentVoteCommand(CreateCommentVoteCommand command)
    {
        UserId = command.UserId;
        IsUpvote = command.IsUpvote;
        CommentId = command.CommentId;
    }

    public bool IsUpvote { get; set; }
    public long UserId { get; set; }
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

        entity.Comment = await commentRepository.SelectAsync(i => i.Id.Equals(request.CommentId))
            ?? throw new NotFoundException($"{nameof(Comment)} is not found by ID: {request.CommentId}");

        entity.User = await userRepository.SelectAsync(i => i.Id.Equals(request.UserId))
            ?? throw new NotFoundException($"{nameof(User)} is not found by ID: {request.UserId}");

        await repository.InsertAsync(entity);
        await repository.SaveAsync();

        return mapper.Map<CommentVoteResultDto>(entity);
    }
}