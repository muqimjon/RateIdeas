using RateIdeas.Domain.Common;
using RateIdeas.Domain.Entities.Votes;

namespace RateIdeas.Domain.Entities;

public class Idea : Auditable
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public long? ImageId { get; set; }
    public Asset Image { get; set; } = default!;

    public int CategoryId { get; set; }
    public Category Category { get; set; } = default!;

    public int UserId { get; set; }
    public User User { get; set; } = default!;

    public ICollection<Comment> Comments { get; set; } = default!;
    public ICollection<IdeaVote> Votes { get; set; } = default!;
}
