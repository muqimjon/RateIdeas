using RateIdeas.Application.CommentVotes.Commands;
using RateIdeas.Application.CommentVotes.DTOs;
using RateIdeas.Application.CommentVotes.Queries;

namespace RateIdeas.WebApi.Controllers.Comments;

public class CommentVotesController(IMediator mediator) : BaseController
{
    [HttpPost("toggle-comment-vote")]
    [ProducesResponseType(typeof(CommentVoteResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create(ToggleCommentVoteCommand command,
        CancellationToken cancellationToken)
        => Ok(new Response
        {
            Data = await mediator.Send(new ToggleCommentVoteCommand(command), cancellationToken)
        });

    [HttpGet("get/{savedIdeaId:long}")]
    [ProducesResponseType(typeof(CommentVoteResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(long savedIdeaId, CancellationToken cancellationToken)
        => Ok(new Response
        {
            Data = await mediator.Send(new GetCommentVoteQuery(savedIdeaId), cancellationToken)
        });

    [HttpGet("get-all")]
    [ProducesResponseType(typeof(IEnumerable<CommentVoteResultDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetForApplication([FromQuery] GetAllCommentVotesQuery query,
        CancellationToken cancellationToken)
        => Ok(new Response
        {
            Data = await mediator.Send(new GetAllCommentVotesQuery(query), cancellationToken)
        });
}