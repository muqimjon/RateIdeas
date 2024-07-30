namespace RateIdeas.Application.Auths.DTOs;

public class UserResponseDto
{
    public long Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public AssetResultDto Asset { get; set; } = default!;
    public string Token { get; set; } = string.Empty;
}