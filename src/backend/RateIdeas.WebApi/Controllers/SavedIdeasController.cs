using RateIdeas.Application.SavedIdeas.Commands;
using RateIdeas.Application.SavedIdeas.DTOs;
using RateIdeas.Application.SavedIdeas.Queries;

namespace RateIdeas.WebApi.Controllers.SavedIdeas;

public class SavedIdeasController(IMediator mediator) : BaseController
{
    [HttpPost("create")]
    [ProducesResponseType(typeof(SavedIdeaResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create(CreateSavedIdeaCommand command,
        CancellationToken cancellationToken)
        => Ok(new Response
        {
            Data = await mediator.Send(new CreateSavedIdeaCommand(command), cancellationToken)
        });

    [HttpPut("update")]
    [ProducesResponseType(typeof(SavedIdeaResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(UpdateSavedIdeaCommand command,
        CancellationToken cancellationToken)
        => Ok(new Response
        {
            Data = await mediator.Send(new UpdateSavedIdeaCommand(command), cancellationToken)
        });

    [HttpDelete("delete/{id:long}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
        => Ok(new Response
        {
            Data = await mediator.Send(new DeleteSavedIdeaCommand(id), cancellationToken)
        });

    [HttpGet("get/{savedIdeaId:long}")]
    [ProducesResponseType(typeof(SavedIdeaResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(long savedIdeaId, CancellationToken cancellationToken)
        => Ok(new Response
        {
            Data = await mediator.Send(new GetSavedIdeaQuery(savedIdeaId), cancellationToken)
        });

    [HttpGet("get-all")]
    [ProducesResponseType(typeof(IEnumerable<SavedIdeaResultDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetForApplication([FromQuery] GetAllSavedIdeasQuery query,
        CancellationToken cancellationToken)
        => Ok(new Response
        {
            Data = await mediator.Send(new GetAllSavedIdeasQuery(query), cancellationToken)
        });
}