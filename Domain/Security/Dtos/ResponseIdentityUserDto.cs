namespace Domain.Security.Dtos;

public record ResponseIdentityUserDto(
    string UserName,
    string Email,
    string JwtToken);