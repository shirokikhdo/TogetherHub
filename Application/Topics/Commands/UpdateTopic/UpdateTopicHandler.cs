namespace Application.Topics.Commands.UpdateTopic;

/// <summary>
/// Обработчик команды обновления темы.
/// </summary>
public class UpdateTopicHandler : ICommandHandler<UpdateTopicCommand, UpdateTopicResult>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IUserAccessor _userAccessor;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="UpdateTopicHandler"/>.
    /// </summary>
    /// <param name="dbContext">Контекст базы данных приложения.</param>
    /// <param name="mapper">Объект для сопоставления данных.</param>
    public UpdateTopicHandler(
        IApplicationDbContext dbContext,
        IMapper mapper,
        IUserAccessor userAccessor)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _userAccessor = userAccessor;
    }

    /// <summary>
    /// Обрабатывает команду обновления темы.
    /// </summary>
    /// <param name="request">Команда обновления темы, содержащая идентификатор и данные для обновления.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Результат обновления темы.</returns>
    /// <exception cref="TopicNotFoundException">Выбрасывается, если тема не найдена или была удалена.</exception>
    public async Task<UpdateTopicResult> Handle(UpdateTopicCommand request, CancellationToken cancellationToken)
    {
        var id = request.Id;
        var dto = request.RequestTopicDto;

        var topicId = TopicId.Of(id);
        var topic = await _dbContext.Topics
            .Include(x => x.Users)
            .ThenInclude(x => x.CurrentUser)
            .FirstOrDefaultAsync(x => x.Id == topicId, cancellationToken);

        if (topic is null || topic.IsDeleted)
            throw new TopicNotFoundException(id);

        var username = _userAccessor.GetUsername();
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(x => x.UserName == username, cancellationToken);

        if (user is null)
            throw new UserNotFoundException(username);

        var organizerUsername = topic.Users
            .FirstOrDefault(x => x.Role == ParticipantRole.Organizer)?
            .CurrentUser?.UserName!;
        if (organizerUsername != username)
            throw new UserNotOrganizerException(username, topic.Id.Value);


        _mapper.Map(dto, topic);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateTopicResult(topic.ToResponseTopicDto());
    }
}