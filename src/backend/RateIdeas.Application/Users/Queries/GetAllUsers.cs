namespace RateIdeas.Application.Users.Queries;

public record GetAllUsersQuery : IRequest<IEnumerable<IdeaResultDto>>
{
}

public class GetAllUsersQueryHandler(IMapper mapper, IRepository<User> repository) : IRequestHandler<GetAllUsersQuery, IEnumerable<IdeaResultDto>>
{
    public async Task<IEnumerable<IdeaResultDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var entities = (await Task.Run(() => repository.SelectAll())).ToList();
        return mapper.Map<IEnumerable<IdeaResultDto>>(entities);
    }
}
