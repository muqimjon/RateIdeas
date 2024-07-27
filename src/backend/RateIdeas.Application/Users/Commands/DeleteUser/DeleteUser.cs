namespace RateIdeas.Application.Users.Commands.DeleteUser;

public record DeleteUserCommand : IRequest<bool>
{
}

public class DeleteUserCommandHandler(IRepository<User> repository,
    IMediator mediator) : IRequestHandler<DeleteUserCommand, bool>
{
    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        User entity = new();

        if (HttpContextHelper.ResponseHeaders is null || (entity = await repository
            .SelectAsync(entity => entity.Id.Equals(HttpContextHelper.GetUserId ?? 0))) is null)
            throw new AuthenticationException("Authentication has not been completed");

        if (entity.Role.Equals(Roles.SuperAdmin))
            throw new ForbiddenException("Deleting a SuperAdmin is forbidden.");

        if (entity.Image is not null)
            await mediator.Send(new DeleteAssetCommand(entity.ImageId), cancellationToken);

        repository.Delete(entity);
        HttpContextHelper.Accessor.HttpContext!.Items.Clear();

        return await repository.SaveAsync() > 0;
    }
}