using RateIdeas.Domain.Common;

namespace RateIdeas.Domain.Entities.Ideas;

public class SavedIdea : Auditable
{
    public int UserId { get; set; }
    public User User { get; set; } = default!;

    public int IdeaId { get; set; }
    public Idea Idea { get; set; } = default!;
}