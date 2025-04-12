namespace Domain.Exceptions;

/// <summary>
/// Исключение, возникающее, когда пользователь не найден.
/// </summary>
public class UserNotFoundException : NotFoundException
{
    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="UserNotFoundException"/> 
    /// с указанным сообщением об ошибке.
    /// </summary>
    /// <param name="message">Сообщение, описывающее, какой пользователь не найден.</param>
    public UserNotFoundException(string message) 
        : base($"Пользователь {message} не найден")
    {

    }
}