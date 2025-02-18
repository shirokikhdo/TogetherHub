namespace Application.Security.Commands.LoginUser;

public record LoginUserCommand(LoginIdentityUserDto LoginIdentityUserDto, CancellationToken CancellationToken) 
    : ICommand<LoginUserResult>;