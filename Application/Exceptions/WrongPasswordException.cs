namespace Application.Exceptions;

public class WrongPasswordException : Exception
{
    public WrongPasswordException(string user, string password)
        : base($"Для пользователя {user} не подходит пароль {password}")
    {
        
    }
}