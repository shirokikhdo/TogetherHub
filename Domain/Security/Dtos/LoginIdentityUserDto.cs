namespace Domain.Security.Dtos;

public record LoginIdentityUserDto(
    string Email, 
    string Password);