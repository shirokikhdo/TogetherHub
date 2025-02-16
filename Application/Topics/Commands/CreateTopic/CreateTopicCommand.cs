namespace Application.Topics.Commands.CreateTopic;

public record CreateTopicCommand(CreateTopicDto RequestTopicDto, CancellationToken CancellationToken) 
    : ICommand<CreateTopicResult>;