var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddApiServices()
    .AddInfrastructureServices(builder.Configuration);

var app = builder.Build();
if (app.Environment.IsDevelopment())
    await app.InitializeDatabaseAsync();
app.UseApiServices();
app.Run();