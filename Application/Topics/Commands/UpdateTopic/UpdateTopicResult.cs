namespace Application.Topics.Commands.UpdateTopic;

/// <summary>
/// Результат обновления темы.
/// </summary>
/// <param name="ResponseTopicDto">DTO обновленной темы, содержащий информацию о теме.</param>
public record UpdateTopicResult(ResponseTopicDto ResponseTopicDto);