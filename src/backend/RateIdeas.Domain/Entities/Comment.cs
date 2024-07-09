using RateIdeas.Domain.Common;
using RateIdeas.Domain.Entities.Votes;

namespace RateIdeas.Domain.Entities;

public class Comment : Auditable
{
    public string Content { get; set; } = string.Empty;

    public long IdeaId { get; set; }
    public Idea Idea { get; set; } = default!;

    public long UserId { get; set; }
    public User User { get; set; } = default!;

    public ICollection<CommentVote> Votes { get; set; } = default!;
}