namespace Application.Security.Commands.RegisterUser;

public class RegisterUserHandler : ICommandHandler<RegisterUserCommand, RegisterUserResult>
{
    public Task<RegisterUserResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}