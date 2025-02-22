namespace Application.Topics.Commands.UpdateTopic;

/// <summary>
/// Команда для обновления темы.
/// </summary>
/// <param name="Id">Уникальный идентификатор темы, которую необходимо обновить.</param>
/// <param name="RequestTopicDto">Объект, содержащий данные для обновления темы.</param>
public record UpdateTopicCommand(Guid Id, UpdateTopicDto RequestTopicDto)
    : ICommand<UpdateTopicResult>;