namespace RateIdeas.Application.Users.Queries;

public record GetAllUsersQuery : IRequest<IEnumerable<UserResultDto>> 
{ 
}

public class GetAllUsersQueryHandler(IMapper mapper, IRepository<User> repository)
    : IRequestHandler<GetAllUsersQuery, IEnumerable<UserResultDto>>
{
    public async Task<IEnumerable<UserResultDto>> Handle(GetAllUsersQuery request,
        CancellationToken cancellationToken)
    {
        var entities = (await Task.Run(() => repository.SelectAll())).ToList();
        return mapper.Map<IEnumerable<UserResultDto>>(entities);
    }
}
