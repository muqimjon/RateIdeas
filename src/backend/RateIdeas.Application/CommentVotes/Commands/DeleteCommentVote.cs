namespace RateIdeas.Application.CommentVotes.Commands;

public record DeleteCommentVoteCommand : IRequest<bool>
{
    public DeleteCommentVoteCommand(long userId) { Id = userId; }
    public long Id { get; set; }
}

public class DeleteCommentVoteCommandHandler(IRepository<CommentVote> repository)
    : IRequestHandler<DeleteCommentVoteCommand, bool>
{
    public async Task<bool> Handle(DeleteCommentVoteCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id.Equals(request.Id))
            ?? throw new NotFoundException($"{nameof(CommentVote)} is not found with ID: {request.Id}");

        repository.Delete(entity);

        return await repository.SaveAsync() > 0;
    }
}