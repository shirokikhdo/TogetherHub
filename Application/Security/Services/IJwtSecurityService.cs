namespace Application.Security.Services;

/// <summary>
/// Интерфейс для работы с JWT (JSON Web Token) безопасностью.
/// </summary>
public interface IJwtSecurityService
{
    /// <summary>
    /// Создает JWT токен для указанного пользователя.
    /// </summary>
    /// <param name="user">Пользователь, для которого создается токен.</param>
    /// <returns>Сгенерированный JWT токен в виде строки.</returns>
    string CreateToken(CustomIdentityUser  user);
}