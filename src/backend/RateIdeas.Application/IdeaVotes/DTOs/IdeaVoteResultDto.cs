namespace RateIdeas.Application.IdeaVotes.DTOs;

public class IdeaVoteResultDto
{
    public long Id { get; set; }
    public bool IsUpvote { get; set; }
    public UserResultDto User { get; set; } = default!;
    public IdeaResultDto Idea { get; set; } = default!;
}