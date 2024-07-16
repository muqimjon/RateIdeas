namespace RateIdeas.Application.Categories.Queries;

public record GetCategoryQuery : IRequest<CategoryResultDto>
{
    public GetCategoryQuery(long userId)
    {
        Id = userId;
    }

    public long Id { get; set; }
}

public class GetCategoryQueryHandler(IRepository<Category> repository, IMapper mapper)
    : IRequestHandler<GetCategoryQuery, CategoryResultDto>
{
    public async Task<CategoryResultDto> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        => mapper.Map<CategoryResultDto>(await repository.SelectAsync(i => i.Id.Equals(request.Id)))
            ?? throw new NotFoundException($"Category is not found with ID = {request.Id}");
}