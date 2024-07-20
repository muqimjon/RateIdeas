namespace RateIdeas.Application.Commons.Exceptions;

public class NotFoundException(string message)
    : BaseException(message, StatusCodes.Status404NotFound)
{
}
