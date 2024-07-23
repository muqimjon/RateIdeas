using RateIdeas.Application.Users.Commands;
using RateIdeas.Application.Users.Commands.CreateUser;
using RateIdeas.Application.Users.Commands.DeleteUser;
using RateIdeas.Application.Users.Commands.UpdateRole;
using RateIdeas.Application.Users.Commands.UpdateUser;
using RateIdeas.Application.Users.DTOs;
using RateIdeas.Application.Users.Queries;

namespace RateIdeas.WebApi.Controllers.Users;

public class UsersController(IMediator mediator) : BaseController
{
    [HttpPost("create")]
    [ProducesResponseType(typeof(UserResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create(CreateUserCommand command,
        CancellationToken cancellationToken)
        => Ok(new Response
        {
            Data = await mediator.Send(new CreateUserCommand(command), cancellationToken)
        });

    [HttpPut("update")]
    [ProducesResponseType(typeof(UserResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(UpdateUserCommand command,
        CancellationToken cancellationToken)
        => Ok(new Response
        {
            Data = await mediator.Send(new UpdateUserCommand(command), cancellationToken)
        });

    [HttpPut("update-by-id")]
    [ProducesResponseType(typeof(UserResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(UpdateUserByIdCommand command,
        CancellationToken cancellationToken)
        => Ok(new Response
        {
            Data = await mediator.Send(new UpdateUserByIdCommand(command), cancellationToken)
        });

    [HttpPut("update-role")]
    [ProducesResponseType(typeof(UserResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Update([FromForm] UpdateRoleCommand command,
        CancellationToken cancellationToken)
        => Ok(new Response
        {
            Data = await mediator.Send(new UpdateRoleCommand(command), cancellationToken)
        });

    [HttpDelete("delete")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete(CancellationToken cancellationToken)
        => Ok(new Response
        {
            Data = await mediator.Send(new DeleteUserCommand(), cancellationToken)
        });

    [HttpDelete("delete-by-id/{userId:long}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete(long userId, CancellationToken cancellationToken)
        => Ok(new Response
        {
            Data = await mediator.Send(new DeleteUserByIdCommand(userId), cancellationToken)
        });

    [HttpGet("get")]
    [ProducesResponseType(typeof(UserResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
        => Ok(new Response
        {
            Data = await mediator.Send(new GetUserQuery(), cancellationToken)
        });

    [HttpGet("get/{userId:long}")]
    [ProducesResponseType(typeof(UserResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(long userId, CancellationToken cancellationToken)
        => Ok(new Response
        {
            Data = await mediator.Send(new GetUserByIdQuery(userId), cancellationToken)
        });

    [HttpGet("get-all")]
    [ProducesResponseType(typeof(IEnumerable<UserResultDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetForApplication(CancellationToken cancellationToken)
        => Ok(new Response
        {
            Data = await mediator.Send(new GetAllUsersQuery(), cancellationToken)
        });
}
