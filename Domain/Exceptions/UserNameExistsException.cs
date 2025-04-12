namespace Domain.Exceptions;

/// <summary>
/// Исключение, возникающее, когда пользователь с указанным именем пользователя (UserName) уже существует.
/// </summary>
public class UserNameExistsException : UserExistsException
{
    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="UserNameExistsException"/> 
    /// с указанным именем пользователя.
    /// </summary>
    /// <param name="userName">Имя пользователя, которое уже существует.</param>
    public UserNameExistsException(string userName)
        : base($"Пользователь с UserName {userName} уже существует")
    {

    }
}