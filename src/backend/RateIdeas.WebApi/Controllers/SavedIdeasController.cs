using RateIdeas.Application.SavedIdeas.Commands;
using RateIdeas.Application.SavedIdeas.DTOs;
using RateIdeas.Application.SavedIdeas.Queries;

namespace RateIdeas.WebApi.Controllers.SavedIdeas;

public class SavedIdeasController(IMediator mediator) : BaseController
{
    [HttpPost("toggle-saved-idea")]
    [ProducesResponseType(typeof(long), StatusCodes.Status200OK)]
    public async Task<IActionResult> Toggle(ToggleSavedIdeaCommand command,
        CancellationToken cancellationToken)
        => Ok(new Response
        {
            Data = await mediator.Send(command, cancellationToken)
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
            Data = await mediator.Send(query, cancellationToken)
        });
}