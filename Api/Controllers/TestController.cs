using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    [Authorize]
    [HttpGet("test1")]
    public IResult Test1() => 
        Results.Ok("Test 1 - OK");

    [HttpGet("test2")]
    public IResult Test2() =>
        Results.Ok("Test 2 - OK");
}