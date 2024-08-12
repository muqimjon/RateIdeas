namespace RateIdeas.Application.CommentVotes.DTOs;

public class CommentVoteResultDto
{
    public long Id { get; set; }
    public bool IsUpvote { get; set; }
    public UserResultForPropDto User { get; set; } = default!;
    public ComentResultForPropDto Comment { get; set; } = default!;
}