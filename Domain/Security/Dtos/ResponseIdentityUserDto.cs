namespace Domain.Security.Dtos;

/// <summary>
/// Представляет ответ для пользователя после успешной аутентификации,
/// включая имя пользователя, адрес электронной почты и токен JWT.
/// </summary>
public record ResponseIdentityUserDto(
    string UserName,
    string Email,
    string JwtToken);