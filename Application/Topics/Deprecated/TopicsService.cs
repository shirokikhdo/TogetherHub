namespace Application.Topics.Deprecated;

/// <summary>
/// Реализация сервиса для работы с темами.
/// </summary>
/// <remarks>
/// Данный сервис устарел и не рекомендуется к использованию.
/// </remarks>
[Obsolete("Данный сервис устарел", true)]
public class TopicsService : ITopicsService
{
    private readonly IApplicationDbContext _dbContext;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="TopicsService"/>.
    /// </summary>
    /// <param name="dbContext">Контекст приложения для работы с базой данных.</param>
    public TopicsService(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Асинхронно получает список всех тем.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены для управления отменой операции.</param>
    /// <returns>Список DTO тем.</returns>
    public async Task<List<ResponseTopicDto>> GetTopicsAsync(CancellationToken cancellationToken)
    {
        var topics = await _dbContext.Topics
            .AsNoTracking()
            .Where(x => !x.IsDeleted)
            .ToListAsync(cancellationToken);

        return topics.ToResponseTopicDtoList();
    }

    /// <summary>
    /// Асинхронно получает информацию о конкретной теме по её идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор темы.</param>
    /// <param name="cancellationToken">Токен отмены для управления отменой операции.</param>
    /// <returns>DTO запрашиваемой темы.</returns>
    /// <exception cref="TopicNotFoundException">Выбрасывается, если тема не найдена или была удалена.</exception>
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

    /// <summary>
    /// Асинхронно создает новую тему.
    /// </summary>
    /// <param name="requestTopicDto">DTO новой темы, содержащий необходимые данные для создания.</param>
    /// <param name="cancellationToken">Токен отмены для управления отменой операции.</param>
    /// <returns>DTO созданной темы.</returns>
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

    /// <summary>
    /// Асинхронно обновляет существующую тему по её идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор темы для обновления.</param>
    /// <param name="requestTopicDto">DTO обновленных данных темы.</param>
    /// <param name="cancellationToken">Токен отмены для управления отменой операции.</param>
    /// <returns>DTO обновленной темы.</returns>
    /// <exception cref="TopicNotFoundException">Выбрасывается, если тема не найдена или была удалена.</exception>
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

    /// <summary>
    /// Асинхронно удаляет тему по её идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор темы для удаления.</param>
    /// <param name="cancellationToken">Токен отмены для управления отменой операции.</param>
    /// <exception cref="TopicNotFoundException">Выбрасывается, если тема не найдена или была удалена.</exception>
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