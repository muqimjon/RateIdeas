using RateIdeas.Application.Comments.Commands;
using RateIdeas.Application.Comments.DTOs;
using RateIdeas.Application.Comments.Queries;

namespace RateIdeas.WebApi.Controllers.Comments;

public class CommentsController(IMediator mediator) : BaseController
{
    [HttpPost("create")]
    [ProducesResponseType(typeof(CommentResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create(CreateCommentCommand command,
        CancellationToken cancellationToken)
        => Ok(new Response
        {
            Data = await mediator.Send(new CreateCommentCommand(command), cancellationToken)
        });

    [HttpPut("update")]
    [ProducesResponseType(typeof(CommentResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(UpdateCommentCommand command,
        CancellationToken cancellationToken)
        => Ok(new Response
        {
            Data = await mediator.Send(new UpdateCommentCommand(command), cancellationToken)
        });

    [HttpDelete("delete/{id:long}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
        => Ok(new Response
        {
            Data = await mediator.Send(new DeleteCommentCommand(id), cancellationToken)
        });

    [HttpGet("get/{savedIdeaId:long}")]
    [ProducesResponseType(typeof(CommentResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(long savedIdeaId, CancellationToken cancellationToken)
        => Ok(new Response
        {
            Data = await mediator.Send(new GetCommentQuery(savedIdeaId), cancellationToken)
        });

    [HttpGet("get-all")]
    [ProducesResponseType(typeof(IEnumerable<CommentResultDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetForApplication([FromQuery] GetAllCommentsQuery query,
        CancellationToken cancellationToken)
        => Ok(new Response
        {
            Data = await mediator.Send(new GetAllCommentsQuery(query), cancellationToken)
        });
}