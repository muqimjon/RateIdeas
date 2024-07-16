namespace RateIdeas.Application.Ideas.Queries;

public record GetAllIdeasQuery : IRequest<IEnumerable<UserResultDto>>
{
}

public class GetAllIdeasQueryHandler(IMapper mapper, IRepository<Idea> repository) : IRequestHandler<GetAllIdeasQuery, IEnumerable<UserResultDto>>
{
    public async Task<IEnumerable<UserResultDto>> Handle(GetAllIdeasQuery request, CancellationToken cancellationToken)
    {
        var entities = (await Task.Run(() => repository.SelectAll())).ToList();
        return mapper.Map<IEnumerable<UserResultDto>>(entities);
    }
}
