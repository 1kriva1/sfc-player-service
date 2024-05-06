using MassTransit;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using SFC.Data.Messages.Enums;
using SFC.Data.Messages.Messages;

namespace SFC.Player.Infrastructure.Services.Hosted;
public class DataInitializationHostedService : BaseInitializationService
{
    private readonly IServiceProvider _services;

    public DataInitializationHostedService(ILogger<DataInitializationHostedService> logger,
        IServiceProvider services) : base(logger)
    {
        _services = services;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Data Initialization Hosted Service running.");

        // Create a new scope to retrieve scoped services
        using IServiceScope scope = _services.CreateScope();

        IPublishEndpoint publisher = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();

        await publisher.Publish(new DataRequireMessage { Initiator = DataInitiator.Player }, cancellationToken);
    }
}
