namespace RateIdeas.Application.IdeaVotes.DTOs;

public class IdeaVoteResultDto
{
    public long Id { get; set; }
    public bool IsUpvote { get; set; }
    public User User { get; set; } = default!;
    public Idea Idea { get; set; } = default!;
}