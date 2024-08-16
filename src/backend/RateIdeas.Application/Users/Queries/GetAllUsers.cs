using Microsoft.EntityFrameworkCore;

namespace RateIdeas.Application.Users.Queries;

public record GetAllUsersQuery : IRequest<IEnumerable<UserResultDto>>
{
    public GetAllUsersQuery(GetAllUsersQuery query)
    {
        Size = query.Size;
        Index = query.Index;
    }

    public int Size { get; set; }
    public int Index { get; set; }
}

public class GetAllUsersQueryHandler(IMapper mapper, IRepository<User> repository)
    : IRequestHandler<GetAllUsersQuery, IEnumerable<UserResultDto>>
{
    public async Task<IEnumerable<UserResultDto>> Handle(GetAllUsersQuery request,
        CancellationToken cancellationToken)
    {
        var entities = await repository.SelectAll(
            includes: [
                "Image",
                "Ideas.Image",
                "SavedIdeas.Idea",
                "Ideas.Category.Image",
                "Ideas.Votes.User.Image",
                "Ideas.Comments.User.Image",
                "Ideas.Comments.Votes.User.Image",
                ])
            .ToPagedList(request.Size, request.Index)
            .ToListAsync(cancellationToken: cancellationToken);

        return mapper.Map<IEnumerable<UserResultDto>>(entities);
    }
}
