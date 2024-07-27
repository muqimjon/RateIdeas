namespace RateIdeas.Application.Categories.Queries;

public record GetAllCategoriesQuery : IRequest<IEnumerable<CategoryResultDto>>
{
    public GetAllCategoriesQuery(GetAllCategoriesQuery query)
    {
        Size = query.Size;
        Index = query.Index;
    }

    public int Size { get; set; }
    public int Index { get; set; }
}

public class GetAllCategoriesQueryHandler(IMapper mapper, IRepository<Category> repository)
    : IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoryResultDto>>
{
    public async Task<IEnumerable<CategoryResultDto>> Handle(GetAllCategoriesQuery request,
        CancellationToken cancellationToken)
    {
        var entities = (await Task.Run(() => repository.SelectAll()))
            .ToPagedList(request.Size, request.Index)
            .ToList();

        return mapper.Map<IEnumerable<CategoryResultDto>>(entities);
    }
}
