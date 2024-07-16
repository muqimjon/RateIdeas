using RateIdeas.Application.Categories.Queries;
using RateIdeas.Application.Categories.Commands;
using RateIdeas.Application.Categories.DTOs;

namespace RateIdeas.WebApi.Controllers.Categories;

public class CategoriesController(IMediator mediator) : BaseController
{
    [HttpPost("create")]
    [ProducesResponseType(typeof(CategoryResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create(CreateCategoryCommand command, CancellationToken cancellationToken)
        => Ok(new Response { Data = await mediator.Send(new CreateCategoryCommand(command), cancellationToken) });

    [HttpPut("update")]
    [ProducesResponseType(typeof(CategoryResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(UpdateCategoryCommand command, CancellationToken cancellationToken)
        => Ok(new Response { Data = await mediator.Send(new UpdateCategoryCommand(command), cancellationToken) });

    [HttpDelete("delete/{id:long}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
        => Ok(new Response { Data = await mediator.Send(new DeleteCategoryCommand(id), cancellationToken) });

    [HttpGet("get/{ideaId:long}")]
    [ProducesResponseType(typeof(CategoryResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(long ideaId, CancellationToken cancellationToken)
        => Ok(new Response { Data = await mediator.Send(new GetCategoryQuery(ideaId), cancellationToken) });

    [HttpGet("get-all")]
    [ProducesResponseType(typeof(IEnumerable<CategoryResultDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetForApplication(CancellationToken cancellationToken)
        => Ok(new Response { Data = await mediator.Send(new GetAllCategoriesQuery(), cancellationToken) });
}