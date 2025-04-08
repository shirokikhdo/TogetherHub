namespace Application.Topics.Commands.CreateTopic;

/// <summary>
/// Результат создания темы.
/// </summary>
/// <param name="Topic">Объект, представляющий созданную тему в формате <see cref="ResponseTopicDto"/>.</param>
public record CreateTopicResult(ResponseTopicDto Topic);