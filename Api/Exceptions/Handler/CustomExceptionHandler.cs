namespace Api.Exceptions.Handler;

/// <summary>
/// Класс для обработки исключений в приложении.
/// Реализует интерфейс <see cref="IExceptionHandler"/> и предоставляет механизм логирования и формирования ответов на исключения.
/// </summary>
public class CustomExceptionHandler : IExceptionHandler
{
    private readonly ILogger<CustomExceptionHandler> _logger;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="CustomExceptionHandler"/>.
    /// </summary>
    /// <param name="logger">Логгер для записи информации об исключениях.</param>
    public CustomExceptionHandler(
        ILogger<CustomExceptionHandler> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Попытка обработки исключения асинхронно.
    /// </summary>
    /// <param name="httpContext">Контекст HTTP, содержащий информацию о запросе и ответе.</param>
    /// <param name="exception">Исключение, которое необходимо обработать.</param>
    /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
    /// <returns>Значение, указывающее, была ли обработка успешной.</returns>
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, 
        Exception exception, 
        CancellationToken cancellationToken)
    {
        _logger.LogWarning($"Обработанное исключение: {exception.Message}, время: {DateTime.Now}");

        (string Detail, string Title, int StatusCode) details = exception switch
        {
            NotFoundException => (exception.Message,
                exception.GetType().Name,
                httpContext.Response.StatusCode = StatusCodes.Status404NotFound),

            UserExistsException => (exception.Message, 
                exception.GetType().Name,
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest),

            WrongPasswordException => (exception.Message, 
                exception.GetType().Name,
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest),

            CreatingUserException => (exception.Message, 
                exception.GetType().Name,
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError),

            _ => (exception.Message,
                exception.GetType().Name,
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError),
        };

        var problemDetails = new ProblemDetails
        {
            Title = details.Title,
            Detail = details.Detail,
            Status = details.StatusCode,
            Instance = httpContext.Request.Path
        };

        problemDetails.Extensions.Add("traceId", httpContext.TraceIdentifier);
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}