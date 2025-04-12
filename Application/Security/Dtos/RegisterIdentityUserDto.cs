namespace Application.Security.Dtos;

/// <summary>
/// Представляет данные для регистрации пользователя, включая полное имя, имя пользователя, электронную почту и пароль.
/// </summary>
public record RegisterIdentityUserDto(
    string FullName, 
    string UserName, 
    string Email, 
    string Password);