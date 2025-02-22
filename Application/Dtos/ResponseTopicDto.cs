namespace Application.Dtos;

/// <summary>
/// DTO (Data Transfer Object) для представления темы ответа.
/// </summary>
/// <param name="Id">Уникальный идентификатор темы.</param>
/// <param name="Title">Название темы.</param>
/// <param name="Summary">Краткое описание темы.</param>
/// <param name="TopicType">Тип темы.</param>
/// <param name="Location">Местоположение, связанное с темой.</param>
/// <param name="EventStart">Дата и время начала события (может быть null).</param>
public record ResponseTopicDto(
    Guid Id,
    string Title,
    string Summary,
    string TopicType,
    LocationDto Location,
    DateTime? EventStart);