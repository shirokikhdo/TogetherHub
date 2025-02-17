using Api.Exceptions.Handler;
using Api.Security.Extensions;

namespace Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddExceptionHandler<CustomExceptionHandler>();
        services.AddControllers();
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
        services.AddIdentityServices(configuration);

        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        app.UseCors("together-hub-policy");
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
}