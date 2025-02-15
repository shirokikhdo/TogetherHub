using Application;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddApiServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApplicationServices();

var app = builder.Build();
if (app.Environment.IsDevelopment())
    await app.InitializeDatabaseAsync();
app.UseApiServices();
app.Run();