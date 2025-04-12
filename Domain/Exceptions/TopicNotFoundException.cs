namespace Domain.Exceptions;

/// <summary>
/// Исключение, возникающее, когда запрашиваемая тема не найдена.
/// </summary>
public class TopicNotFoundException : NotFoundException
{
    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="TopicNotFoundException"/> 
    /// с указанным сообщением об ошибке.
    /// </summary>
    /// <param name="message">Сообщение, описывающее ошибку.</param>
    public TopicNotFoundException(string message) 
        : base(message)
    {

    }

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="TopicNotFoundException"/> 
    /// с сообщением, содержащим идентификатор темы, которая не была найдена.
    /// </summary>
    /// <param name="id">Идентификатор темы.</param>
    public TopicNotFoundException(Guid id)
        : base($"Topic с id {id} не найден")
    {

    }
}