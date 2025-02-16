namespace Application.Topics.Commands.DeleteTopic;

public record DeleteTopicCommand(Guid Id, CancellationToken CancellationToken) 
    : ICommand<DeleteTopicResult>;