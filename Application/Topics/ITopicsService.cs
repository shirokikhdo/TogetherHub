using Application.Dtos;

namespace Application.Topics;

public interface ITopicsService
{
    Task<List<ResponseTopicDto>> GetTopicsAsync();
    Task<ResponseTopicDto> GetTopicAsync(Guid id);
    Task<ResponseTopicDto> CreateTopicAsync(CreateTopicDto requestTopicDto);
    Task<ResponseTopicDto> UpdateTopicAsync(Guid id, UpdateTopicDto requestTopicDto);
    Task DeleteTopicAsync(Guid id);
}