using RateIdeas.Application.Ideas.Commands;
using RateIdeas.Application.Senders.EmailServices.Commands;
using RateIdeas.Application.Users.DTOs;

namespace RateIdeas.WebApi.Controllers;

public class AuthController(IMediator mediator) : BaseController
{
    [HttpPost("login")]
    [ProducesResponseType(typeof(UserResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create(LogInCommand command,
    CancellationToken cancellationToken)
        => Ok(new Response
        {
            Data = await mediator.Send(command, cancellationToken)
        });

    [HttpPost("send-email")]
    public async ValueTask<IActionResult> SendEmailAsync([FromForm] SendEmailCommand command,
        CancellationToken cancellationToken)
    {
        await mediator.Send(command, cancellationToken);
        return Ok();
    }
}
