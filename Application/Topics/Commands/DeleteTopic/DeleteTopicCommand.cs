namespace Application.Topics.Commands.DeleteTopic;

/// <summary>
/// Команда для удаления темы.
/// </summary>
/// <param name="Id">Уникальный идентификатор темы, которую необходимо удалить.</param>
public record DeleteTopicCommand(Guid Id) 
    : ICommand<DeleteTopicResult>;