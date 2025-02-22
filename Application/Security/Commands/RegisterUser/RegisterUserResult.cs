namespace Application.Security.Commands.RegisterUser;

/// <summary>
/// Результат регистрации пользователя.
/// </summary>
/// <param name="User">Информация о зарегистрированном пользователе, включая его данные и токен.</param>
public record RegisterUserResult(ResponseIdentityUserDto User);