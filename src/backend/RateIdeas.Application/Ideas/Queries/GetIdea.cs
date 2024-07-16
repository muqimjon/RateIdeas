namespace RateIdeas.Application.Ideas.Queries;

public record GetIdeaQuery : IRequest<IdeaResultDto>
{
    public GetIdeaQuery(long userId)
    {
        Id = userId;
    }

    public long Id { get; set; }
}

public class GetIdeaQueryHandler(IRepository<Idea> repository, IMapper mapper)
    : IRequestHandler<GetIdeaQuery, IdeaResultDto>
{
    public async Task<IdeaResultDto> Handle(GetIdeaQuery request, CancellationToken cancellationToken)
        => mapper.Map<IdeaResultDto>(await repository.SelectAsync(i => i.Id.Equals(request.Id)))
            ?? throw new NotFoundException($"Idea is not found with ID = {request.Id}");
}