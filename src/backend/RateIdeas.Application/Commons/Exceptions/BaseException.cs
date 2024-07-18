namespace RateIdeas.Application.Commons.Exceptions;

public class BaseException(string message, int StatusCode) : Exception(message)
{
    public int StatusCode { get; set; } = StatusCode;
}