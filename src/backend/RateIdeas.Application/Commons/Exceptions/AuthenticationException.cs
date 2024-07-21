namespace RateIdeas.Application.Commons.Exceptions;

public class AuthenticationException(string message)
    : BaseException(message, StatusCodes.Status407ProxyAuthenticationRequired)
{
}
