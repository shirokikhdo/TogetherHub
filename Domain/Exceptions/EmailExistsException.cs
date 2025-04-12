namespace Domain.Exceptions;

/// <summary>
/// Исключение, возникающее, когда пользователь с указанным адресом электронной почты уже существует.
/// </summary>
public class EmailExistsException : UserExistsException
{
    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="EmailExistsException"/> 
    /// с указанным адресом электронной почты.
    /// </summary>
    /// <param name="email">Адрес электронной почты, который уже существует.</param>
    public EmailExistsException(string email) 
        : base($"Пользователь с Email {email} уже существует")
    {
        
    }
}