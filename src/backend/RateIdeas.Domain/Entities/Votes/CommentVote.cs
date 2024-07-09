using RateIdeas.Domain.Common;

namespace RateIdeas.Domain.Entities.Votes;

public class CommentVote : Auditable
{
    public bool IsUpvote { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = default!;

    public int IdeaId { get; set; }
    public Comment Idea { get; set; } = default!;
}