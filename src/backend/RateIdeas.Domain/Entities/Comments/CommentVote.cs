using RateIdeas.Domain.Common;

namespace RateIdeas.Domain.Entities.Comments;

public class CommentVote : Auditable
{
    public bool IsUpvote { get; set; }

    public long UserId { get; set; }
    public User User { get; set; } = default!;

    public long IdeaId { get; set; }
    public Comment Idea { get; set; } = default!;
}