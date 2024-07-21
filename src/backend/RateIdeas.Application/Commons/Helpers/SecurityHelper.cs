namespace RateIdeas.Application.Commons.Helpers;

public static class SecurityHelper
{
    public static string Encrypt(string password)
        => BCrypt.Net.BCrypt.HashPassword(password);

    public static bool Verify(string hashedPassword, string password)
        => BCrypt.Net.BCrypt.Verify(password, hashedPassword);

    public static string GenerateCode()
        => new Random().Next(100000, 1000000).ToString();
}