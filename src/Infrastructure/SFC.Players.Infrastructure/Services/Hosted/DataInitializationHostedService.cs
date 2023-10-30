using MassTransit;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using SFC.Data.Contracts.Enums;
using SFC.Data.Contracts.Events;

namespace SFC.Players.Infrastructure.Services.Hosted;
public class DataInitializationHostedService : IHostedService
{
    private readonly ILogger<DataInitializationHostedService> _logger;
    private readonly IServiceProvider _services;

    public DataInitializationHostedService(ILogger<DataInitializationHostedService> logger, IServiceProvider services)
    {
        _logger = logger;
        _services = services;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Data Initialization Hosted Service running.");

        // Create a new scope to retrieve scoped services
        using IServiceScope scope = _services.CreateScope();

        IPublishEndpoint publisher = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();

        await publisher.Publish(new DataRequireEvent { Initiator = DataInitiator.Players }, cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Data Initialization Hosted Servic is stopping.");
        return Task.CompletedTask;
    }
}
