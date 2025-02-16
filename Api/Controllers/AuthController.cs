using Domain.Security;
using Domain.Security.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<CustomIdentityUser> _userManager;

    public AuthController(UserManager<CustomIdentityUser> userManager)
    {
        _userManager = userManager;
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
        
        var response = new ResponseIdentityUserDto(
            user.UserName!, 
            user.Email!, 
            "jwt-token");

        return Results.Ok(response);
    }
}