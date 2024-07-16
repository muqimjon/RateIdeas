namespace RateIdeas.Application.IdeaVotes.Queries;

public record GetAllIdeaVotesQuery : IRequest<IEnumerable<IdeaVoteResultDto>>
{
}

public class GetAllIdeaVotesQueryHandler(IMapper mapper, IRepository<IdeaVote> repository)
    : IRequestHandler<GetAllIdeaVotesQuery, IEnumerable<IdeaVoteResultDto>>
{
    public async Task<IEnumerable<IdeaVoteResultDto>> Handle(GetAllIdeaVotesQuery request,
        CancellationToken cancellationToken)
    {
        var entities = (await Task.Run(() => repository.SelectAll())).ToList();
        return mapper.Map<IEnumerable<IdeaVoteResultDto>>(entities);
    }
}
