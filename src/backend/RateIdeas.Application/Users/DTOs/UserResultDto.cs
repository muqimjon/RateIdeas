namespace RateIdeas.Application.Users.DTOs;

public class UserResultDto
{
    public long Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTimeOffset DateOfBirth { get; set; }

    public AssetResultDto Image { get; set; } = default!;
    public ICollection<IdeaResultDto> Users { get; set; } = default!;
    public ICollection<IdeaVoteResultDto> Votes { get; set; } = default!;
    public ICollection<CommentResultDto> Comments { get; set; } = default!;
    public ICollection<CommentVoteResultDto> CommentVotes { get; set; } = default!;
}