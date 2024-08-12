namespace RateIdeas.Application.Users.Queries;

public record GetUserByIdQuery : IRequest<UserResultDto>
{
    public GetUserByIdQuery(long id)
    {
        Id = id;
    }

    public long Id { get; set; }
}

public class GetUserByIdQueryHandler(IRepository<User> repository, IMapper mapper)
    : IRequestHandler<GetUserByIdQuery, UserResultDto>
{
    public async Task<UserResultDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        => mapper.Map<UserResultDto>(await repository.SelectAsync(i => i.Id.Equals(request.Id),
            includes: [
                "Image",
                "Ideas.Image",
                "SavedIdeas.Idea",
                "Ideas.Category.Image",
                "Ideas.Votes.User.Image",
                "Ideas.Comments.User.Image",
                "Ideas.Comments.Votes.User.Image",
            ]))
        ?? throw new NotFoundException($"{nameof(User)} is not found with ID: {request.Id}");
}