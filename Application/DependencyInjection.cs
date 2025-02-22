namespace Application;

/// <summary>
/// Класс, содержащий методы расширения для внедрения зависимостей в приложение.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Добавляет сервисы приложения в коллекцию сервисов.
    /// </summary>
    /// <param name="services">Коллекция сервисов, в которую будут добавлены необходимые зависимости.</param>
    /// <returns>Обновленная коллекция сервисов.</returns>
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services)
    {
        services.AddScoped<IJwtSecurityService, JwtSecurityService>();

        return services;
    }
}