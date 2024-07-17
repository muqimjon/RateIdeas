namespace RateIdeas.Application.Commons.Exceptions;

public class BaseException(string message) : Exception(message)
{
    public int StatusCode { get; set; } = 404;
}