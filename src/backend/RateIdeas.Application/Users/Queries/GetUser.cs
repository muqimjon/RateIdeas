namespace RateIdeas.Application.Users.Queries;

public record GetUserQuery : IRequest<UserResultDto>
{
}

public class GetUserQueryHandler(IMapper mapper,
    IRepository<User> repository) : IRequestHandler<GetUserQuery, UserResultDto>
{
    public async Task<UserResultDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        => mapper.Map<UserResultDto>(await repository.SelectAsync(i => i.Id.Equals(HttpContextHelper.GetUserId)))
            ?? throw new UnAuthenticationException("Authentication has not been completed   ");
}