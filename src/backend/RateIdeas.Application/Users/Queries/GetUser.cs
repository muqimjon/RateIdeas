namespace RateIdeas.Application.Users.Queries;

public record GetUserQuery : IRequest<UserResultDto>
{
}

public class GetUserQueryHandler(IMapper mapper,
    IRepository<User> repository) : IRequestHandler<GetUserQuery, UserResultDto>
{
    public async Task<UserResultDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        User entity = new();

        if (HttpContextHelper.ResponseHeaders is null
            || (entity = await repository.SelectAsync(entity =>
            entity.Id.Equals(HttpContextHelper.GetUserId ?? 0),
            includes: [
                "Image",
                "Ideas.Image",
                "SavedIdeas.Idea",
                "Ideas.Category.Image",
                "Ideas.Votes.User.Image",
                "Ideas.Comments.User.Image",
                "Ideas.Comments.Votes.User.Image",
            ])) is null)
            throw new AuthenticationException("Authentication has not been completed");

        return mapper.Map<UserResultDto>(entity);
    }
}