namespace RateIdeas.Application.Ideas.Queries;

public record GetIdeasQuery : IRequest<IEnumerable<IdeaResultDto>>
{
}

public class GetIdeasQueryHandler(IMapper mapper,
    IRepository<User> userRepository,
    IRepository<Idea> repository)
    : IRequestHandler<GetIdeasQuery, IEnumerable<IdeaResultDto>>
{
    public async Task<IEnumerable<IdeaResultDto>> Handle(GetIdeasQuery request, CancellationToken cancellationToken)
    {
        _ = await userRepository.SelectAsync(entity => entity.Id.Equals(HttpContextHelper.GetUserId ?? 0))
            ?? throw new AuthenticationException("Authentication has not been completed");

        return mapper.Map<IEnumerable<IdeaResultDto>>(await repository.SelectAsync(i
            => i.UserId.Equals(HttpContextHelper.GetUserId)));
    }
}