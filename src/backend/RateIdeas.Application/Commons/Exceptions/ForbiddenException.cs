namespace RateIdeas.Application.Commons.Exceptions;

public class ForbiddenException(string message)
    : BaseException(message, StatusCodes.Status403Forbidden)
{
}