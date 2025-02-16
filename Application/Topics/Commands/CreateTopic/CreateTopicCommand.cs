namespace Application.Topics.Commands.CreateTopic;

public record CreateTopicCommand(CreateTopicDto RequestTopicDto) 
    : ICommand<CreateTopicResult>;