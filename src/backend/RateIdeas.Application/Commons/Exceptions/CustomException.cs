namespace RateIdeas.Application.Commons.Exceptions;

public class CustomException(string message, int StatusCode)
    : BaseException(message, StatusCode)
{
}