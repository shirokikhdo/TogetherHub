namespace Application.Exceptions;

public class CreatingUserException : Exception
{
    public CreatingUserException(string message) 
        : base(message)
    {
        
    }

    public CreatingUserException(CustomIdentityUser user, string password)
        : base($"При создании пользователя произошла ошибка. " +
               $"{nameof(user.Email)}: {user.Email}; " +
               $"{nameof(user.UserName)}: " +
               $"{user.UserName}; Password: {password}")
    {

    }
}