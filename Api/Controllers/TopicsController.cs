namespace Api.Controllers;

/// <summary>
/// Контроллер для управления темами.
/// Позволяет выполнять операции получения, создания, обновления и удаления тем.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class TopicsController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="TopicsController"/>.
    /// </summary>
    /// <param name="mediator">Экземпляр медиатора для обработки команд и запросов.</param>
    public TopicsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Получает список всех тем.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
    /// <returns>Результат выполнения запроса на получение тем.</returns>
    [HttpGet]
    public async Task<IResult> GetTopics(CancellationToken cancellationToken)
    {
        var query = new GetTopicsQuery();
        var result =  await _mediator.Send(query, cancellationToken);
        return Results.Ok(result);
    }

    /// <summary>
    /// Получает конкретную тему по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор темы.</param>
    /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
    /// <returns>Результат выполнения запроса на получение темы.</returns>
    [HttpGet("{id:guid}")]
    public async Task<IResult> GetTopic(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetTopicQuery(id);
        var result = await _mediator.Send(query, cancellationToken);
        return Results.Ok(result);
    }

    /// <summary>
    /// Создает новую тему.
    /// </summary>
    /// <param name="createTopicDto">Объект данных для создания темы.</param>
    /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
    /// <returns>Результат выполнения операции создания темы.</returns>
    [HttpPost]
    public async Task<IResult> CreateTopic(
        [FromBody] CreateTopicDto createTopicDto,
        CancellationToken cancellationToken)
    {
        var command = new CreateTopicCommand(createTopicDto);
        var result = await _mediator.Send(command, cancellationToken);
        return Results.Created($"/topics/{result.Topic.Id}", result.Topic);
    }

    /// <summary>
    /// Обновляет существующую тему по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор темы.</param>
    /// <param name="updateTopicDto">Объект данных для обновления темы.</param>
    /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
    /// <returns>Результат выполнения операции обновления темы.</returns>
    [HttpPut("{id:guid}")]
    public async Task<IResult> UpdateTopic(
        Guid id,
        [FromBody] UpdateTopicDto updateTopicDto,
        CancellationToken cancellationToken)
    {
        var command = new UpdateTopicCommand(id, updateTopicDto);
        var result = await _mediator.Send(command, cancellationToken);
        return Results.Ok(result);
    }

    /// <summary>
    /// Удаляет тему по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор темы.</param>
    /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
    /// <returns>Результат выполнения операции удаления темы.</returns>
    [HttpDelete("{id:guid}")]
    [Authorize(Policy = "IsTopicAuthor")]
    public async Task<IResult> DeleteTopic(
        Guid id,
        CancellationToken cancellationToken)
    {
        var command = new DeleteTopicCommand(id);
        var result = await _mediator.Send(command, cancellationToken);
        return Results.Ok(result);
    }

    [HttpPost("join/{id:guid}")]
    public async Task<IResult> JoinLeaveTopic(
        Guid id,
        CancellationToken cancellationToken)
    {
        var command = new JoinLeaveTopicCommand(id);
        var result = await _mediator.Send(command, cancellationToken);
        return Results.Ok(result);
    }
}