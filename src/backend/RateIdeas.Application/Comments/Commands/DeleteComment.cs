namespace RateIdeas.Application.Comments.Commands;

public record DeleteCommentCommand : IRequest<bool>
{
    public DeleteCommentCommand(long userId) { Id = userId; }
    public long Id { get; set; }
}

public class DeleteCommentCommandHandler(IRepository<Comment> repository)
    : IRequestHandler<DeleteCommentCommand, bool>
{
    public async Task<bool> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id.Equals(request.Id))
            ?? throw new NotFoundException($"{nameof(Comment)} is not found with ID: {request.Id}");

        repository.Delete(entity);

        return await repository.SaveAsync() > 0;
    }
}