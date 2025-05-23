using AutoMapper;

using MassTransit;

using MediatR;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using SFC.Identity.Messages.Events;
using SFC.Identity.Messages.Events.User;
using SFC.Player.Application.Features.Identity.Commands.Create;
using SFC.Player.Infrastructure.Extensions;
using SFC.Player.Infrastructure.Settings.RabbitMq;

namespace SFC.Player.Infrastructure.Consumers.Identity.Domain.User.Events;
public class UserCreatedConsumer(IMapper mapper, ISender mediator) : IConsumer<UserCreated>
{
    private readonly IMapper _mapper = mapper;
    private readonly ISender _mediator = mediator;

    public async Task Consume(ConsumeContext<UserCreated> context)
    {
        UserCreated @event = context.Message;

        CreateUserCommand command = _mapper.Map<CreateUserCommand>(@event);

        await _mediator.Send(command)
                       .ConfigureAwait(false);
    }
}

public class UserCreatedDefinition : ConsumerDefinition<UserCreatedConsumer>
{
    private readonly RabbitMqSettings _settings;

    private Exchange Exchange { get { return _settings.Exchanges.Identity.Value.Domain.User.Events.Created; } }

    public UserCreatedDefinition(IConfiguration configuration)
    {
        _settings = configuration.GetRabbitMqSettings();
        EndpointName = "sfc.player.identity.user.created.queue";
    }

    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<UserCreatedConsumer> consumerConfigurator,
            IRegistrationContext context)
    {
        endpointConfigurator.ConfigureConsumeTopology = false;

        if (endpointConfigurator is IRabbitMqReceiveEndpointConfigurator rmq)
        {
            rmq.AutoDelete = true;
            rmq.DiscardFaultedMessages();

            // "sfc.identity.user.created"
            rmq.Bind(Exchange.Name, x =>
            {
                x.AutoDelete = true;
                x.ExchangeType = Exchange.Type;
            });
        }
    }
}
