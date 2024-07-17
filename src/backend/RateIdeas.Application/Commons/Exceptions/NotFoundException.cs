﻿namespace RateIdeas.Application.Commons.Exceptions;

public class NotFoundException(string message) : BaseException(message)
{
    public int StatusCode { get; set; } = 404;
}
