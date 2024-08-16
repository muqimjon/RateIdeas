using Microsoft.EntityFrameworkCore;

namespace RateIdeas.Application.Ideas.Queries;

public record GetIdeasByUserIdQuery : IRequest<IEnumerable<IdeaResultDto>>
{
    public GetIdeasByUserIdQuery(long id)
    {
        Id = id;
    }

    public long Id { get; set; }
}

public class GetIdeasByUserIdQueryHandler(IRepository<Idea> repository, IMapper mapper)
    : IRequestHandler<GetIdeasByUserIdQuery, IEnumerable<IdeaResultDto>>
{
    public async Task<IEnumerable<IdeaResultDto>> Handle(GetIdeasByUserIdQuery request,
        CancellationToken cancellationToken)
        => mapper.Map<IEnumerable<IdeaResultDto>>(
            await repository.SelectAll(i => i.UserId.Equals(request.Id),
                includes: [
                    "User.Image",
                    "Comments.User.Image",
                    "Comments.Votes.User.Image",
                    "Category.Image",
                    "Votes.User.Image",
                    "Image"
                    ]
                )
            .ToListAsync(cancellationToken: cancellationToken))
        ?? throw new NotFoundException($"{nameof(Idea)} is not found with ID: {request.Id}");
}