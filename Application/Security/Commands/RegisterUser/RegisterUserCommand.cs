namespace Application.Security.Commands.RegisterUser;

public record RegisterUserCommand(RegisterIdentityUserDto RegisterIdentityUserDto, CancellationToken CancellationToken) 
    : ICommand<RegisterUserResult>;