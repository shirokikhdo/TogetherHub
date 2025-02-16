using Domain.Security;
using Infrastructure.Data.DataBaseContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Data.Extensions;

public static class DatabaseExtensions
{
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

    private static async Task SeedData(
        ApplicationDbContext dbContext, 
        UserManager<CustomIdentityUser> userManager)
    {
        await SeedTopicsAsync(dbContext);
        await SeedUsersAsync(dbContext, userManager);
    }

    private static async Task SeedTopicsAsync(ApplicationDbContext dbContext)
    {
        if (!await dbContext.Topics.AnyAsync())
        {
            await dbContext.AddRangeAsync(InitialData.Topics);
            await dbContext.SaveChangesAsync();
        }
    }

    private static async Task SeedUsersAsync(
        ApplicationDbContext dbContext,
        UserManager<CustomIdentityUser> userManager)
    {
        if (!await userManager.Users.AnyAsync())
        {
            foreach (var user in InitialData.Users)
                await userManager.CreateAsync(user, "1111");
        }
    }
}