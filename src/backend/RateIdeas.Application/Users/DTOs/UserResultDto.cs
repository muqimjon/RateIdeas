namespace RateIdeas.Application.Users.DTOs;

public class UserResultDto
{
    public long Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public Roles Role { get; set; }
    public DateTimeOffset DateOfBirth { get; set; }

    public long ImageId { get; set; }
    public AssetResultDto Image { get; set; } = default!;

    public ICollection<IdeaResultForPropDto> Ideas { get; set; } = default!;
    public ICollection<SavedIdeaResultDto> SavedIdeas { get; set; } = default!;
}