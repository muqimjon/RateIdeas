namespace RateIdeas.Application.IdeaVotes.Commands;

public record CreateIdeaVoteCommand : IRequest<IdeaVoteResultDto>
{
    public CreateIdeaVoteCommand(CreateIdeaVoteCommand command)
    {
        IdeaId = command.IdeaId;
        IsUpvote = command.IsUpvote;
    }
    public bool IsUpvote { get; set; }
    public long IdeaId { get; set; }
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

        if (HttpContextHelper.ResponseHeaders is null || (entity.User = await userRepository
            .SelectAsync(entity => entity.Id.Equals(HttpContextHelper.GetUserId ?? 0))) is null)
            throw new AuthenticationException("Authentication has not been completed");

        entity.Idea = await ideaRepository.SelectAsync(i => i.Id.Equals(request.IdeaId))
            ?? throw new NotFoundException($"{nameof(Idea)} is not found by ID: {request.IdeaId}");

        entity.UserId = (long)HttpContextHelper.GetUserId!;
        await repository.InsertAsync(entity);
        await repository.SaveAsync();

        return mapper.Map<IdeaVoteResultDto>(entity);
    }
}