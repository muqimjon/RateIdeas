namespace RateIdeas.Application.Comments.Commands;

public record UpdateCommentCommand : IRequest<CommentResultDto>
{
    public UpdateCommentCommand(UpdateCommentCommand command)
    {
        Id = command.Id;
        IdeaId = command.IdeaId;
        UserId = command.UserId;
        Content = command.Content;
    }

    public long Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public long IdeaId { get; set; }
    public long UserId { get; set; }
}

public class UpdateCommentCommandHandler(IMapper mapper,
    IRepository<Idea> ideaRepository,
    IRepository<User> userRepository,
    IRepository<Comment> repository) : IRequestHandler<UpdateCommentCommand, CommentResultDto>
{
    public async Task<CommentResultDto> Handle(UpdateCommentCommand request,
        CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new NotFoundException($"{nameof(Comment)} is not found by ID: {request.Id}");

        entity.Idea = await ideaRepository.SelectAsync(i => i.Id.Equals(request.IdeaId))
            ?? throw new NotFoundException($"{nameof(Idea)} is not found by ID: {request.IdeaId}");

        entity.User = await userRepository.SelectAsync(i => i.Id.Equals(request.UserId))
            ?? throw new NotFoundException($"{nameof(User)} is not found by ID: {request.UserId}");

        mapper.Map(request, entity);

        repository.Update(entity);
        await repository.SaveAsync();

        return mapper.Map<CommentResultDto>(entity);
    }
}