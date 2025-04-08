namespace Domain.Exceptions;

/// <summary>
/// Представляет исключение доменной логики.
/// </summary>
public class DomainException : Exception
{
    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="DomainException"/> 
    /// с указанным сообщением об ошибке.
    /// </summary>
    /// <param name="message">Сообщение, описывающее ошибку.</param>
    public DomainException(string message) 
        : base($"Domain exception: ({message}).")
    {
        
    }
}