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
            throw new EmailExistsException(dto.Email);

        if (await _userManager.Users.AnyAsync(x =>
                x.UserName == dto.UserName, cancellationToken))
            throw new UserNameExistsException(dto.UserName);

        var user = new CustomIdentityUser
        {
            FullName = dto.FullName,
            Email = dto.Email,
            UserName = dto.UserName,
            About = string.Empty
        };

        var result = await _userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded)
            throw new CreatingUserException(user, dto.Password);

        var token = _jwtSecurityService.CreateToken(user);
        var response = new ResponseIdentityUserDto(
            user.UserName,
            user.Email,
            token);

        return new RegisterUserResult(response);
    }
}