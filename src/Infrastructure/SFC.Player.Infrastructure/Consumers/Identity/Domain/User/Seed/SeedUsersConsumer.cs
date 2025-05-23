using AutoMapper;

using MassTransit;

using MediatR;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using SFC.Identity.Messages.Commands;
using SFC.Identity.Messages.Commands.User;
using SFC.Player.Application.Features.Identity.Commands.CreateRange;
using SFC.Player.Infrastructure.Extensions;
using SFC.Player.Infrastructure.Settings.RabbitMq;

using Exchange = SFC.Player.Infrastructure.Settings.RabbitMq.Exchange;

namespace SFC.Player.Infrastructure.Consumers.Identity.Domain.User.Seed;
public class SeedUsersConsumer(IMapper mapper, IWebHostEnvironment environment, ISender mediator) : IConsumer<SeedUsers>
{
    private readonly IMapper _mapper = mapper;
    private readonly IWebHostEnvironment _environment = environment;
    private readonly ISender _mediator = mediator;

    public async Task Consume(ConsumeContext<SeedUsers> context)
    {
        if (_environment.IsDevelopment())
        {
            SeedUsers message = context.Message;

            CreateUsersCommand command = _mapper.Map<CreateUsersCommand>(message.Users);

            await _mediator.Send(command)
                           .ConfigureAwait(false);
        }
    }
}

public class SeedUsersConsumerDefinition : ConsumerDefinition<SeedUsersConsumer>
{
    private readonly RabbitMqSettings _settings;

    private Exchange Exchange { get { return _settings.Exchanges.Identity.Value.Domain.User.Seed.Seed; } }

    public SeedUsersConsumerDefinition(IConfiguration configuration)
    {
        _settings = configuration.GetRabbitMqSettings();
        EndpointName = "sfc.player.identity.users.seed.queue";
    }

    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<SeedUsersConsumer> consumerConfigurator, IRegistrationContext context)
    {
        endpointConfigurator.ConfigureConsumeTopology = false;

        if (endpointConfigurator is IRabbitMqReceiveEndpointConfigurator rmq)
        {
            rmq.AutoDelete = true;
            rmq.DiscardFaultedMessages();

            // "sfc.team.identity.users.seed"
            rmq.Bind(Exchange.Name, x =>
            {
                x.AutoDelete = true;
                x.RoutingKey = _settings.Exchanges.Player.Key.BuildExchangeRoutingKey(_settings.Exchanges.Identity.Key);
                x.ExchangeType = Exchange.Type;
            });
        }
    }
}
