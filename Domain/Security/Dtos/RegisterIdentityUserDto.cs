namespace Domain.Security.Dtos;

public record RegisterIdentityUserDto(
    string FullName, 
    string UserName, 
    string Email, 
    string Password);