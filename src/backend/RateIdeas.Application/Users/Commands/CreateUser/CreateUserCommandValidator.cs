namespace RateIdeas.Application.Users.Commands.CreateUser;

public class UpdateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(v => v.Email)
            .NotEmpty()
            .EmailAddress();
    }
}
