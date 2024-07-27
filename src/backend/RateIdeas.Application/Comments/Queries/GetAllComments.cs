namespace RateIdeas.Application.Comments.Queries;

public record GetAllCommentsQuery : IRequest<IEnumerable<CommentResultDto>>
{
    public GetAllCommentsQuery(GetAllCommentsQuery query)
    {
        Size = query.Size;
        Index = query.Index;
    }

    public int Size { get; set; }
    public int Index { get; set; }
}

public class GetAllCommentsQueryHandler(IMapper mapper, IRepository<Comment> repository)
    : IRequestHandler<GetAllCommentsQuery, IEnumerable<CommentResultDto>>
{
    public async Task<IEnumerable<CommentResultDto>> Handle(GetAllCommentsQuery request,
        CancellationToken cancellationToken)
    {
        var entities = (await Task.Run(() => repository.SelectAll()))
            .ToPagedList(request.Size, request.Index)
            .ToList();

        return mapper.Map<IEnumerable<CommentResultDto>>(entities);
    }
}
