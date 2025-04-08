namespace Application.Exceptions;

/// <summary>
/// Исключение, возникающее при ошибке создания пользователя.
/// </summary>
public class CreatingUserException : Exception
{
    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="CreatingUserException"/> 
    /// с указанным сообщением об ошибке.
    /// </summary>
    /// <param name="message">Сообщение, описывающее ошибку.</param>
    public CreatingUserException(string message) 
        : base(message)
    {
        
    }

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="CreatingUserException"/> 
    /// с информацией о пользователе и пароле, вызвавших ошибку.
    /// </summary>
    /// <param name="user">Пользователь, для которого произошла ошибка.</param>
    /// <param name="password">Пароль, связанный с пользователем.</param>
    public CreatingUserException(CustomIdentityUser user, string password)
        : base($"При создании пользователя произошла ошибка. " +
               $"{nameof(user.Email)}: {user.Email}; " +
               $"{nameof(user.UserName)}: " +
               $"{user.UserName}; Password: {password}")
    {

    }
}