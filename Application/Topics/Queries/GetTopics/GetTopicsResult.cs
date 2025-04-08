namespace Application.Topics.Queries.GetTopics;

/// <summary>
/// Результат запроса для получения списка тем.
/// </summary>
/// <param name="Topics">Список тем, представленных в виде объектов <see cref="ResponseTopicDto"/>.</param>
public record GetTopicsResult(List<ResponseTopicDto> Topics);