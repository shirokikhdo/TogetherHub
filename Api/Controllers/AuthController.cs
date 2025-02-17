using Api.Security.Services;
using Domain.Security;
using Domain.Security.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[AllowAnonymous]
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<CustomIdentityUser> _userManager;
    private readonly IJwtSecurityService _jwtSecurityService;

    public AuthController(
        UserManager<CustomIdentityUser> userManager, 
        IJwtSecurityService jwtSecurityService)
    {
        _userManager = userManager;
        _jwtSecurityService = jwtSecurityService;
    }

    [HttpPost("login")]
    public async Task<IResult> Login(
        [FromBody] LoginIdentityUserDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user is null)
            return Results.Unauthorized();

        var result = await _userManager.CheckPasswordAsync(user, dto.Password);
        if (!result) 
            return Results.Unauthorized();

        var token = _jwtSecurityService.CreateToken(user);
        var response = new ResponseIdentityUserDto(
            user.UserName!, 
            user.Email!, 
            token);

        return Results.Ok(response);
    }
}