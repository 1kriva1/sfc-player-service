using SFC.Player.Api.Infrastructure.Extensions;
using SFC.Player.Infrastructure.Persistence;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

WebApplication app = builder
       .ConfigureServices()
       .ConfigurePipeline();

app.Run();

public partial class Program { }
