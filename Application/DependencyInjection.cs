﻿namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services)
    {
        services.AddScoped<IJwtSecurityService, JwtSecurityService>();

        return services;
    }
}