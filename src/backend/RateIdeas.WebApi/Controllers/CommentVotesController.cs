using RateIdeas.Application.CommentVotes.Commands;
using RateIdeas.Application.CommentVotes.DTOs;
using RateIdeas.Application.CommentVotes.Queries;

namespace RateIdeas.WebApi.Controllers.Comments;

public class CommentVotesController(IMediator mediator) : BaseController
{
    [HttpPost("create")]
    [ProducesResponseType(typeof(CommentVoteResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create(CreateCommentVoteCommand command,
        CancellationToken cancellationToken)
        => Ok(new Response
        {
            Data = await mediator.Send(new CreateCommentVoteCommand(command), cancellationToken)
        });

    [HttpPut("update")]
    [ProducesResponseType(typeof(CommentVoteResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(UpdateCommentVoteCommand command,
        CancellationToken cancellationToken)
        => Ok(new Response
        {
            Data = await mediator.Send(new UpdateCommentVoteCommand(command), cancellationToken)
        });

    [HttpDelete("delete/{id:long}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
        => Ok(new Response
        {
            Data = await mediator.Send(new DeleteCommentVoteCommand(id), cancellationToken)
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
    public async Task<IActionResult> GetForApplication(CancellationToken cancellationToken)
        => Ok(new Response
        {
            Data = await mediator.Send(new GetAllCommentVotesQuery(), cancellationToken)
        });
}