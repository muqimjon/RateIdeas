namespace RateIdeas.Application.Commons.Exceptions;

public class UnAuthenticationException(string message)
    : BaseException(message, StatusCodes.Status401Unauthorized)
{
    public static string Authentication { get; set; } = nameof(Authentication);
}