namespace Api;

/// <summary>
/// Статический класс для регистрации зависимостей и настройки сервисов приложения.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Добавляет необходимые сервисы для работы API в контейнер зависимостей.
    /// </summary>
    /// <param name="services">Коллекция сервисов, в которую будут добавлены новые сервисы.</param>
    /// <returns>Обновленная коллекция сервисов.</returns>
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddExceptionHandler<CustomExceptionHandler>();
        services.AddControllers(options =>
        {
            var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
            options.Filters.Add(new AuthorizeFilter(policy));
        });
        services.AddEndpointsApiExplorer();
        services.AddSwagger();
        services.AddCors(options =>
        {
            options.AddPolicy("together-hub-policy", policy =>
            {
                policy.AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithOrigins("http://localhost:3000");
            });
        });
        services.AddMediatR(cfg => 
            cfg.RegisterServicesFromAssembly(typeof(GetTopicsHandler).Assembly));
        services.AddAutoMapper(typeof(MappingProfile).Assembly);

        return services;
    }

    /// <summary>
    /// Настраивает middleware для работы API в приложении.
    /// </summary>
    /// <param name="app">Экземпляр приложения, к которому будут применены middleware.</param>
    /// <returns>Обновленный экземпляр приложения.</returns>
    public static WebApplication UseApiServices(this WebApplication app)
    {
        app.UseCors("together-hub-policy");
        //app.UseMiddleware<ValidationMiddleware>();
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseExceptionHandler(options => {});
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        return app;
    }

    /// <summary>
    /// Добавляет и настраивает Swagger для генерации документации API.
    /// </summary>
    /// <param name="services">Коллекция сервисов, в которую будут добавлены настройки Swagger.</param>
    /// <returns>Обновленная коллекция сервисов.</returns>
    private static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "TogetherHub",
                Version = "v1"
            });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer",
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        return services;
    }
}