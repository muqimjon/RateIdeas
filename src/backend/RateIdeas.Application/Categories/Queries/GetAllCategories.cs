namespace RateIdeas.Application.Categories.Queries;

public record GetAllCategoriesQuery : IRequest<IEnumerable<CategoryResultDto>>
{
}

public class GetAllCategoriesQueryHandler(IMapper mapper, IRepository<Category> repository) : IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoryResultDto>>
{
    public async Task<IEnumerable<CategoryResultDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var entities = (await Task.Run(() => repository.SelectAll())).ToList();
        return mapper.Map<IEnumerable<CategoryResultDto>>(entities);
    }
}
