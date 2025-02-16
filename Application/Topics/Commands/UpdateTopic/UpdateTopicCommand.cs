namespace Application.Topics.Commands.UpdateTopic;

public record UpdateTopicCommand(Guid Id, UpdateTopicDto RequestTopicDto, CancellationToken CancellationToken)
    : ICommand<UpdateTopicResult>;