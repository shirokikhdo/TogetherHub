namespace Application.Topics.Commands.DeleteTopic;

/// <summary>
/// Результат выполнения операции удаления темы.
/// </summary>
/// <param name="IsSuccess">Указывает, была ли операция удаления успешной.</param>
public record DeleteTopicResult(bool IsSuccess);