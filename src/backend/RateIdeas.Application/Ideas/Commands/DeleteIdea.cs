namespace RateIdeas.Application.Ideas.Commands;

public record DeleteIdeaCommand : IRequest<bool>
{
    public DeleteIdeaCommand(long userId) { Id = userId; }
    public long Id { get; set; }
}

public class DeleteIdeaCommandHandler(IRepository<Idea> repository,
    IMediator mediator) : IRequestHandler<DeleteIdeaCommand, bool>
{
    public async Task<bool> Handle(DeleteIdeaCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id.Equals(request.Id))
            ?? throw new NotFoundException($"{nameof(Idea)} is not found with ID={request.Id}");

        await mediator.Send(new DeleteAssetCommand(request.Id), cancellationToken);
        repository.Delete(entity);
        return await repository.SaveAsync() > 0;
    }
}