namespace Application.Exceptions;

public class UserNameExistsException : UserExistsException
{
    public UserNameExistsException(string userName)
        : base($"Пользователь с UserName {userName} уже существует")
    {

    }
}