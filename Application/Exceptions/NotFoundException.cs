﻿namespace Application.Exceptions;

/// <summary>
/// Исключение, возникающее, когда запрашиваемый ресурс не найден.
/// </summary>
public class NotFoundException : Exception
{
    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="NotFoundException"/> 
    /// с указанным сообщением об ошибке.
    /// </summary>
    /// <param name="message">Сообщение, описывающее ошибку.</param>
    public NotFoundException(string message) 
        : base(message)
    {
        
    }
}