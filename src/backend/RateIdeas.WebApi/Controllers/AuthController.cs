using RateIdeas.Application.Auths.Commands.LogIn;
using RateIdeas.Application.Auths.Commands.MailVerification;
using RateIdeas.Application.Auths.Commands.Register;
using RateIdeas.Application.Auths.DTOs;

namespace RateIdeas.WebApi.Controllers;

public class AuthController(IMediator mediator) : BaseController
{
    [HttpPost("login")]
    [ProducesResponseType(typeof(UserResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Login(LogInCommand command,
    CancellationToken cancellationToken)
        => Ok(new Response
        {
            Data = await mediator.Send(command, cancellationToken)
        });

    [HttpPost("sign-up")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<IActionResult> SignUp(RegisterCommand command,
    CancellationToken cancellationToken)
        => Ok(new Response
        {
            Data = await mediator.Send(command, cancellationToken)
        });

    [HttpPost("verify-email")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<IActionResult> Verify(VerifyEmailCommand command,
    CancellationToken cancellationToken)
        => Ok(new Response
        {
            Data = await mediator.Send(command, cancellationToken)
        });

    //[HttpPost("send-email")]
    //public async ValueTask<IActionResult> SendEmailAsync([FromForm] SendEmailCommand command,
    //    CancellationToken cancellationToken)
    //    => Ok(new Response
    //    {
    //        Data = await mediator.Send(command, cancellationToken)
    //    });
}
