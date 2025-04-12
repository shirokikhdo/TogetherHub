namespace Application.Extensions;

/// <summary>
/// Расширения для работы с темами (Topic).
/// </summary>
public static class TopicExtensions
{
    /// <summary>
    /// Преобразует объект типа <see cref="Topic"/> в объект типа <see cref="ResponseTopicDto"/>.
    /// </summary>
    /// <param name="topic">Объект темы, который необходимо преобразовать.</param>
    /// <returns>Преобразованный объект типа <see cref="ResponseTopicDto"/>.</returns>
    public static ResponseTopicDto ToResponseTopicDto(this Topic topic) =>
        new ResponseTopicDto(
            topic.Id.Value,
            topic.Title,
            topic.Summary,
            topic.TopicType,
            new LocationDto(topic.Location.City, topic.Location.Street),
            topic.EventStart,
            topic.Users);

    /// <summary>
    /// Преобразует список объектов типа <see cref="Topic"/> в список объектов типа <see cref="ResponseTopicDto"/>.
    /// </summary>
    /// <param name="topics">Список тем, который необходимо преобразовать.</param>
    /// <returns>Список преобразованных объектов типа <see cref="ResponseTopicDto"/>.</returns>
    public static List<ResponseTopicDto> ToResponseTopicDtoList(this List<Topic> topics) =>
        topics.Select(x=>x.ToResponseTopicDto()).ToList();

    /// <summary>
    /// Преобразует объект типа <see cref="CreateTopicDto"/> в объект типа <see cref="Topic"/>.
    /// </summary>
    /// <param name="dto">Объект, содержащий данные для создания темы.</param>
    /// <param name="id">Идентификатор создаваемой темы.</param>
    /// <returns>Созданный объект типа <see cref="Topic"/>.</returns>
    public static Topic ToTopic(this CreateTopicDto dto, TopicId id) =>
        new Topic
        {
            Id = id,
            Title = dto.Title,
            Summary = dto.Summary,
            TopicType = dto.TopicType,
            Location = Location.Of(dto.Location.City, dto.Location.Street),
            EventStart = dto.EventStart,
        };
}