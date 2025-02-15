using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class TopicsController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Hello() => 
        await Task.FromResult(Ok(new { text = "hello" }));
}