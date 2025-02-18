using Application.Security.Commands.LoginUser;
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
        [FromBody] RegisterIdentityUserDto dto)
    {
        if (await _userManager.Users.AnyAsync(x => x.Email == dto.Email))
            return Results.BadRequest("Email занят");

        if (await _userManager.Users.AnyAsync(x => x.UserName == dto.UserName))
            return Results.BadRequest("UserName занят");

        var user = new CustomIdentityUser
        {
            FullName = dto.FullName,
            Email = dto.Email,
            UserName = dto.UserName,
            About = string.Empty
        };

        var result = await _userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded) 
            return Results.BadRequest(result.Errors);
        
        var token = _jwtSecurityService.CreateToken(user);
        var response = new ResponseIdentityUserDto(
            user.UserName, 
            user.Email, 
            token);

        return Results.Ok(response);
    }
}