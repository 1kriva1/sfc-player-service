using MassTransit;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using SFC.Player.Application.Common.Enums;

namespace SFC.Player.Infrastructure.Services.Hosted;
public class DataInitializationHostedService(ILogger<DataInitializationHostedService> logger, IServiceProvider services)
    : BaseInitializationService(logger)
{
    private readonly IServiceProvider _services = services;

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        EventId eventId = new((int)RequestId.InitData, Enum.GetName(RequestId.InitData));
        Action<ILogger, Exception?> logStartExecution = LoggerMessage.Define(LogLevel.Information, eventId,
            "Data Initialization Hosted Service running.");
        logStartExecution(Logger, null);

        using IServiceScope scope = _services.CreateScope();

        // send require data
        await SendRequireDataAsync(scope, cancellationToken).ConfigureAwait(false);
    }

    private static Task SendRequireDataAsync(IServiceScope scope, CancellationToken cancellationToken)
    {
        // use bus because it is Initiator (reference to mass transit documentation)
        IBus bus = scope.ServiceProvider.GetRequiredService<IBus>();

        bus.Send(new SFC.Player.Messages.Commands.Data.RequireData(), cancellationToken);

        return Task.CompletedTask;
    }
}
