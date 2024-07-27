namespace RateIdeas.Application.IdeaVotes.Commands;

public record CreateIdeaVoteCommand : IRequest<IdeaVoteResultDto>
{
    public CreateIdeaVoteCommand(CreateIdeaVoteCommand command)
    {
        IdeaId = command.IdeaId;
        UserId = command.UserId;
        IsUpvote = command.IsUpvote;
    }
    public bool IsUpvote { get; set; }
    public long IdeaId { get; set; }
    public long UserId { get; set; }
}

public class CreateIdeaVoteCommandHandler(IMapper mapper,
    IRepository<Idea> ideaRepository,
    IRepository<User> userRepository,
    IRepository<IdeaVote> repository) : IRequestHandler<CreateIdeaVoteCommand, IdeaVoteResultDto>
{
    public async Task<IdeaVoteResultDto> Handle(CreateIdeaVoteCommand request,
        CancellationToken cancellationToken)
    {
        var entity = mapper.Map<IdeaVote>(request);

        entity.Idea = await ideaRepository.SelectAsync(i => i.Id.Equals(request.IdeaId))
            ?? throw new NotFoundException($"{nameof(Idea)} is not found by ID: {request.IdeaId}");

        entity.User = await userRepository.SelectAsync(i => i.Id.Equals(request.UserId))
            ?? throw new NotFoundException($"{nameof(User)} is not found by ID: {request.UserId}");

        await repository.InsertAsync(entity);
        await repository.SaveAsync();

        return mapper.Map<IdeaVoteResultDto>(entity);
    }
}