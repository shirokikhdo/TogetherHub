using Domain.Security.Dtos;
using System.Text.Json;

namespace Api.Middleware;

public class ValidationMiddleware
{
    private readonly RequestDelegate _next;

    public ValidationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

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

    private bool IsValidPassword(string password) =>
        password.Length is >= 4 and <= 8 
        && password.Any(char.IsDigit);
}