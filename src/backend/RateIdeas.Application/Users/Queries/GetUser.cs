namespace RateIdeas.Application.Users.Queries;

public record GetUserQuery : IRequest<UserResultDto>
{
}

public class GetUserQueryHandler(IMapper mapper,
    IRepository<User> repository) : IRequestHandler<GetUserQuery, UserResultDto>
{
    public async Task<UserResultDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        User entity = new();

        if (HttpContextHelper.ResponseHeaders is null
            || (entity = await repository.SelectAsync(entity =>
            entity.Id.Equals(HttpContextHelper.GetUserId ?? 0))) is null)
            throw new AuthenticationException("Authentication has not been completed");

        return mapper.Map<UserResultDto>(entity);
    }
}