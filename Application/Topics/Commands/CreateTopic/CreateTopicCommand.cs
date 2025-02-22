namespace Application.Topics.Commands.CreateTopic;

/// <summary>
/// Команда для создания новой темы.
/// </summary>
/// <param name="RequestTopicDto">Объект данных, содержащий информацию о создаваемой теме.</param>
public record CreateTopicCommand(CreateTopicDto RequestTopicDto) 
    : ICommand<CreateTopicResult>;