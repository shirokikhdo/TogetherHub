using Application.Security.Commands.LoginUser;
using Application.Security.Commands.RegisterUser;
using Application.Security.Services;

namespace Api.Controllers;

[AllowAnonymous]
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<CustomIdentityUser> _userManager;
    private readonly IJwtSecurityService _jwtSecurityService;
    private readonly IMediator _mediator;

    public AuthController(
        UserManager<CustomIdentityUser> userManager, 
        IJwtSecurityService jwtSecurityService,
        IMediator mediator)
    {
        _userManager = userManager;
        _jwtSecurityService = jwtSecurityService;
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