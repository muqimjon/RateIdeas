namespace RateIdeas.Application.Commons.Exceptions;

public class AlreadyExistException(string message)
    : BaseException(message, StatusCodes.Status409Conflict)
{
}
