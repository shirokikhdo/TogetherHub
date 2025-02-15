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
    public async Task<ActionResult<List<Topic>>> GetTopics()
    {
        var topics = await _topicsService.GetTopicsAsync();
        return Ok(topics);
    }
}