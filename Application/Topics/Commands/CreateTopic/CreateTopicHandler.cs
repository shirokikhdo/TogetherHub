namespace Application.Topics.Commands.CreateTopic;

/// <summary>
/// Обработчик команды для создания новой темы.
/// </summary>
public class CreateTopicHandler : ICommandHandler<CreateTopicCommand, CreateTopicResult>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IUserAccessor _userAccessor;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="CreateTopicHandler"/>.
    /// </summary>
    /// <param name="dbContext">Контекст базы данных приложения.</param>
    /// <param name="mapper">Объект для преобразования данных.</param>
    public CreateTopicHandler(
        IApplicationDbContext dbContext,
        IMapper mapper,
        IUserAccessor userAccessor)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _userAccessor = userAccessor;
    }

    /// <summary>
    /// Обрабатывает команду создания темы.
    /// </summary>
    /// <param name="request">Команда для создания темы, содержащая данные о теме.</param>
    /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
    /// <returns>Результат создания темы.</returns>
    public async Task<CreateTopicResult> Handle(CreateTopicCommand request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUsername(), cancellationToken: cancellationToken);

        var topic = _mapper.Map<Topic>(request.RequestTopicDto);

        var relationship = Relationship.Create(
            RelationshipId.Of(Guid.NewGuid()),
            user!.Id,
            user,
            ParticipantRole.Organizer,
            topic.Id,
            topic);

        topic.Users.Add(relationship);

        await _dbContext.Topics.AddAsync(topic, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new CreateTopicResult(topic.ToResponseTopicDto());
    }
}