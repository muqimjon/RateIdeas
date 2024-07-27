using RateIdeas.Domain.Common;
using RateIdeas.Domain.Entities.Comments;
using RateIdeas.Domain.Entities.Ideas;
using RateIdeas.Domain.Enums;

namespace RateIdeas.Domain.Entities;

public class User : Auditable
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public DateTimeOffset DateOfBirth { get; set; }
    public Roles Role { get; set; }

    public long? ImageId { get; set; }
    public Asset Image { get; set; } = default!;

    public ICollection<Idea> Ideas { get; set; } = default!;
    public ICollection<IdeaVote> IdeaVotes { get; set; } = default!;
    public ICollection<Comment> Comments { get; set; } = default!;
    public ICollection<CommentVote> CommentVotes { get; set; } = default!;
    public ICollection<SavedIdea> SavedIdeas { get; set; } = default!;
}
