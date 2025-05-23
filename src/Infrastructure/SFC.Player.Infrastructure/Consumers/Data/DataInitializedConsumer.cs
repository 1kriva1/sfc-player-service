using AutoMapper;

using MassTransit;

using MediatR;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using SFC.Data.Messages.Events.Data;
using SFC.Player.Application.Features.Data.Commands.Reset;
using SFC.Player.Infrastructure.Extensions;
using SFC.Player.Infrastructure.Settings.RabbitMq;

namespace SFC.Player.Infrastructure.Consumers.Data;
public class DataInitializedConsumer(IMapper mapper, ISender mediator) : IConsumer<DataInitialized>
{
    private readonly IMapper _mapper = mapper;
    private readonly ISender _mediator = mediator;

    public async Task Consume(ConsumeContext<DataInitialized> context)
    {
        DataInitialized message = context.Message;

        ResetDataCommand command = _mapper.Map<ResetDataCommand>(message);

        await _mediator.Send(command).ConfigureAwait(true);
    }
}

public class DataInitializedDefinition : ConsumerDefinition<DataInitializedConsumer>
{
    private readonly RabbitMqSettings _settings;

    private Exchange Exchange { get { return _settings.Exchanges.Data.Value.Data.Source.Initialized; } }

    public DataInitializedDefinition(IConfiguration configuration)
    {
        _settings = configuration.GetRabbitMqSettings();
        EndpointName = "sfc.player.data.initialized.queue";
    }

    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<DataInitializedConsumer> consumerConfigurator,
            IRegistrationContext context)
    {
        endpointConfigurator.ConfigureConsumeTopology = false;

        if (endpointConfigurator is IRabbitMqReceiveEndpointConfigurator rmq)
        {
            rmq.AutoDelete = true;
            rmq.DiscardFaultedMessages();

            rmq.Bind(Exchange.Name, x =>
            {
                x.AutoDelete = true;
                x.ExchangeType = Exchange.Type;
            });
        }
    }
}