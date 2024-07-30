namespace RateIdeas.Application.Commons.Exceptions;

public class TooManyAttemptsException(string message)
    : BaseException(message, StatusCodes.Status429TooManyRequests)
{
}