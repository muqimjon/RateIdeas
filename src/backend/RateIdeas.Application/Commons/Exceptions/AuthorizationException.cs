namespace RateIdeas.Application.Commons.Exceptions;

public class AuthorizationException(string message)
    : BaseException(message, StatusCodes.Status401Unauthorized)
{
}