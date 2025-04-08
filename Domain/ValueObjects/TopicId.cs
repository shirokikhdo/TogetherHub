namespace Domain.ValueObjects;

/// <summary>
/// Представляет уникальный идентификатор темы.
/// </summary>
public record TopicId
{
    /// <summary>
    /// Получает значение идентификатора темы.
    /// </summary>
    public Guid Value { get; }

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="TopicId"/> с заданным значением.
    /// </summary>
    /// <param name="value">Уникальный идентификатор темы.</param>
    private TopicId(Guid value)
    {
        Value = value;
    }

    /// <summary>
    /// Создает новый экземпляр класса <see cref="TopicId"/> с заданным значением.
    /// </summary>
    /// <param name="value">Уникальный идентификатор темы.</param>
    /// <returns>Созданный экземпляр <see cref="TopicId"/>.</returns>
    /// <exception cref="DomainException">Выбрасывается, если <paramref name="value"/> является пустым идентификатором.</exception>
    public static TopicId Of(Guid value)
    {
        if (value == Guid.Empty)
            throw new DomainException("TopicId не может быть пустым");

        return new TopicId(value);
    }
}