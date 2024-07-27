namespace RateIdeas.Application.Users.Commands.UpdateRole;

public record UpdateRoleCommand : IRequest<bool>
{
    public UpdateRoleCommand(UpdateRoleCommand command)
    {
        UserId = command.UserId;
        Role = command.Role;
    }

    public long UserId { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Roles Role { get; set; }
}

public class UpdateRoleCommandHandler(IRepository<User> userRepository)
    : IRequestHandler<UpdateRoleCommand, bool>
{
    public async Task<bool> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {

        if (HttpContextHelper.ResponseHeaders is null ||HttpContextHelper.GetUserId is null)
            throw new AuthenticationException("Authentication has not been completed");
        
        if (request.UserId.Equals(1))
            throw new ForbiddenException("Delete general moderator is forbidden");

        var entity = await userRepository.SelectAsync(entity => entity.Id.Equals(request.UserId))
            ?? throw new NotFoundException($"{nameof(User)} is not found by ID: {request.UserId}");

        entity.Role = request.Role switch
        {
            Roles.SuperAdmin => Roles.SuperAdmin,
            Roles.Admin => Roles.Admin,
            _ => Roles.User,
        };

        userRepository.Update(entity);
        await userRepository.SaveAsync();

        return default!;
    }
}
