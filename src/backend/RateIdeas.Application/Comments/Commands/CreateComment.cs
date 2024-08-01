namespace RateIdeas.Application.Comments.Commands;

public record CreateCommentCommand : IRequest<CommentResultDto>
{
    public CreateCommentCommand(CreateCommentCommand command)
    {
        IdeaId = command.IdeaId;
        Content = command.Content;
    }
    public string Content { get; set; } = string.Empty;
    public long IdeaId { get; set; }
}

public class CreateCommentCommandHandler(IMapper mapper,
    IRepository<User> userRepository,
    IRepository<Idea> ideaRepository,
    IRepository<Comment> repository) : IRequestHandler<CreateCommentCommand, CommentResultDto>
{
    public async Task<CommentResultDto> Handle(CreateCommentCommand request,
        CancellationToken cancellationToken)
    {
        var entity = mapper.Map<Comment>(request);

        if (HttpContextHelper.ResponseHeaders is null || (entity.User = await userRepository
            .SelectAsync(entity => entity.Id.Equals(HttpContextHelper.GetUserId ?? 0))) is null)
            throw new AuthenticationException("Authentication has not been completed");

        entity.Idea = await ideaRepository.SelectAsync(i => i.Id.Equals(request.IdeaId))
            ?? throw new NotFoundException($"{nameof(Idea)} is not found by ID: {request.IdeaId}");

        entity.UserId = (long)HttpContextHelper.GetUserId!;
        await repository.InsertAsync(entity);
        await repository.SaveAsync();

        return mapper.Map<CommentResultDto>(entity);
    }
}