namespace RateIdeas.Application.SavedIdeas.Commands;

public record ToggleSavedIdeaCommand(long IdeaId) : IRequest<long?>;

public class ToggleSavedIdeaCommandHandler(IMapper mapper,
    IRepository<SavedIdea> repository,
    IRepository<User> userRepository)
    : IRequestHandler<ToggleSavedIdeaCommand, long?>
{
    public async Task<long?> Handle(
        ToggleSavedIdeaCommand request, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<SavedIdea>(request);

        if (HttpContextHelper.ResponseHeaders is null || (entity.User = await userRepository
            .SelectAsync(entity => entity.Id.Equals(HttpContextHelper.GetUserId ?? 0))) is null)
            throw new AuthenticationException("Authentication has not been completed");

        entity.UserId = entity.User.Id;

        var existSavedIdea = await repository.SelectAsync(item
            => item.UserId.Equals(entity.UserId)
            && item.IdeaId.Equals(request.IdeaId));

        if (existSavedIdea is null)
        {
            await repository.InsertAsync(entity);
            await repository.SaveAsync();
            return entity.Id;
        }

        repository.Delete(existSavedIdea);
        await repository.SaveAsync();
        return default;
    }
}
