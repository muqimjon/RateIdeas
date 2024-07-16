namespace RateIdeas.Application.Categories.Commands;

public record DeleteCategoryCommand : IRequest<bool>
{
    public DeleteCategoryCommand(long userId) { Id = userId; }
    public long Id { get; set; }
}

public class DeleteCategoryCommandHandler(IRepository<Category> repository,
    IMediator mediator) : IRequestHandler<DeleteCategoryCommand, bool>
{
    public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new NotFoundException($"{nameof(Category)} is not found with ID={request.Id}");

        await mediator.Send(new DeleteAssetCommand(request.Id), cancellationToken);
        repository.Delete(entity);
        return await repository.SaveAsync() > 0;
    }
}