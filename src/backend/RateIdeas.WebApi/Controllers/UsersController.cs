using RateIdeas.Application.Users.Commands;
using RateIdeas.Application.Users.DTOs;
using RateIdeas.Application.Users.Queries;

namespace RateIdeas.WebApi.Controllers.Users;

public class UsersController(IMediator mediator) : BaseController
{
    [HttpPost("create")]
    [ProducesResponseType(typeof(UserResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create(CreateUserCommand command, CancellationToken cancellationToken)
        => Ok(new Response { Data = await mediator.Send(new CreateUserCommand(command), cancellationToken) });

    [HttpPut("update")]
    [ProducesResponseType(typeof(UserResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(UpdateUserCommand command, CancellationToken cancellationToken)
        => Ok(new Response { Data = await mediator.Send(new UpdateUserCommand(command), cancellationToken) });

    [HttpDelete("delete/{id:long}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
        => Ok(new Response { Data = await mediator.Send(new DeleteUserCommand(id), cancellationToken) });

    [HttpGet("get/{userId:long}")]
    [ProducesResponseType(typeof(UserResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(long userId, CancellationToken cancellationToken)
        => Ok(new Response { Data = await mediator.Send(new GetUserQuery(userId), cancellationToken) });

    [HttpGet("get-all")]
    [ProducesResponseType(typeof(IEnumerable<UserResultDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetForApplication(CancellationToken cancellationToken)
        => Ok(new Response { Data = await mediator.Send(new GetAllUsersQuery(), cancellationToken) });
}
