namespace RateIdeas.Application.Users.Commands;

public record DeleteUserCommand : IRequest<bool>
{
    public DeleteUserCommand(long userId) { Id = userId; }
    public long Id { get; set; }
}

public class DeleteUserCommandHandler(IRepository<User> repository,
    IMediator mediator) : IRequestHandler<DeleteUserCommand, bool>
{
    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new NotFoundException($"This User is not found by id: {request.Id} | Delete User");

        await mediator.Send(new DeleteAssetCommand(request.Id), cancellationToken);
        repository.Delete(entity);
        return await repository.SaveAsync() > 0;
    }
}