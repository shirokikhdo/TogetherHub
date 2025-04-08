namespace Application.Security.Commands.RegisterUser;

/// <summary>
/// Команда для регистрации нового пользователя.
/// </summary>
/// <param name="RegisterIdentityUserDto">Данные пользователя, необходимые для регистрации.</param>
public record RegisterUserCommand(RegisterIdentityUserDto RegisterIdentityUserDto) 
    : ICommand<RegisterUserResult>;