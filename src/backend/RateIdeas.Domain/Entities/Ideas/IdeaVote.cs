using RateIdeas.Domain.Common;

namespace RateIdeas.Domain.Entities.Ideas;

public class IdeaVote : Auditable
{
    public bool IsUpvote { get; set; }

    public long UserId { get; set; }
    public User User { get; set; } = default!;

    public long IdeaId { get; set; }
    public Idea Idea { get; set; } = default!;
}
