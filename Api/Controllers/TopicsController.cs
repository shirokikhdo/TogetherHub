using Application.Topics.Queries.GetTopics;
using MediatR;
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
    public async Task<ActionResult<List<ResponseTopicDto>>> GetTopics(CancellationToken cancellationToken)
    {
        var query = new GetTopicsQuery(cancellationToken);
        var result =  await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ResponseTopicDto>> GetTopic(Guid id, CancellationToken cancellationToken)
    {
        return Ok();
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