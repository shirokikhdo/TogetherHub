using Application.Data.DataBaseContext;
using Application.Dtos;
using Application.Extensions;
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

    public Task<ResponseTopicDto> GetTopicAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseTopicDto> CreateTopicAsync(CreateTopicDto requestTopicDto)
    {
        throw new NotImplementedException();
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