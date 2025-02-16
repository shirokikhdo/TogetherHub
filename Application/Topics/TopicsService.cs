using Application.Data.DataBaseContext;
using Application.Dtos;
using Application.Exceptions;
using Application.Extensions;
using Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace Application.Topics;

public class TopicsService : ITopicsService
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ILogger<TopicsService> _logger;

    public TopicsService(
        IApplicationDbContext dbContext,
        ILogger<TopicsService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<List<ResponseTopicDto>> GetTopicsAsync()
    {
        var topics = await _dbContext.Topics
            .AsNoTracking()
            .ToListAsync();

        return topics.ToResponseTopicDtoList();
    }

    public async Task<ResponseTopicDto> GetTopicAsync(Guid id)
    {
        var topicId = TopicId.Of(id);
        var topic = await _dbContext.Topics.FindAsync([topicId]);

        if (topic is null)
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

    public Task<ResponseTopicDto> UpdateTopicAsync(Guid id, UpdateTopicDto requestTopicDto)
    {
        throw new NotImplementedException();
    }

    public Task DeleteTopicAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}