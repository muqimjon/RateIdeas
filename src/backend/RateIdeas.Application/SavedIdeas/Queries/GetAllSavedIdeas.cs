using Microsoft.EntityFrameworkCore;

namespace RateIdeas.Application.SavedIdeas.Queries;

public record GetAllSavedIdeasQuery : IRequest<IEnumerable<SavedIdeaResultDto>>
{
    public GetAllSavedIdeasQuery(GetAllSavedIdeasQuery query)
    {
        Size = query.Size;
        Index = query.Index;
    }
    public int Size { get; set; }
    public int Index { get; set; }
}

public class GetAllSavedIdeasQueryHandler(IMapper mapper, IRepository<SavedIdea> repository)
    : IRequestHandler<GetAllSavedIdeasQuery, IEnumerable<SavedIdeaResultDto>>
{
    public async Task<IEnumerable<SavedIdeaResultDto>> Handle(GetAllSavedIdeasQuery request,
        CancellationToken cancellationToken)
    {
        var entities = await repository.SelectAll(
            includes: [
                "Idea.Image",
                "Idea.User.Image",
                "Idea.Category.Image",
                "Idea.Votes.User.Image",
                "Idea.Comments.User.Image",
                "Idea.Comments.Votes.User.Image",
                ])
            .ToPagedList(request.Size, request.Index)
            .ToListAsync(cancellationToken: cancellationToken);

        return mapper.Map<IEnumerable<SavedIdeaResultDto>>(entities);
    }
}
