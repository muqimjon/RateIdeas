namespace RateIdeas.Application.Ideas.Queries;

public record GetAllIdeasQuery : IRequest<IEnumerable<IdeaResultDto>>
{
    public GetAllIdeasQuery(GetAllIdeasQuery query)
    {
        Size = query.Size;
        Index = query.Index;
    }

    public int Size { get; set; }
    public int Index { get; set; }
}

public class GetAllIdeasQueryHandler(IMapper mapper, IRepository<Idea> repository)
    : IRequestHandler<GetAllIdeasQuery, IEnumerable<IdeaResultDto>>
{
    public async Task<IEnumerable<IdeaResultDto>> Handle(GetAllIdeasQuery request,
        CancellationToken cancellationToken)
    {
        var entities = (await Task.Run(() => repository.SelectAll(
            includes: ["User.Image", "Comments", "Category.Image", "Votes", "Image"])))
            .ToPagedList(request.Size, request.Index)
            .ToList();

        return mapper.Map<IEnumerable<IdeaResultDto>>(entities);
    }
}
