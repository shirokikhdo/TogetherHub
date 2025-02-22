namespace Application.Security.Commands.LoginUser;

public class LoginUserHandler : ICommandHandler<LoginUserCommand, LoginUserResult>
{
    private readonly UserManager<CustomIdentityUser> _userManager;
    private readonly IJwtSecurityService _jwtSecurityService;

    public LoginUserHandler(
        UserManager<CustomIdentityUser> userManager,
        IJwtSecurityService jwtSecurityService)
    {
        _userManager = userManager;
        _jwtSecurityService = jwtSecurityService;
    }

    public async Task<LoginUserResult> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var dto = request.LoginIdentityUserDto;

        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user is null)
            throw new UserNotFoundException(dto.Email);

        var result = await _userManager.CheckPasswordAsync(user, dto.Password);
        if (!result)
            throw new WrongPasswordException(user.Email!, dto.Password);

        var token = _jwtSecurityService.CreateToken(user);
        var response = new ResponseIdentityUserDto(
            user.UserName!,
            user.Email!,
            token);

        return new LoginUserResult(response);
    }
}