﻿namespace RateIdeas.Application.SavedIdeas.Queries;

public record GetSavedIdeaQuery : IRequest<SavedIdeaResultDto>
{
    public GetSavedIdeaQuery(long id)
    {
        Id = id;
    }

    public long Id { get; set; }
}

public class GetSavedIdeaQueryHandler(IMapper mapper, IRepository<SavedIdea> repository)
    : IRequestHandler<GetSavedIdeaQuery, SavedIdeaResultDto>
{
    public async Task<SavedIdeaResultDto> Handle(GetSavedIdeaQuery request,
        CancellationToken cancellationToken)
        => mapper.Map<SavedIdeaResultDto>(await repository.SelectAsync(i
            => i.Id.Equals(request.Id),
            includes: [
                "Idea.Image",
                "Idea.User.Image",
                "Idea.Category.Image",
                "Idea.Votes.User.Image",
                "Idea.Comments.User.Image",
                "Idea.Comments.Votes.User.Image",
                ])
            )
        ?? throw new NotFoundException($"{nameof(SavedIdea)} is not found with ID: {request.Id}");
}