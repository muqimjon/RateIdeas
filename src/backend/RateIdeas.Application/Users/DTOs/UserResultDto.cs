using RateIdeas.Application.Assets.DTOs;
using RateIdeas.Domain.Entities.Comments;
using RateIdeas.Domain.Entities.Ideas;

namespace RateIdeas.Application.Users.DTOs;

public class UserResultDto
{
    public long Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTimeOffset DateOfBirth { get; set; }

    public AssetResultDto Image { get; set; } = default!;
    public ICollection<Idea> Users { get; set; } = default!;
    public ICollection<IdeaVote> Votes { get; set; } = default!;
    public ICollection<Comment> Comments { get; set; } = default!;
    public ICollection<CommentVote> CommentVotes { get; set; } = default!;
}