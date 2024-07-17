namespace RateIdeas.Application.Categories.Commands;

public record CreateCommentCommand : IRequest<CommentResultDto>
{
    public CreateCommentCommand(CreateCommentCommand command)
    {
        IdeaId = command.IdeaId;
        UserId = command.UserId;
        Content = command.Content;
    }
    public string Content { get; set; } = string.Empty;
    public long IdeaId { get; set; }
    public long UserId { get; set; }
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

        entity.Idea = await ideaRepository.SelectAsync(i => i.Id.Equals(request.IdeaId))
            ?? throw new NotFoundException($"{nameof(Idea)} is not found by ID: {request.IdeaId}");

        entity.User = await userRepository.SelectAsync(i => i.Id.Equals(request.UserId))
            ?? throw new NotFoundException($"{nameof(User)} is not found by ID: {request.UserId}");

        await repository.InsertAsync(entity);
        await repository.SaveAsync();

        return mapper.Map<CommentResultDto>(entity);
    }
}