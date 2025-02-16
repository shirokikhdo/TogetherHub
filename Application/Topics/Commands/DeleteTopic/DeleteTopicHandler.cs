namespace Application.Topics.Commands.DeleteTopic;

public class DeleteTopicHandler : ICommandHandler<DeleteTopicCommand, DeleteTopicResult>
{
    private readonly IApplicationDbContext _dbContext;

    public DeleteTopicHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<DeleteTopicResult> Handle(DeleteTopicCommand request, CancellationToken cancellationToken)
    {
        var id = request.Id;
        var topicId = TopicId.Of(id);
        var topic = await _dbContext.Topics.FindAsync([topicId], cancellationToken);

        if (topic is null || topic.IsDeleted)
            throw new TopicNotFoundException(id);

        topic.IsDeleted = true;
        topic.DeletedAt = DateTime.UtcNow;

        var result = await _dbContext.SaveChangesAsync(cancellationToken);
        return new DeleteTopicResult(result > 0);
    }
}