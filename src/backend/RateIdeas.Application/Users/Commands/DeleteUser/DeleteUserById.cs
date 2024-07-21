namespace RateIdeas.Application.Users.Commands.DeleteUser;

public record DeleteUserByIdCommand : IRequest<bool>
{
    public DeleteUserByIdCommand(long userId) { Id = userId; }
    public long Id { get; set; }
}

public class DeleteUserByIdCommandHandler(IRepository<User> repository,
    IMediator mediator) : IRequestHandler<DeleteUserByIdCommand, bool>
{
    public async Task<bool> Handle(DeleteUserByIdCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id.Equals(request.Id))
            ?? throw new NotFoundException($"{nameof(User)} is not found by ID: {request.Id}");

        if (entity.Role.Equals(Roles.SuperAdmin))
            throw new ForbiddenExistException("Deleting a SuperAdmin is forbidden.");

        if (entity.Image is not null)
            await mediator.Send(new DeleteAssetCommand(entity.ImageId), cancellationToken);

        repository.Delete(entity);

        return await repository.SaveAsync() > 0;
    }
}