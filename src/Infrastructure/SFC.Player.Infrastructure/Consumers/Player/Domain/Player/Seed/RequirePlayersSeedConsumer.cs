using AutoMapper;

using MassTransit;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using SFC.Player.Application.Interfaces.Player;
using SFC.Player.Infrastructure.Extensions;
using SFC.Player.Infrastructure.Settings.RabbitMq;
using SFC.Player.Messages.Commands.Player;

using Exchange = SFC.Player.Infrastructure.Settings.RabbitMq.Exchange;

namespace SFC.Player.Infrastructure.Consumers.Player.Domain.Player.Seed;
public class RequirePlayersSeedConsumer(IMapper mapper, IPlayerSeedService playerSeedService)
    : IConsumer<RequirePlayersSeed>
{
    private readonly IMapper _mapper = mapper;
    private readonly IPlayerSeedService _playerSeedService = playerSeedService;

    public async Task Consume(ConsumeContext<RequirePlayersSeed> context)
    {
        RequirePlayersSeed message = context.Message;

        IEnumerable<PlayerEntity> players = await _playerSeedService.GetSeedPlayersAsync().ConfigureAwait(true);

        SeedPlayers command = _mapper.Map<SeedPlayers>(players)
                                     .SetCommandInitiator(message.Initiator);

        await context.Publish(command).ConfigureAwait(true);
    }
}

public class RequirePlayersSeedDefinition : ConsumerDefinition<RequirePlayersSeedConsumer>
{
    private readonly RabbitMqSettings _settings;

    private Message Exchange { get { return _settings.Exchanges.Player.Value.Domain.Player.Seed.RequireSeed; } }

    public RequirePlayersSeedDefinition(IConfiguration configuration)
    {
        _settings = configuration.GetRabbitMqSettings();
        EndpointName = "sfc.player.players.seed.require.queue";
    }

    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<RequirePlayersSeedConsumer> consumerConfigurator,
            IRegistrationContext context)
    {
        endpointConfigurator.ConfigureConsumeTopology = false;

        if (endpointConfigurator is IRabbitMqReceiveEndpointConfigurator rmq)
        {
            rmq.AutoDelete = true;
            rmq.DiscardFaultedMessages();

            // "sfc.player.players.seed.require"
            rmq.Bind(Exchange.Name, x => x.AutoDelete = true);
        }
    }
}
