namespace Application.Topics.Queries.GetTopic;

/// <summary>
/// Результат запроса на получение информации о теме.
/// </summary>
/// <param name="Topic">Информация о теме, представленная в виде объекта <see cref="ResponseTopicDto"/>.</param>
public record GetTopicResult(ResponseTopicDto Topic);