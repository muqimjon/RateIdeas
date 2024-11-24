using Microsoft.AspNetCore.Authorization;
using RateIdeas.Application.Auths.Commands.LogIn;
using RateIdeas.Application.Auths.Commands.MailVerification;
using RateIdeas.Application.Auths.Commands.Register;
using RateIdeas.Application.Auths.DTOs;
using RateIdeas.WebApi.Controllers.Commons;

namespace RateIdeas.WebApi.Controllers;

public class AuthController(IMediator mediator) : BaseController
{
    /// <summary>
    /// Tizimga kirish
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("login")]
    [ProducesResponseType(typeof(UserResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> LogIn(LogInCommand command,
    CancellationToken cancellationToken)
        => Ok(new Response
        {
            Data = await mediator.Send(command, cancellationToken)
        });

    /// <summary>
    /// Ro'yxatdan o'tish
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("sign-up")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<IActionResult> SignUp(RegisterCommand command,
    CancellationToken cancellationToken)
        => Ok(new Response
        {
            Data = await mediator.Send(command, cancellationToken)
        });

    /// <summary>
    /// Emailga yuborilgan tasdiqlash kodini yuborish
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [AllowAnonymous]
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
