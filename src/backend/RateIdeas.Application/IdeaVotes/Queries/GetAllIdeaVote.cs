﻿using Microsoft.EntityFrameworkCore;

namespace RateIdeas.Application.IdeaVotes.Queries;

public record GetAllIdeaVotesQuery : IRequest<IEnumerable<IdeaVoteResultDto>>
{
    public GetAllIdeaVotesQuery(GetAllIdeaVotesQuery query)
    {
        Size = query.Size;
        Index = query.Index;
    }

    public int Size { get; set; }
    public int Index { get; set; }
}

public class GetAllIdeaVotesQueryHandler(IMapper mapper, IRepository<IdeaVote> repository)
    : IRequestHandler<GetAllIdeaVotesQuery, IEnumerable<IdeaVoteResultDto>>
{
    public async Task<IEnumerable<IdeaVoteResultDto>> Handle(GetAllIdeaVotesQuery request,
        CancellationToken cancellationToken)
    {
        var entities = await repository.SelectAll(
            includes: [
                "User.Image"
                ])
            .ToPagedList(request.Size, request.Index)
            .ToListAsync(cancellationToken: cancellationToken);

        return mapper.Map<IEnumerable<IdeaVoteResultDto>>(entities);
    }
}
