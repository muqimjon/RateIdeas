namespace RateIdeas.Application.CommentVotes.Commands;

public record UpdateCommentVoteCommand : IRequest<CommentVoteResultDto>
{
    public UpdateCommentVoteCommand(UpdateCommentVoteCommand command)
    {
        Id = command.Id;
        UserId = command.UserId;
        IsUpvote = command.IsUpvote;
        CommentId = command.CommentId;
    }

    public long Id { get; set; }
    public bool IsUpvote { get; set; }
    public long UserId { get; set; }
    public long CommentId { get; set; }
}

public class UpdateCommentVoteCommandHandler(IMapper mapper,
    IRepository<User> userRepository,
    IRepository<Comment> commentRepository,
    IRepository<CommentVote> repository) : IRequestHandler<UpdateCommentVoteCommand, CommentVoteResultDto>
{
    public async Task<CommentVoteResultDto> Handle(UpdateCommentVoteCommand request,
        CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new NotFoundException($"{nameof(CommentVote)} is not found by ID: {request.Id}");

        entity.Comment = await commentRepository.SelectAsync(i => i.Id.Equals(request.CommentId))
            ?? throw new NotFoundException($"{nameof(Idea)} is not found by ID: {request.CommentId}");

        entity.User = await userRepository.SelectAsync(i => i.Id.Equals(request.UserId))
            ?? throw new NotFoundException($"{nameof(User)} is not found by ID: {request.UserId}");

        mapper.Map(request, entity);

        repository.Update(entity);
        await repository.SaveAsync();

        return mapper.Map<CommentVoteResultDto>(entity);
    }
}