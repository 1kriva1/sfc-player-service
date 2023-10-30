using SFC.Players.Api.Extensions;
using SFC.Players.Infrastructure.Persistence;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

WebApplication app = builder
       .ConfigureServices()
       .ConfigurePipeline();

app.Run();

public partial class Program { }
