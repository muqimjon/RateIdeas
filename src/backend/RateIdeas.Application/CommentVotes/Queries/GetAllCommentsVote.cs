namespace RateIdeas.Application.CommentVotes.Queries;

public record GetAllCommentVotesQuery : IRequest<IEnumerable<CommentVoteResultDto>>
{
}

public class GetAllCommentVotesQueryHandler(IMapper mapper, IRepository<CommentVote> repository)
    : IRequestHandler<GetAllCommentVotesQuery, IEnumerable<CommentVoteResultDto>>
{
    public async Task<IEnumerable<CommentVoteResultDto>> Handle(GetAllCommentVotesQuery request,
        CancellationToken cancellationToken)
    {
        var entities = (await Task.Run(() => repository.SelectAll())).ToList();
        return mapper.Map<IEnumerable<CommentVoteResultDto>>(entities);
    }
}
