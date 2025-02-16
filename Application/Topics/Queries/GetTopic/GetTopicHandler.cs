namespace Application.Topics.Queries.GetTopic;

public class GetTopicHandler : IQueryHandler<GetTopicQuery, GetTopicResult>
{
    private readonly IApplicationDbContext _dbContext;

    public GetTopicHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetTopicResult> Handle(GetTopicQuery request, CancellationToken cancellationToken)
    {
        var id = request.Id;
        var topicId = TopicId.Of(id);
        var topic = await _dbContext.Topics
            .FindAsync([topicId], cancellationToken);

        if (topic is null || topic.IsDeleted)
            throw new TopicNotFoundException(id);

        var result = new GetTopicResult(topic.ToResponseTopicDto());
        return result;
    }
}