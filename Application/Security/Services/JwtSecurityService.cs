namespace Application.Security.Services;

/// <summary>
/// Реализация сервиса безопасности для работы с JWT (JSON Web Token).
/// </summary>
public class JwtSecurityService : IJwtSecurityService
{
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="JwtSecurityService"/> 
    /// с указанной конфигурацией.
    /// </summary>
    /// <param name="configuration">Объект конфигурации, содержащий настройки аутентификации.</param>
    public JwtSecurityService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <summary>
    /// Создает JWT токен для указанного пользователя.
    /// </summary>
    /// <param name="user">Пользователь, для которого создается токен.</param>
    /// <returns>Сгенерированный JWT токен в виде строки.</returns>
    public string CreateToken(CustomIdentityUser user)
    {
        var secretKey = _configuration["AuthSettings:SecretKey"]!;

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Name, user.UserName!),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!),
            new Claim("is_premium", "true")
        };

        var bytes = Encoding.UTF8.GetBytes(secretKey);
        var key = new SymmetricSecurityKey(bytes);
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var tokenHandler = new JsonWebTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            SigningCredentials = credentials,
            Subject = new ClaimsIdentity(claims),
            IssuedAt = DateTime.UtcNow,
            NotBefore = DateTime.UtcNow.AddMinutes(0),
            Expires = DateTime.UtcNow.AddMinutes(10)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return token;
    }
}