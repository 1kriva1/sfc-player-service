using MassTransit;

using Microsoft.Extensions.Configuration;

using SFC.Identity.Messages.Commands.User;
using SFC.Player.Application.Interfaces.Identity;
using SFC.Player.Infrastructure.Extensions;
using SFC.Player.Infrastructure.Settings.RabbitMq;

namespace SFC.Player.Infrastructure.Services.Identity;
public class UserSeedService(IBus bus, IConfiguration configuration) : IUserSeedService
{
    private readonly IBus _bus = bus;
    private readonly IConfiguration _configuration = configuration;

    public async Task SendRequireUsersSeedAsync(CancellationToken cancellationToken = default)
    {
        RabbitMqSettings settings = _configuration.GetRabbitMqSettings();

        RequireUsersSeed command = new() { Initiator = settings.Exchanges.Player.Key };

        await _bus.Send<RequireUsersSeed>(command, cancellationToken)
                  .ConfigureAwait(false);
    }
}
