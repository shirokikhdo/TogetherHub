namespace Api.Controllers;

[AllowAnonymous]
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    public async Task<IResult> Login(
        [FromBody] LoginIdentityUserDto dto,
        CancellationToken cancellationToken)
    {
        var command = new LoginUserCommand(dto, cancellationToken);
        var result = await _mediator.Send(command, cancellationToken);

        return Results.Ok(result);
    }

    [HttpPost("register")]
    public async Task<IResult> Register(
        [FromBody] RegisterIdentityUserDto dto,
        CancellationToken cancellationToken)
    {
        var command = new RegisterUserCommand(dto, cancellationToken);
        var result = await _mediator.Send(command, cancellationToken);

        return Results.Ok(result);
    }
}