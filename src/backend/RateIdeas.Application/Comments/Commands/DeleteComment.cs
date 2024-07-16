namespace RateIdeas.Application.Categories.Commands;

public record DeleteCommentCommand : IRequest<bool>
{
    public DeleteCommentCommand(long userId) { Id = userId; }
    public long Id { get; set; }
}

public class DeleteCommentCommandHandler(IRepository<Comment> repository,
    IMediator mediator) : IRequestHandler<DeleteCommentCommand, bool>
{
    public async Task<bool> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id.Equals(request.Id))
            ?? throw new NotFoundException($"{nameof(Comment)} is not found with ID={request.Id}");

        await mediator.Send(new DeleteAssetCommand(request.Id), cancellationToken);
        repository.Delete(entity);

        return await repository.SaveAsync() > 0;
    }
}