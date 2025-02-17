using System.Security.Claims;
using System.Text;
using Domain.Security;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace Api.Security.Services;

public class JwtSecurityService : IJwtSecurityService
{
    private readonly IConfiguration _configuration;

    public JwtSecurityService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

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