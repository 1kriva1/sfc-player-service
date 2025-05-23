using AutoMapper;

using MassTransit;

using MediatR;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using SFC.Identity.Messages.Events;
using SFC.Identity.Messages.Events.User;
using SFC.Player.Application.Features.Identity.Commands.CreateRange;
using SFC.Player.Application.Interfaces.Persistence.Repository;
using SFC.Player.Infrastructure.Extensions;
using SFC.Player.Infrastructure.Settings.RabbitMq;

namespace SFC.Player.Infrastructure.Consumers.Identity.Domain.User.Seed;
public class UsersSeededConsumer(IMapper mapper, IWebHostEnvironment environment, ISender mediator) : IConsumer<UsersSeeded>
{
    private readonly IMapper _mapper = mapper;
    private readonly IWebHostEnvironment _environment = environment;
    private readonly ISender _mediator = mediator;

    public async Task Consume(ConsumeContext<UsersSeeded> context)
    {
        if (_environment.IsDevelopment())
        {
            UsersSeeded @event = context.Message;

            CreateUsersCommand command = _mapper.Map<CreateUsersCommand>(@event.Users);

            await _mediator.Send(command)
                           .ConfigureAwait(false);
        }
    }
}

public class UsersSeededConsumerDefinition : ConsumerDefinition<UsersSeededConsumer>
{
    private readonly RabbitMqSettings _settings;

    private Exchange Exchange { get { return _settings.Exchanges.Identity.Value.Domain.User.Seed.Seeded; } }

    public UsersSeededConsumerDefinition(IConfiguration configuration)
    {
        _settings = configuration.GetRabbitMqSettings();
        EndpointName = "sfc.player.identity.users.seeded.queue";
    }

    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<UsersSeededConsumer> consumerConfigurator, IRegistrationContext context)
    {
        endpointConfigurator.ConfigureConsumeTopology = false;

        if (endpointConfigurator is IRabbitMqReceiveEndpointConfigurator rmq)
        {
            rmq.AutoDelete = true;
            rmq.DiscardFaultedMessages();

            // "sfc.team.identity.users.seeded"
            rmq.Bind(Exchange.Name, x =>
            {
                x.AutoDelete = true;
                x.ExchangeType = Exchange.Type;
            });
        }
    }
}
