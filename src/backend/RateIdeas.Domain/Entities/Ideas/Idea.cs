using RateIdeas.Domain.Common;
using RateIdeas.Domain.Entities.Comments;

namespace RateIdeas.Domain.Entities.Ideas;

public class Idea : Auditable
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public long? ImageId { get; set; }
    public Asset Image { get; set; } = default!;

    public long CategoryId { get; set; }
    public Category Category { get; set; } = default!;

    public long UserId { get; set; }
    public User User { get; set; } = default!;

    public ICollection<Comment> Comments { get; set; } = default!;
    public ICollection<IdeaVote> Votes { get; set; } = default!;
}
