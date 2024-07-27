namespace RateIdeas.Application.Users.DTOs;

public class UserResultForPropDto
{
    public long Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public Roles Role { get; set; }
    public DateTimeOffset DateOfBirth { get; set; }

    public AssetResultDto Image { get; set; } = default!;
}