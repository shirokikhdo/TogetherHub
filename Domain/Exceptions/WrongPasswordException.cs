namespace Domain.Exceptions;

/// <summary>
/// Исключение, возникающее, когда введённый пароль неверен для указанного пользователя.
/// </summary>
public class WrongPasswordException : DomainException
{
    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="WrongPasswordException"/> 
    /// с указанным именем пользователя и паролем.
    /// </summary>
    /// <param name="user">Имя пользователя, для которого введён неверный пароль.</param>
    /// <param name="password">Введённый пароль, который не подходит.</param>
    public WrongPasswordException(string user, string password)
        : base($"Для пользователя {user} не подходит пароль {password}")
    {
        
    }
}