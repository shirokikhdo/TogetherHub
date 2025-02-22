namespace Application.Topics.Queries.GetTopic;

/// <summary>
/// Запрос на получение информации о теме.
/// </summary>
/// <param name="Id">Уникальный идентификатор темы.</param>
public record GetTopicQuery(Guid Id)
    : IQuery<GetTopicResult>;