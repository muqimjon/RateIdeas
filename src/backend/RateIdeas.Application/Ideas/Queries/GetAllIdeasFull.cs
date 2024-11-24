using Microsoft.EntityFrameworkCore;

namespace RateIdeas.Application.Ideas.Queries;

public record GetAllIdeasFullQuery : IRequest<IEnumerable<IdeaResultDto>>
{
    public GetAllIdeasFullQuery(GetAllIdeasFullQuery query)
    {
        Size = query.Size;
        Index = query.Index;
    }

    public int Size { get; set; }
    public int Index { get; set; }
}

public class GetAllIdeasFullQueryHandler(IMapper mapper,
    IRepository<Idea> repository,
    IRepository<SavedIdea> savedIdeaRepository)
    : IRequestHandler<GetAllIdeasFullQuery, IEnumerable<IdeaResultDto>>
{
    public async Task<IEnumerable<IdeaResultDto>> Handle(GetAllIdeasFullQuery request,
        CancellationToken cancellationToken)
    {
        var userId = HttpContextHelper.GetUserId ??
            throw new AuthenticationException("Authentication has not been completed");

        var entities = await repository.SelectAll(
            includes: [
                "Comments.User.Image",
                "Comments.Votes.User.Image",
                "Category.Image",
                "Votes.User.Image",
                "Image"
                ])
            .ToPagedList(request.Size, request.Index)
            .ToListAsync(cancellationToken: cancellationToken);

        var result = mapper.Map<IEnumerable<IdeaResultDto>>(entities);
        var savedIdeas = savedIdeaRepository.SelectAll();

        foreach (var entity in result)
            entity.IsSaved = savedIdeas.Any(e
                => e.UserId.Equals(userId)
                && e.IdeaId.Equals(entity.Id));

        return result;
    }
}
