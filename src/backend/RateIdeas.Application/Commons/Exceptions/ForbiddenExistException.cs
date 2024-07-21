namespace RateIdeas.Application.Commons.Exceptions;

public class ForbiddenExistException(string message)
    : BaseException(message, StatusCodes.Status403Forbidden)
{
}