namespace RateIdeas.Application.Auths.Commands.LogIn;

public class LogInCommandValidator : AbstractValidator<LogInCommand>
{
    public LogInCommandValidator()
    {
        RuleFor(v => v.EmailOrUserName)
            .NotEmpty();
    }
}