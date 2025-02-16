namespace Application.Topics.Deprecated;

[Obsolete("Данный сервис устарел", true)]
public class TopicsService : ITopicsService
{
    private readonly IApplicationDbContext _dbContext;

    public TopicsService(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<ResponseTopicDto>> GetTopicsAsync(CancellationToken cancellationToken)
    {
        var topics = await _dbContext.Topics
            .AsNoTracking()
            .Where(x => !x.IsDeleted)
            .ToListAsync(cancellationToken);

        return topics.ToResponseTopicDtoList();
    }

    public async Task<ResponseTopicDto> GetTopicAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        var topicId = TopicId.Of(id);
        var topic = await _dbContext.Topics.FindAsync([topicId], cancellationToken);

        if (topic is null || topic.IsDeleted)
            throw new TopicNotFoundException(id);

        return topic.ToResponseTopicDto();
    }

    public async Task<ResponseTopicDto> CreateTopicAsync(
        CreateTopicDto requestTopicDto,
        CancellationToken cancellationToken)
    {
        var id = Guid.NewGuid();
        var topicId = TopicId.Of(id);
        var topic = requestTopicDto.ToTopic(topicId);

        await _dbContext.Topics.AddAsync(topic, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return topic.ToResponseTopicDto();
    }

    public async Task<ResponseTopicDto> UpdateTopicAsync(
        Guid id,
        UpdateTopicDto requestTopicDto,
        CancellationToken cancellationToken)
    {
        var topicId = TopicId.Of(id);
        var topic = await _dbContext.Topics.FindAsync([topicId], cancellationToken);

        if (topic is null || topic.IsDeleted)
            throw new TopicNotFoundException(id);

        if (!string.IsNullOrEmpty(requestTopicDto.Title))
            topic.Title = requestTopicDto.Title;

        if (!string.IsNullOrEmpty(requestTopicDto.Summary))
            topic.Summary = requestTopicDto.Summary;

        if (!string.IsNullOrEmpty(requestTopicDto.TopicType))
            topic.TopicType = requestTopicDto.TopicType;

        topic.Location = Location.Of(
            requestTopicDto.Location.City,
            requestTopicDto.Location.Street);

        topic.EventStart = requestTopicDto.EventStart;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return topic.ToResponseTopicDto();
    }

    public async Task DeleteTopicAsync(Guid id, CancellationToken cancellationToken)
    {
        var topicId = TopicId.Of(id);
        var topic = await _dbContext.Topics.FindAsync([topicId], cancellationToken);

        if (topic is null || topic.IsDeleted)
            throw new TopicNotFoundException(id);

        //_dbContext.Topics.Remove(topic);
        topic.IsDeleted = true;
        topic.DeletedAt = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}