using RateIdeas.Application.IdeaVotes.Commands;
using RateIdeas.Application.IdeaVotes.DTOs;
using RateIdeas.Application.IdeaVotes.Queries;

namespace RateIdeas.WebApi.Controllers.IdeaVotes;

public class IdeaVotesController(IMediator mediator) : BaseController
{
    [HttpPost("create")]
    [ProducesResponseType(typeof(IdeaVoteResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create(CreateIdeaVoteCommand command,
        CancellationToken cancellationToken)
        => Ok(new Response
        {
            Data = await mediator.Send(new CreateIdeaVoteCommand(command), cancellationToken)
        });

    [HttpPut("update")]
    [ProducesResponseType(typeof(IdeaVoteResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(UpdateIdeaVoteCommand command,
        CancellationToken cancellationToken)
        => Ok(new Response
        {
            Data = await mediator.Send(new UpdateIdeaVoteCommand(command), cancellationToken)
        });

    [HttpDelete("delete/{id:long}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
        => Ok(new Response
        {
            Data = await mediator.Send(new DeleteIdeaVoteCommand(id), cancellationToken)
        });

    [HttpGet("get/{userId:long}")]
    [ProducesResponseType(typeof(IdeaVoteResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(long userId, CancellationToken cancellationToken)
        => Ok(new Response
        {
            Data = await mediator.Send(new GetIdeaVoteQuery(userId), cancellationToken)
        });

    [HttpGet("get-all")]
    [ProducesResponseType(typeof(IEnumerable<IdeaVoteResultDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetForApplication([FromQuery] GetAllIdeaVotesQuery query,
        CancellationToken cancellationToken)
        => Ok(new Response
        {
            Data = await mediator.Send(new GetAllIdeaVotesQuery(query), cancellationToken)
        });
}
