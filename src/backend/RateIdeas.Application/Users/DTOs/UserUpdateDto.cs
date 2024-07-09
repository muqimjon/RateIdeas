namespace RateIdeas.Application.Users.DTOs;

public class UserUpdateDto
{
    public long Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public DateTimeOffset DateOfBirth { get; set; }
    public long? ImageId { get; set; }
}