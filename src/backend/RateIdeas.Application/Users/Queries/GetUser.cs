namespace RateIdeas.Application.Users.Queries;

public record GetUserQuery : IRequest<IdeaResultDto>
{
    public GetUserQuery(long userId)
    {
        Id = userId;
    }

    public long Id { get; set; }
}

public class GetUserQueryHandler(IRepository<User> repository, IMapper mapper) : IRequestHandler<GetUserQuery, IdeaResultDto>
{
    public async Task<IdeaResultDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        => mapper.Map<IdeaResultDto>(await repository.SelectAsync(i => i.Id.Equals(request.Id)))
            ?? throw new NotFoundException($"User is not found with ID = {request.Id}");
}