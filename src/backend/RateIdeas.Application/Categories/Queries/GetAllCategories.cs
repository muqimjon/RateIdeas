namespace RateIdeas.Application.Categories.Queries;

public record GetAllCategoriesQuery : IRequest<IEnumerable<UserResultDto>>
{
}

public class GetAllCategoriesQueryHandler(IMapper mapper, IRepository<Category> repository) : IRequestHandler<GetAllCategoriesQuery, IEnumerable<UserResultDto>>
{
    public async Task<IEnumerable<UserResultDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var entities = (await Task.Run(() => repository.SelectAll())).ToList();
        return mapper.Map<IEnumerable<UserResultDto>>(entities);
    }
}
