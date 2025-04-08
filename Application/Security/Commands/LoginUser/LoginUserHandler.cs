namespace Application.Security.Commands.LoginUser;

/// <summary>
/// Обработчик команды входа пользователя в систему.
/// </summary>
public class LoginUserHandler : ICommandHandler<LoginUserCommand, LoginUserResult>
{
    private readonly UserManager<CustomIdentityUser> _userManager;
    private readonly IJwtSecurityService _jwtSecurityService;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="LoginUserHandler"/>.
    /// </summary>
    /// <param name="userManager">Менеджер пользователей для работы с пользователями.</param>
    /// <param name="jwtSecurityService">Сервис для создания JWT-токенов.</param>
    public LoginUserHandler(
        UserManager<CustomIdentityUser> userManager,
        IJwtSecurityService jwtSecurityService)
    {
        _userManager = userManager;
        _jwtSecurityService = jwtSecurityService;
    }

    /// <summary>
    /// Обрабатывает команду входа пользователя.
    /// </summary>
    /// <param name="request">Команда входа пользователя, содержащая данные для аутентификации.</param>
    /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
    /// <returns>Результат входа пользователя, содержащий информацию о пользователе и токен.</returns>
    /// <exception cref="UserNotFoundException">Выбрасывается, если пользователь с указанным адресом электронной почты не найден.</exception>
    /// <exception cref="WrongPasswordException">Выбрасывается, если пароль неверен.</exception>
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