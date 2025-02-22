namespace Api.Middleware;

/// <summary>
/// Промежуточное ПО для валидации входящих HTTP-запросов.
/// Обрабатывает запросы на регистрацию пользователей и проверяет корректность используемых методов и данных.
/// </summary>
public class ValidationMiddleware
{
    private readonly RequestDelegate _next;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="ValidationMiddleware"/>.
    /// </summary>
    /// <param name="next">Следующий делегат в конвейере обработки запросов.</param>
    public ValidationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    /// <summary>
    /// Асинхронно обрабатывает HTTP-запрос.
    /// Проверяет метод запроса и валидирует данные для регистрации пользователя.
    /// </summary>
    /// <param name="context">Контекст HTTP, содержащий информацию о текущем запросе и ответе.</param>
    /// <returns>Асинхронная задача, представляющая результат обработки запроса.</returns>
    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Method.Equals("GET")
            && context.Request.Path.Value!.ToLower().Contains("/register"))
        {
            await context.Response.WriteAsJsonAsync(new
            {
                Title = "Пользователь ввел неверный тип метода",
                Status = StatusCodes.Status400BadRequest,
                Detail = "Details",
                Instance = context.Request.Path
            });
            return;
        }

        if (context.Request.Method.Equals("POST"))
        {
            context.Request.EnableBuffering();
            var body = await new StreamReader(context.Request.Body).ReadToEndAsync();
            context.Request.Body.Position = 0;

            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var model = JsonSerializer.Deserialize<RegisterIdentityUserDto>(body, options);
                if (model?.Password != null && !IsValidPassword(model.Password))
                {
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsJsonAsync(new
                    {
                        Title = "Пароль не подходит",
                        Status = StatusCodes.Status400BadRequest,
                        Detail = "Detail",
                        Instance = context.Request.Path
                    });
                    return;
                }
                await _next(context);
            }
            catch
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsJsonAsync(new
                {
                    Message = "Ошибка валидации данных"
                });
            }
        }
        else
        {
            await _next(context);
        }
    }

    /// <summary>
    /// Проверяет, является ли пароль допустимым по заданным критериям.
    /// </summary>
    /// <param name="password">Пароль, который необходимо проверить.</param>
    /// <returns><c>true</c>, если пароль допустим; в противном случае <c>false</c>.</returns>
    private bool IsValidPassword(string password) =>
        password.Length is >= 4 and <= 8 
        && password.Any(char.IsDigit);
}