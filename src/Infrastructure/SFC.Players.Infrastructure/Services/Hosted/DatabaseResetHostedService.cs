using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using SFC.Players.Infrastructure.Persistence;

namespace SFC.Players.Infrastructure.Services.Hosted;
public class DatabaseResetHostedService : IHostedService
{
    private readonly ILogger<DatabaseResetHostedService> _logger;
    private readonly IServiceProvider _services;
    private readonly IHostEnvironment _hostEnvironment;

    public DatabaseResetHostedService(
        ILogger<DatabaseResetHostedService> logger,
        IServiceProvider services,
        IHostEnvironment hostEnvironment)
    {
        _logger = logger;
        _services = services;
        _hostEnvironment = hostEnvironment;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Data Initialization Hosted Service running.");

        using IServiceScope scope = _services.CreateScope();

        PlayersDbContext context = scope.ServiceProvider.GetRequiredService<PlayersDbContext>();

        if (_hostEnvironment.IsDevelopment())
        {
            await context.Database.EnsureDeletedAsync(cancellationToken);

            if (context.Database.IsRelational())
            {
                await context.Database.MigrateAsync(cancellationToken);
            }
        }

        await context.Database.EnsureCreatedAsync(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Data Initialization Hosted Service is stopping.");
        return Task.CompletedTask;
    }
}
