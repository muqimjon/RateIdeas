using RateIdeas.Domain.Entities.Comments;

namespace RateIdeas.Application.Ideas.DTOs;

public class IdeaResultDto
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Asset Image { get; set; } = default!;
    public Category Category { get; set; } = default!;
    public User User { get; set; } = default!;

    public ICollection<Comment> Comments { get; set; } = default!;
    public ICollection<IdeaVote> Votes { get; set; } = default!;
}