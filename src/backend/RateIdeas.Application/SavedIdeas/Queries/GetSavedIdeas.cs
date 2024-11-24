using Microsoft.EntityFrameworkCore;

namespace RateIdeas.Application.SavedIdeas.Queries;

public record GetAllSavedIdeasQuery(int Size, int Index) : IRequest<IEnumerable<SavedIdeaResultDto>>;

public class GetAllSavedIdeasQueryHandler(IMapper mapper, IRepository<SavedIdea> repository)
    : IRequestHandler<GetAllSavedIdeasQuery, IEnumerable<SavedIdeaResultDto>>
{
    public async Task<IEnumerable<SavedIdeaResultDto>> Handle(GetAllSavedIdeasQuery request,
        CancellationToken cancellationToken)
    {
        if (HttpContextHelper.GetUserId is null)
            throw new AuthenticationException("Authentication has not been completed");

        var entities = await repository.SelectAll(
            si => si.UserId == HttpContextHelper.GetUserId,
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
