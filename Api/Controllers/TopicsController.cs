using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TopicsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TopicsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IResult> GetTopics(CancellationToken cancellationToken)
    {
        var query = new GetTopicsQuery(cancellationToken);
        var result =  await _mediator.Send(query, cancellationToken);
        return Results.Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IResult> GetTopic(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetTopicQuery(id, cancellationToken);
        var result = await _mediator.Send(query, cancellationToken);
        return Results.Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<ResponseTopicDto>> CreateTopic(
        [FromBody] CreateTopicDto createTopicDto,
        CancellationToken cancellationToken)
    {
        return Ok();
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ResponseTopicDto>> UpdateTopic(
        Guid id,
        [FromBody] UpdateTopicDto updateTopicDto,
        CancellationToken cancellationToken)
    {
        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteTopic(
        Guid id,
        CancellationToken cancellationToken)
    {
        return Ok();
    }
}