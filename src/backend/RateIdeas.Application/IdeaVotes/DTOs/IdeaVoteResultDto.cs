namespace RateIdeas.Application.IdeaVotes.DTOs;

public class IdeaVoteResultDto
{
    public long Id { get; set; }
    public bool IsUpvote { get; set; }
    public UserResultForPropDto User { get; set; } = default!;
    public long IdeaId { get; set; }
}