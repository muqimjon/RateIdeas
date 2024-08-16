﻿using Microsoft.EntityFrameworkCore;

namespace RateIdeas.Application.Ideas.Queries;

public record GetIdeasQuery : IRequest<IEnumerable<IdeaResultDto>>
{
}

public class GetIdeasQueryHandler(IMapper mapper,
    IRepository<Idea> repository)
    : IRequestHandler<GetIdeasQuery, IEnumerable<IdeaResultDto>>
{
    public async Task<IEnumerable<IdeaResultDto>> Handle(
        GetIdeasQuery request, CancellationToken cancellationToken)
    {
        if (HttpContextHelper.ResponseHeaders is null || HttpContextHelper.GetUserId is null)
            throw new AuthenticationException("Authentication has not been completed");

        return mapper.Map<IEnumerable<IdeaResultDto>>(
            await repository.SelectAll(i => i.UserId.Equals(HttpContextHelper.GetUserId ?? 0),
                includes: [
                    "User.Image",
                    "Comments.User.Image",
                    "Comments.Votes.User.Image",
                    "Category.Image",
                    "Votes.User.Image",
                    "Image"
                    ]
                )
            .ToListAsync(cancellationToken: cancellationToken));
    }
}