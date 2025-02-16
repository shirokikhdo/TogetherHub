using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TopicsController : ControllerBase
{
    private readonly ITopicsService _topicsService;

    public TopicsController(ITopicsService topicsService)
    {
        _topicsService = topicsService;
    }

    [HttpGet]
    public async Task<ActionResult<List<ResponseTopicDto>>> GetTopics(CancellationToken cancellationToken)
    {
        var topics = await _topicsService.GetTopicsAsync(cancellationToken);
        return Ok(topics);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ResponseTopicDto>> GetTopic(Guid id, CancellationToken cancellationToken)
    {
        var topic = await _topicsService.GetTopicAsync(id, cancellationToken);
        return Ok(topic);
    }

    [HttpPost]
    public async Task<ActionResult<ResponseTopicDto>> CreateTopic(
        [FromBody] CreateTopicDto createTopicDto,
        CancellationToken cancellationToken)
    {
        var topic = await _topicsService.CreateTopicAsync(createTopicDto, cancellationToken);
        return Ok(topic);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ResponseTopicDto>> UpdateTopic(
        Guid id,
        [FromBody] UpdateTopicDto updateTopicDto,
        CancellationToken cancellationToken)
    {
        var topic = await _topicsService.UpdateTopicAsync(id, updateTopicDto, cancellationToken);
        return Ok(topic);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteTopic(
        Guid id,
        CancellationToken cancellationToken)
    {
        await _topicsService.DeleteTopicAsync(id, cancellationToken);
        return Ok();
    }
}