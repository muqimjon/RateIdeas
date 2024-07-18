namespace RateIdeas.Application.Commons.Exceptions;

public class AlreadyExistException(string message) : BaseException(message, 403)
{
}
