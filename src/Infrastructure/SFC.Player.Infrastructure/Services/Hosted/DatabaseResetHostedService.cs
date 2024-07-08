using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using SFC.Player.Infrastructure.Persistence;

namespace SFC.Player.Infrastructure.Services.Hosted;
public class DatabaseResetHostedService(
    ILogger<DatabaseResetHostedService> logger,
    IServiceProvider services,
    IHostEnvironment hostEnvironment) : BaseInitializationService(logger)
{
    private readonly IServiceProvider _services = services;
    private readonly IHostEnvironment _hostEnvironment = hostEnvironment;

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Data Initialization Hosted Service running.");

        using IServiceScope scope = _services.CreateScope();

        PlayerDbContext context = scope.ServiceProvider.GetRequiredService<PlayerDbContext>();

        if (_hostEnvironment.IsDevelopment())
        {
            await context.Database.EnsureDeletedAsync(cancellationToken);
        }

        await context.Database.EnsureCreatedAsync(cancellationToken);
    }
}
