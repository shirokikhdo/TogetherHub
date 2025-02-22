namespace Api.Controllers;

/// <summary>
/// Контроллер для аутентификации пользователей.
/// Позволяет выполнять операции входа и регистрации пользователей.
/// </summary>
[AllowAnonymous]
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="AuthController"/>.
    /// </summary>
    /// <param name="mediator">Экземпляр медиатора для обработки команд.</param>
    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Выполняет вход пользователя в систему.
    /// </summary>
    /// <param name="dto">Объект данных для входа пользователя.</param>
    /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
    /// <returns>Результат выполнения операции входа.</returns>
    [HttpPost("login")]
    public async Task<IResult> Login(
        [FromBody] LoginIdentityUserDto dto,
        CancellationToken cancellationToken)
    {
        var command = new LoginUserCommand(dto);
        var result = await _mediator.Send(command, cancellationToken);

        return Results.Ok(result);
    }

    /// <summary>
    /// Регистрирует нового пользователя в системе.
    /// </summary>
    /// <param name="dto">Объект данных для регистрации пользователя.</param>
    /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
    /// <returns>Результат выполнения операции регистрации.</returns>
    [HttpPost("register")]
    public async Task<IResult> Register(
        [FromBody] RegisterIdentityUserDto dto,
        CancellationToken cancellationToken)
    {
        var command = new RegisterUserCommand(dto);
        var result = await _mediator.Send(command, cancellationToken);

        return Results.Ok(result);
    }
}