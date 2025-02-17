using Domain.Security;

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
        var secretKey = _configuration["AuthSettings:SecretKey"];
        throw new NotImplementedException();
    }
}