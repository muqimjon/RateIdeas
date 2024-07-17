namespace RateIdeas.Application.IdeaVotes.Commands;

public record DeleteIdeaVoteCommand : IRequest<bool>
{
    public DeleteIdeaVoteCommand(long userId) { Id = userId; }
    public long Id { get; set; }
}

public class DeleteIdeaVoteCommandHandler(IRepository<IdeaVote> repository,
    IMediator mediator) : IRequestHandler<DeleteIdeaVoteCommand, bool>
{
    public async Task<bool> Handle(DeleteIdeaVoteCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new NotFoundException($"{nameof(IdeaVote)} is not found by ID: {request.Id}");

        await mediator.Send(new DeleteAssetCommand(request.Id), cancellationToken);
        repository.Delete(entity);

        return await repository.SaveAsync() > 0;
    }
}