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
    public async Task<IResult> CreateTopic(
        [FromBody] CreateTopicDto createTopicDto,
        CancellationToken cancellationToken)
    {
        var command = new CreateTopicCommand(createTopicDto, cancellationToken);
        var result = await _mediator.Send(command, cancellationToken);
        return Results.Created($"/topics/{result.Topic.Id}", result.Topic);
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
    public async Task<IResult> DeleteTopic(
        Guid id,
        CancellationToken cancellationToken)
    {
        var command = new DeleteTopicCommand(id, cancellationToken);
        var result = await _mediator.Send(command, cancellationToken);
        return Results.Ok(result);
    }
}