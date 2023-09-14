using SFC.Players.Api;
using SFC.Players.Infrastructure.Persistence;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

WebApplication app = builder
       .ConfigureServices()
       .ConfigurePipeline();

if (app.Environment.IsDevelopment())
{
    await app.ResetDatabaseAsync();
}

app.Run();

public partial class Program { }
