namespace RateIdeas.Application.IdeaVotes.Queries;

public record GetIdeaVoteQuery : IRequest<IdeaVoteResultDto>
{
    public GetIdeaVoteQuery(long id)
    {
        Id = id;
    }

    public long Id { get; set; }
}

public class GetIdeaVoteQueryHandler(IMapper mapper, IRepository<IdeaVote> repository)
    : IRequestHandler<GetIdeaVoteQuery, IdeaVoteResultDto>
{
    public async Task<IdeaVoteResultDto> Handle(GetIdeaVoteQuery request,
        CancellationToken cancellationToken)
        => mapper.Map<IdeaVoteResultDto>(await repository.SelectAsync(i => i.Id.Equals(request.Id)))
            ?? throw new NotFoundException($"{nameof(IdeaVote)} is not found with ID: {request.Id}");
}