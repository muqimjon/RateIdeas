namespace RateCategorys.Application.Categorys.Queries;

public record GetAllCategorysQuery : IRequest<IEnumerable<UserResultDto>>
{
}

public class GetAllCategorysQueryHandler(IMapper mapper, IRepository<Category> repository) : IRequestHandler<GetAllCategorysQuery, IEnumerable<UserResultDto>>
{
    public async Task<IEnumerable<UserResultDto>> Handle(GetAllCategorysQuery request, CancellationToken cancellationToken)
    {
        var entities = (await Task.Run(() => repository.SelectAll())).ToList();
        return mapper.Map<IEnumerable<UserResultDto>>(entities);
    }
}
