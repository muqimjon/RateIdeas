using Microsoft.EntityFrameworkCore;

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
        var entities = await repository.SelectAll(
            includes: [
                "Image",
                "Ideas.User.Image",
                "Ideas.Comments.User.Image",
                "Ideas.Comments.Votes.User.Image",
                "Ideas.Votes.User.Image",
                "Ideas.Image"
                ])
            .ToPagedList(request.Size, request.Index)
            .ToListAsync(cancellationToken: cancellationToken);

        return mapper.Map<IEnumerable<CategoryResultDto>>(entities);
    }
}
