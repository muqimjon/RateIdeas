namespace RateIdeas.Application.Auths.DTOs;

public class UserRegistrationDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public DateTimeOffset DateOfBirth { get; set; }
    public IFormFile? FormFile { get; set; }
    public string VerificationCode { get; set; } = string.Empty;
    public int AttemptCount { get; set; }
    public bool IsBlocked { get; set; }
    public DateTimeOffset LifeTime { get; set; }
}