namespace Application.Security.Commands.LoginUser;

public record LoginUserCommand(LoginIdentityUserDto LoginIdentityUserDto) 
    : ICommand<LoginUserResult>;