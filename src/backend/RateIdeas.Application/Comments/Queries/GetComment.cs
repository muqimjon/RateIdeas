namespace RateIdeas.Application.Comments.Queries;

public record GetCommentQuery : IRequest<CommentResultDto>
{
    public GetCommentQuery(long id)
    {
        Id = id;
    }

    public long Id { get; set; }
}

public class GetCommentQueryHandler(IMapper mapper, IRepository<Comment> repository) :
    IRequestHandler<GetCommentQuery, CommentResultDto>
{
    public async Task<CommentResultDto> Handle(GetCommentQuery request, CancellationToken cancellationToken)
        => mapper.Map<CommentResultDto>(await repository.SelectAsync(i => i.Id.Equals(request.Id)))
            ?? throw new NotFoundException($"{nameof(Comment)} is not found with ID: {request.Id}");
}