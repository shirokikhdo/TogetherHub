namespace Infrastructure.Data.Extensions;

/// <summary>
/// Расширения для работы с базой данных.
/// Содержит методы для инициализации базы данных и заполнения её начальными данными.
/// </summary>
public static class DatabaseExtensions
{
    /// <summary>
    /// Асинхронно инициализирует базу данных приложения.
    /// Выполняет миграции базы данных и заполняет её начальными данными.
    /// </summary>
    /// <param name="app">Экземпляр приложения, для которого будет выполняться инициализация базы данных.</param>
    /// <returns>Задача, представляющая асинхронную операцию.</returns>
    public static async Task InitializeDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var dbContext = scope.ServiceProvider
            .GetRequiredService<ApplicationDbContext>();

        var userManager = scope.ServiceProvider
            .GetRequiredService<UserManager<CustomIdentityUser>>();

        dbContext.Database.MigrateAsync().GetAwaiter().GetResult();

        await SeedData(dbContext, userManager);
    }

    /// <summary>
    /// Заполняет базу данных начальными данными.
    /// </summary>
    /// <param name="dbContext">Контекст базы данных для доступа к сущностям.</param>
    /// <param name="userManager">Менеджер пользователей для управления пользователями.</param>
    /// <returns>Задача, представляющая асинхронную операцию.</returns>
    private static async Task SeedData(
        ApplicationDbContext dbContext, 
        UserManager<CustomIdentityUser> userManager)
    {
        await SeedTopicsAsync(dbContext);
        await SeedUsersAsync(userManager);
    }

    /// <summary>
    /// Заполняет базу данных начальными темами, если они отсутствуют.
    /// </summary>
    /// <param name="dbContext">Контекст базы данных для доступа к сущности тем.</param>
    /// <returns>Задача, представляющая асинхронную операцию.</returns>
    private static async Task SeedTopicsAsync(ApplicationDbContext dbContext)
    {
        if (!await dbContext.Topics.AnyAsync())
        {
            await dbContext.AddRangeAsync(InitialData.Topics);
            await dbContext.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Заполняет базу данных начальными пользователями, если они отсутствуют.
    /// </summary>
    /// <param name="userManager">Менеджер пользователей для управления пользователями.</param>
    /// <returns>Задача, представляющая асинхронную операцию.</returns>
    private static async Task SeedUsersAsync(UserManager<CustomIdentityUser> userManager)
    {
        if (!await userManager.Users.AnyAsync())
        {
            foreach (var user in InitialData.Users)
                await userManager.CreateAsync(user, "1111");
        }
    }
}