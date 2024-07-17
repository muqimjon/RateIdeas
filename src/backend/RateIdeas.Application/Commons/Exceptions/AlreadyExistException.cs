namespace RateIdeas.Application.Commons.Exceptions;

public class AlreadyExistException(string message) : BaseException(message)
{
    public int StatusCode { get; set; } = 403;
}