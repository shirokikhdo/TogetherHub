namespace Application.Security.Commands.RegisterUser;

/// <summary>
/// Обработчик команды регистрации нового пользователя.
/// </summary>
public class RegisterUserHandler : ICommandHandler<RegisterUserCommand, RegisterUserResult>
{
    private readonly UserManager<CustomIdentityUser> _userManager;
    private readonly IJwtSecurityService _jwtSecurityService;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="RegisterUserHandler"/>.
    /// </summary>
    /// <param name="userManager">Менеджер пользователей для управления учетными записями.</param>
    /// <param name="jwtSecurityService">Сервис для работы с JWT-токенами.</param>
    public RegisterUserHandler(
        UserManager<CustomIdentityUser> userManager, 
        IJwtSecurityService jwtSecurityService)
    {
        _userManager = userManager;
        _jwtSecurityService = jwtSecurityService;
    }

    /// <summary>
    /// Обрабатывает команду регистрации пользователя.
    /// </summary>
    /// <param name="request">Команда регистрации пользователя, содержащая данные нового пользователя.</param>
    /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
    /// <returns>Результат регистрации пользователя, содержащий информацию о созданном пользователе и токене.</returns>
    /// <exception cref="EmailExistsException">Вызывается, если email уже существует в системе.</exception>
    /// <exception cref="UserNameExistsException">Вызывается, если имя пользователя уже существует в системе.</exception>
    /// <exception cref="CreatingUserException">Вызывается, если не удалось создать учетную запись пользователя.</exception>
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