namespace Application.Topics.Commands.CreateTopic;

public class CreateTopicHandler : ICommandHandler<CreateTopicCommand, CreateTopicResult>
{
    private readonly IApplicationDbContext _dbContext;

    public CreateTopicHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CreateTopicResult> Handle(CreateTopicCommand request, CancellationToken cancellationToken)
    {
        var id = Guid.NewGuid();
        var topicId = TopicId.Of(id);
        var topic = request.RequestTopicDto.ToTopic(topicId);

        await _dbContext.Topics.AddAsync(topic, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new CreateTopicResult(topic.ToResponseTopicDto());
    }
}