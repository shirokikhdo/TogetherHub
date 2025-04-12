namespace Application.Security.Dtos;

/// <summary>
/// Представляет данные для входа пользователя, включая электронную почту и пароль.
/// </summary>
public record LoginIdentityUserDto(
    string Email, 
    string Password);