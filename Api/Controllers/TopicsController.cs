using Application.Dtos;
using Application.Topics;
using Domain.Models;
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
    public async Task<ActionResult<List<ResponseTopicDto>>> GetTopics()
    {
        var topics = await _topicsService.GetTopicsAsync();
        return Ok(topics);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<List<ResponseTopicDto>>> GetTopic(Guid id)
    {
        var topic = await _topicsService.GetTopicAsync(id);
        return Ok(topic);
    }
}