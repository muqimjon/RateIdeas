namespace RateIdeas.Application.Commons.Exceptions;

public class UnAuthorizedException(string message) : BaseException(message, 401)
{
}