namespace Application.Security.Commands.LoginUser;

public class LoginUserHandler : ICommandHandler<LoginUserCommand, LoginUserResult>
{
    public Task<LoginUserResult> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}