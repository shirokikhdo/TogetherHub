namespace Domain.Models;

/// <summary>
/// Представляет тему, которая содержит информацию о событии.
/// </summary>
public class Topic : Entity<TopicId>
{
    /// <summary>
    /// Получает или задает заголовок темы.
    /// </summary>
    public string Title { get; set; } = default!;

    /// <summary>
    /// Получает или задает дату и время начала события.
    /// </summary>
    public DateTime? EventStart { get; set; } = default!;

    /// <summary>
    /// Получает или задает краткое описание темы.
    /// </summary>
    public string Summary { get; set; } = default!;

    /// <summary>
    /// Получает или задает тип темы.
    /// </summary>
    public string TopicType { get; set; } = default!;

    /// <summary>
    /// Получает или задает местоположение события.
    /// </summary>
    public Location Location { get; set; } = default!;

    public bool IsVoided { get; set; } = default!;

    public List<Relationship> Users { get; set; } = new List<Relationship>();

    public List<Comment> Comments { get; set; } = new List<Comment>();

    /// <summary>
    /// Создает новый экземпляр темы с заданными параметрами.
    /// </summary>
    /// <param name="topicId">Идентификатор темы.</param>
    /// <param name="title">Заголовок темы.</param>
    /// <param name="eventStart">Дата и время начала события.</param>
    /// <param name="summary">Краткое описание темы.</param>
    /// <param name="topicType">Тип темы.</param>
    /// <param name="location">Местоположение события.</param>
    /// <returns>Созданный экземпляр темы.</returns>
    /// <exception cref="ArgumentException">Если заголовок, описание или тип темы пусты.</exception>
    public static Topic Create(
        TopicId topicId,
        string title,
        DateTime eventStart,
        string summary,
        string topicType,
        Location location)
    {
        ArgumentException.ThrowIfNullOrEmpty(title);
        ArgumentException.ThrowIfNullOrEmpty(summary);
        ArgumentException.ThrowIfNullOrEmpty(topicType);

        var topic = new Topic
        {
            Id = topicId,
            Title = title,
            EventStart = eventStart,
            Summary = summary,
            TopicType = topicType,
            Location = location
        };
        
        return topic;
    }
}