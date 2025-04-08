namespace Application.Topics.Queries.GetTopic;

/// <summary>
/// Обработчик запроса для получения информации о теме.
/// </summary>
/// <remarks>
/// Реализует интерфейс <see cref="IQueryHandler{TQuery, TResult}"/> для обработки запросов типа <see cref="GetTopicQuery"/> 
/// и возвращает результат типа <see cref="GetTopicResult"/>.
/// </remarks>
public class GetTopicHandler : IQueryHandler<GetTopicQuery, GetTopicResult>
{
    private readonly IApplicationDbContext _dbContext;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="GetTopicHandler"/>.
    /// </summary>
    /// <param name="dbContext">Контекст приложения для работы с базой данных.</param>
    public GetTopicHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Асинхронно обрабатывает запрос на получение информации о теме.
    /// </summary>
    /// <param name="request">Запрос на получение темы, содержащий идентификатор темы.</param>
    /// <param name="cancellationToken">Токен отмены для управления отменой операции.</param>
    /// <returns>Результат запроса, содержащий информацию о теме.</returns>
    /// <exception cref="TopicNotFoundException">Выбрасывается, если тема не найдена или была удалена.</exception>
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