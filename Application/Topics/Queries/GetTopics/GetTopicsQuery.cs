namespace Application.Topics.Queries.GetTopics;

/// <summary>
/// Запрос для получения списка тем.
/// </summary>
/// <remarks>
/// Этот класс реализует интерфейс <see cref="IQuery{TResult}"/>, 
/// который используется для передачи запроса на получение результата типа <see cref="GetTopicsResult"/>.
/// </remarks>
public record GetTopicsQuery 
    : IQuery<GetTopicsResult>;