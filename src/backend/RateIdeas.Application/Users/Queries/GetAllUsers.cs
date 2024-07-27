namespace RateIdeas.Application.Users.Queries;

public record GetAllUsersQuery : IRequest<IEnumerable<UserResultDto>>
{
    public GetAllUsersQuery(GetAllUsersQuery query)
    {
        Size = query.Size;
        Index = query.Index;
    }
    public int Size { get; set; }
    public int Index { get; set; }
}

public class GetAllUsersQueryHandler(IMapper mapper, IRepository<User> repository)
    : IRequestHandler<GetAllUsersQuery, IEnumerable<UserResultDto>>
{
    public async Task<IEnumerable<UserResultDto>> Handle(GetAllUsersQuery request,
        CancellationToken cancellationToken)
    {
        var entities = (await Task.Run(() => repository.SelectAll()))
            .ToPagedList(request.Size, request.Index)
            .ToList();

        return mapper.Map<IEnumerable<UserResultDto>>(entities);
    }
}
