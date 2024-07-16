namespace RateIdeas.Application.SavedIdeas.Commands;

public record DeleteSavedIdeaCommand : IRequest<bool>
{
    public DeleteSavedIdeaCommand(long userId) { Id = userId; }
    public long Id { get; set; }
}

public class DeleteSavedIdeaCommandHandler(IRepository<SavedIdea> repository,
    IMediator mediator) : IRequestHandler<DeleteSavedIdeaCommand, bool>
{
    public async Task<bool> Handle(DeleteSavedIdeaCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new NotFoundException($"{nameof(SavedIdea)} is not found by ID={request.Id}");

        await mediator.Send(new DeleteAssetCommand(request.Id), cancellationToken);
        repository.Delete(entity);

        return await repository.SaveAsync() > 0;
    }
}