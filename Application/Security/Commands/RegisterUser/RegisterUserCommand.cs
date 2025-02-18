namespace Application.Security.Commands.RegisterUser;

public record RegisterUserCommand(RegisterIdentityUserDto RegisterIdentityUserDto) 
    : ICommand<RegisterUserResult>;