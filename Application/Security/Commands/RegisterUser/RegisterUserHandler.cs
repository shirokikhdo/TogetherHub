using Application.Security.Services;
using Domain.Security.Dtos;
using Domain.Security;
using Microsoft.AspNetCore.Identity;

namespace Application.Security.Commands.RegisterUser;

public class RegisterUserHandler : ICommandHandler<RegisterUserCommand, RegisterUserResult>
{
    private readonly UserManager<CustomIdentityUser> _userManager;
    private readonly IJwtSecurityService _jwtSecurityService;

    public RegisterUserHandler(
        UserManager<CustomIdentityUser> userManager, 
        IJwtSecurityService jwtSecurityService)
    {
        _userManager = userManager;
        _jwtSecurityService = jwtSecurityService;
    }

    public async Task<RegisterUserResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var dto = request.RegisterIdentityUserDto;

        if (await _userManager.Users.AnyAsync(x => 
                x.Email == dto.Email, cancellationToken))
            return null;

        if (await _userManager.Users.AnyAsync(x => 
                x.UserName == dto.UserName, cancellationToken))
            return null;

        var user = new CustomIdentityUser
        {
            FullName = dto.FullName,
            Email = dto.Email,
            UserName = dto.UserName,
            About = string.Empty
        };

        var result = await _userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded)
            return null;

        var token = _jwtSecurityService.CreateToken(user);
        var response = new ResponseIdentityUserDto(
            user.UserName,
            user.Email,
            token);

        return new RegisterUserResult(response);
    }
}