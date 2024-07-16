namespace RateIdeas.Application.Users.Queries;

public record GetUserQuery : IRequest<UserResultDto>
{
    public GetUserQuery(long id)
    {
        Id = id;
    }

    public long Id { get; set; }
}

public class GetUserQueryHandler(IRepository<User> repository, IMapper mapper)
    : IRequestHandler<GetUserQuery, UserResultDto>
{
    public async Task<UserResultDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        => mapper.Map<UserResultDto>(await repository.SelectAsync(i => i.Id.Equals(request.Id)))
            ?? throw new NotFoundException($"User is not found with ID = {request.Id}");
}