namespace RateCategorys.Application.Categorys.Queries;

public record GetCategoryQuery : IRequest<UserResultDto>
{
    public GetCategoryQuery(long userId)
    {
        Id = userId;
    }

    public long Id { get; set; }
}

public class GetCategoryQueryHandler(IRepository<Category> repository, IMapper mapper) : IRequestHandler<GetCategoryQuery, UserResultDto>
{
    public async Task<UserResultDto> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        => mapper.Map<UserResultDto>(await repository.SelectAsync(i => i.Id.Equals(request.Id)))
            ?? throw new NotFoundException($"Category is not found with ID = {request.Id}");
}