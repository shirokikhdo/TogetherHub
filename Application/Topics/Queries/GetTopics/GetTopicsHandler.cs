namespace Application.Topics.Queries.GetTopics;

/// <summary>
/// Обработчик запроса для получения списка тем.
/// </summary>
/// <remarks>
/// Реализует интерфейс <see cref="IQueryHandler{TQuery, TResult}"/> для обработки запросов типа <see cref="GetTopicsQuery"/> и возвращает результат типа <see cref="GetTopicsResult"/>.
/// </remarks>
public class GetTopicsHandler : IQueryHandler<GetTopicsQuery, GetTopicsResult>
{
    private readonly IApplicationDbContext _dbContext;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="GetTopicsHandler"/>.
    /// </summary>
    /// <param name="dbContext">Контекст приложения для доступа к данным.</param>
    public GetTopicsHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Обрабатывает запрос на получение списка тем.
    /// </summary>
    /// <param name="request">Запрос на получение тем.</param>
    /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
    /// <returns>Асинхронная задача, содержащая результат запроса <see cref="GetTopicsResult"/>.</returns>
    public async Task<GetTopicsResult> Handle(GetTopicsQuery request, CancellationToken cancellationToken)
    {
        var topics = await _dbContext.Topics
            .AsNoTracking()
            .Include(x=>x.Users)
            .ThenInclude(x=>x.CurrentUser)
            .Where(x=>!x.IsDeleted)
            .ToListAsync(cancellationToken);

        var result = new GetTopicsResult(topics.ToResponseTopicDtoList());
        return result;
    }
}