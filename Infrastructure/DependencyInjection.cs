namespace Infrastructure;

/// <summary>
/// Класс, содержащий методы для настройки зависимостей и сервисов инфраструктуры.
/// Предоставляет методы для регистрации контекста базы данных, идентификации и аутентификации пользователей.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Метод расширения для добавления инфраструктурных сервисов в контейнер зависимостей.
    /// Регистрация контекста базы данных, идентификации пользователей и аутентификации с использованием JWT.
    /// </summary>
    /// <param name="services">Коллекция сервисов, в которую будут добавлены инфраструктурные сервисы.</param>
    /// <param name="configuration">Объект конфигурации, используемый для получения строк подключения и других параметров.</param>
    /// <returns>Обновленная коллекция сервисов.</returns>
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("SqLiteConnection");
        services.AddDbContext<ApplicationDbContext>(
            options => options.UseSqlite(connectionString));

        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        services.AddScoped<IJwtSecurityService, JwtSecurityService>();
        
        services.AddHttpContextAccessor();
        services.AddScoped<IUserAccessor, UserAccessor>();

        services.AddIdentityCore<CustomIdentityUser>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 1;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.User.RequireUniqueEmail = true;
        }).AddEntityFrameworkStores<ApplicationDbContext>();

        var secretKey = configuration["AuthSettings:SecretKey"]!;
        var bytes = Encoding.UTF8.GetBytes(secretKey);
        var key = new SymmetricSecurityKey(bytes);

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateLifetime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });

        return services;
    }
}