namespace RateIdeas.Application.CommentVotes.DTOs;

public class CommentVoteResultDto
{
    public long Id { get; set; }
    public bool IsUpvote { get; set; }
    public User User { get; set; } = default!;
    public Comment Idea { get; set; } = default!;
}