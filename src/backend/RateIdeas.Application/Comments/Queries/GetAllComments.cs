namespace RateIdeas.Application.Comments.Queries;

public record GetAllCommentsQuery : IRequest<IEnumerable<CommentResultDto>>
{
}

public class GetAllCommentsQueryHandler(IMapper mapper, IRepository<Comment> repository)
    : IRequestHandler<GetAllCommentsQuery, IEnumerable<CommentResultDto>>
{
    public async Task<IEnumerable<CommentResultDto>> Handle(GetAllCommentsQuery request,
        CancellationToken cancellationToken)
    {
        var entities = (await Task.Run(() => repository.SelectAll())).ToList();
        return mapper.Map<IEnumerable<CommentResultDto>>(entities);
    }
}
