using Application.Data.DataBaseContext;
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

    public async Task<List<Topic>> GetTopicsAsync() => 
        await _dbContext.Topics
            .AsNoTracking()
            .ToListAsync();

    public Task<Topic> GetTopicAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Topic> CreateTopicAsync(Topic topicRequestDto)
    {
        throw new NotImplementedException();
    }

    public Task<Topic> UpdateTopicAsync(Guid id, Topic topicRequestDto)
    {
        throw new NotImplementedException();
    }

    public Task DeleteTopicAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}