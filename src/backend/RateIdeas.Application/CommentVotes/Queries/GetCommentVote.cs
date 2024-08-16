namespace RateIdeas.Application.CommentVotes.Queries;

public record GetCommentVoteQuery : IRequest<CommentVoteResultDto>
{
    public GetCommentVoteQuery(long id)
    {
        Id = id;
    }

    public long Id { get; set; }
}

public class GetCommentVoteQueryHandler(IMapper mapper, IRepository<CommentVote> repository) :
    IRequestHandler<GetCommentVoteQuery, CommentVoteResultDto>
{
    public async Task<CommentVoteResultDto> Handle(GetCommentVoteQuery request, CancellationToken cancellationToken)
        => mapper.Map<CommentVoteResultDto>(
            await repository.SelectAsync(i => i.Id.Equals(request.Id),
                includes: [
                    "User.Image",
                ])
            )
        ?? throw new NotFoundException($"{nameof(CommentVote)} is not found with ID: {request.Id}");
}