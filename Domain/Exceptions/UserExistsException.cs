namespace Domain.Exceptions;

/// <summary>
/// Исключение, возникающее, когда пользователь уже существует.
/// </summary>
public class UserExistsException : DomainException
{
    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="UserExistsException"/> 
    /// с указанным сообщением об ошибке.
    /// </summary>
    /// <param name="message">Сообщение, описывающее ошибку.</param>
    public UserExistsException(string message)
        : base(message)
    {
        
    }
}