namespace Application.Security.Commands.LoginUser;

/// <summary>
/// Команда для входа пользователя в систему.
/// </summary>
/// <param name="LoginIdentityUserDto">Объект, содержащий данные для входа пользователя.</param>
public record LoginUserCommand(LoginIdentityUserDto LoginIdentityUserDto) 
    : ICommand<LoginUserResult>;