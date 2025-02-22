namespace Application.Topics.Commands.DeleteTopic;

/// <summary>
/// Обработчик команды для удаления темы.
/// </summary>
public class DeleteTopicHandler : ICommandHandler<DeleteTopicCommand, DeleteTopicResult>
{
    private readonly IApplicationDbContext _dbContext;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="DeleteTopicHandler"/>.
    /// </summary>
    /// <param name="dbContext">Контекст базы данных приложения, используемый для доступа к данным.</param>
    public DeleteTopicHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Обрабатывает команду удаления темы.
    /// </summary>
    /// <param name="request">Команда удаления темы, содержащая идентификатор удаляемой темы.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Результат выполнения операции удаления темы.</returns>
    /// <exception cref="TopicNotFoundException">Выбрасывается, если тема не найдена или уже удалена.</exception>
    public async Task<DeleteTopicResult> Handle(DeleteTopicCommand request, CancellationToken cancellationToken)
    {
        var id = request.Id;
        var topicId = TopicId.Of(id);
        var topic = await _dbContext.Topics.FindAsync([topicId], cancellationToken);

        if (topic is null || topic.IsDeleted)
            throw new TopicNotFoundException(id);

        topic.IsDeleted = true;
        topic.DeletedAt = DateTime.UtcNow;

        var result = await _dbContext.SaveChangesAsync(cancellationToken);
        return new DeleteTopicResult(result > 0);
    }
}