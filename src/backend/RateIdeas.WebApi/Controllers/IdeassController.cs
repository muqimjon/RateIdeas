using RateIdeas.Application.Ideas.Commands;
using RateIdeas.Application.Ideas.DTOs;
using RateIdeas.Application.Ideas.Queries;

namespace RateIdeas.WebApi.Controllers.Ideas;

public class IdeasController(IMediator mediator) : BaseController
{
    [HttpPost("create")]
    [ProducesResponseType(typeof(IdeaResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create(CreateIdeaCommand command, CancellationToken cancellationToken)
        => Ok(new Response
        {
            Data = await mediator.Send(
            new CreateIdeaCommand(command), cancellationToken)
        });

    [HttpPut("update")]
    [ProducesResponseType(typeof(IdeaResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(UpdateIdeaCommand command, CancellationToken cancellationToken)
        => Ok(new Response
        {
            Data = await mediator.Send(
            new UpdateIdeaCommand(command), cancellationToken)
        });

    [HttpDelete("delete/{id:long}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
        => Ok(new Response
        {
            Data = await mediator.Send(
            new DeleteIdeaCommand(id), cancellationToken)
        });

    [HttpGet("get/{ideaId:long}")]
    [ProducesResponseType(typeof(IdeaResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(long ideaId, CancellationToken cancellationToken)
        => Ok(new Response
        {
            Data = await mediator.Send(
            new GetIdeaQuery(ideaId), cancellationToken)
        });

    [HttpGet("get-all")]
    [ProducesResponseType(typeof(IEnumerable<IdeaResultDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetForApplication(CancellationToken cancellationToken)
        => Ok(new Response
        {
            Data = await mediator.Send(
            new GetAllIdeasQuery(), cancellationToken)
        });
}
