namespace Application.Topics.Deprecated;

[Obsolete("Данный сервис устарел", true)]
public interface ITopicsService
{
    Task<List<ResponseTopicDto>> GetTopicsAsync(CancellationToken cancellationToken);
    Task<ResponseTopicDto> GetTopicAsync(Guid id, CancellationToken cancellationToken);
    Task<ResponseTopicDto> CreateTopicAsync(CreateTopicDto requestTopicDto, CancellationToken cancellationToken);
    Task<ResponseTopicDto> UpdateTopicAsync(Guid id, UpdateTopicDto requestTopicDto, CancellationToken cancellationToken);
    Task DeleteTopicAsync(Guid id, CancellationToken cancellationToken);
}