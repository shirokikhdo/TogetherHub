namespace Application.Security.Commands.LoginUser;

/// <summary>
/// Результат входа пользователя в систему.
/// </summary>
/// <param name="User">Информация о пользователе, включая токен аутентификации.</param>
public record LoginUserResult(ResponseIdentityUserDto User);