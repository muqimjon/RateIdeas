namespace RateIdeas.Application.CommentVotes.Queries;

public record GetAllCommentVotesQuery : IRequest<IEnumerable<CommentVoteResultDto>>
{
    public GetAllCommentVotesQuery(GetAllCommentVotesQuery query)
    {
        Size = query.Size;
        Index = query.Index;
    }

    public int Size { get; set; }
    public int Index { get; set; }
}

public class GetAllCommentVotesQueryHandler(IMapper mapper, IRepository<CommentVote> repository)
    : IRequestHandler<GetAllCommentVotesQuery, IEnumerable<CommentVoteResultDto>>
{
    public async Task<IEnumerable<CommentVoteResultDto>> Handle(GetAllCommentVotesQuery request,
        CancellationToken cancellationToken)
    {
        var entities = (await Task.Run(() => repository.SelectAll(includes: ["User.Image"])))
            .ToPagedList(request.Size, request.Index)
            .ToList();

        return mapper.Map<IEnumerable<CommentVoteResultDto>>(entities);
    }
}
