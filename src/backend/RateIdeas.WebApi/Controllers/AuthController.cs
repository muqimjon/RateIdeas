using RateIdeas.Application.Ideas.Commands;
using RateIdeas.Application.Users.DTOs;

namespace RateIdeas.WebApi.Controllers;

public class AuthController(IMediator mediator) : BaseController
{
    [HttpPost("login")]
    [ProducesResponseType(typeof(UserResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create(GenerateTokenCommand command,
    CancellationToken cancellationToken)
        => Ok(new Response
        {
            Data = await mediator.Send(new GenerateTokenCommand(command), cancellationToken)
        });
}
