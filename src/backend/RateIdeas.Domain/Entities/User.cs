using RateIdeas.Domain.Common;
using RateIdeas.Domain.Entities.Comments;
using RateIdeas.Domain.Entities.Ideas;

namespace RateIdeas.Domain.Entities;

public class User : Auditable
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public DateTimeOffset DateOfBirth { get; set; }

    public long? ImageId { get; set; }
    public Asset Image { get; set; } = default!;

    public ICollection<SavedIdea> SavedIdeas { get; set; } = default!;
    public ICollection<Idea> Users { get; set; } = default!;
    public ICollection<IdeaVote> Votes { get; set; } = default!;
    public ICollection<Comment> Comments { get; set; } = default!;
    public ICollection<CommentVote> CommentVotes { get; set; } = default!;
}
