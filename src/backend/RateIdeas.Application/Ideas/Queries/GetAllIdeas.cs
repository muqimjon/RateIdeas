namespace RateIdeas.Application.Ideas.Queries;

public record GetAllIdeasQuery : IRequest<IEnumerable<IdeaResultDto>>
{
}

public class GetAllIdeasQueryHandler(IMapper mapper, IRepository<Idea> repository) : IRequestHandler<GetAllIdeasQuery, IEnumerable<IdeaResultDto>>
{
    public async Task<IEnumerable<IdeaResultDto>> Handle(GetAllIdeasQuery request, CancellationToken cancellationToken)
    {
        var entities = (await Task.Run(() => repository.SelectAll())).ToList();
        return mapper.Map<IEnumerable<UserResultDto>>(entities);
    }
}
