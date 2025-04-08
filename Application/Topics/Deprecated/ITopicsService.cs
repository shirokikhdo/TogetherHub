namespace Application.Topics.Deprecated;

/// <summary>
/// Интерфейс для работы с темами.
/// </summary>
/// <remarks>
/// Данный сервис устарел и не рекомендуется к использованию.
/// </remarks>
[Obsolete("Данный сервис устарел", true)]
public interface ITopicsService
{
    /// <summary>
    /// Получает список всех тем асинхронно.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены для управления отменой операции.</param>
    /// <returns>Список DTO тем.</returns>
    Task<List<ResponseTopicDto>> GetTopicsAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Получает информацию о конкретной теме по её идентификатору асинхронно.
    /// </summary>
    /// <param name="id">Идентификатор темы.</param>
    /// <param name="cancellationToken">Токен отмены для управления отменой операции.</param>
    /// <returns>DTO запрашиваемой темы.</returns>
    Task<ResponseTopicDto> GetTopicAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Создает новую тему асинхронно.
    /// </summary>
    /// <param name="requestTopicDto">DTO новой темы, содержащий необходимые данные для создания.</param>
    /// <param name="cancellationToken">Токен отмены для управления отменой операции.</param>
    /// <returns>DTO созданной темы.</returns>
    Task<ResponseTopicDto> CreateTopicAsync(CreateTopicDto requestTopicDto, CancellationToken cancellationToken);

    /// <summary>
    /// Обновляет существующую тему по её идентификатору асинхронно.
    /// </summary>
    /// <param name="id">Идентификатор темы для обновления.</param>
    /// <param name="requestTopicDto">DTO обновленных данных темы.</param>
    /// <param name="cancellationToken">Токен отмены для управления отменой операции.</param>
    /// <returns>DTO обновленной темы.</returns>
    Task<ResponseTopicDto> UpdateTopicAsync(Guid id, UpdateTopicDto requestTopicDto, CancellationToken cancellationToken);

    /// <summary>
    /// Удаляет тему по её идентификатору асинхронно.
    /// </summary>
    /// <param name="id">Идентификатор темы для удаления.</param>
    /// <param name="cancellationToken">Токен отмены для управления отменой операции.</param>
    Task DeleteTopicAsync(Guid id, CancellationToken cancellationToken);
}