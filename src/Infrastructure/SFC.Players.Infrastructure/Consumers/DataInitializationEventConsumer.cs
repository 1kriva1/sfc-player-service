using MassTransit;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using SFC.Data.Contracts.Configuration;
using SFC.Data.Contracts.Enums;
using SFC.Data.Contracts.Events;
using SFC.Data.Contracts.Extensions;
using SFC.Players.Application.Interfaces.Persistence;
using SFC.Players.Domain.Entities.Data;
using SFC.Players.Infrastructure.Extensions;

namespace SFC.Players.Infrastructure.Consumers;
public class DataInitializationEventConsumer : IConsumer<DataInitializationEvent>
{
    private readonly ILogger<DataInitializationEventConsumer> _logger;
    private readonly IHostEnvironment _hostEnvironment;
    private readonly IDataRepository<FootballPosition> _positionsRepository;
    private readonly IDataRepository<GameStyle> _gameStylesRepository;
    private readonly IDataRepository<StatCategory> _statCategoriesRepository;
    private readonly IDataRepository<StatSkill> _statSkillsRepository;
    private readonly IDataRepository<StatType> _statTypesRepository;
    private readonly IDataRepository<WorkingFoot> _workingFootsRepository;
    private readonly IPlayerRepository _playerRepository;

    public DataInitializationEventConsumer(
        ILogger<DataInitializationEventConsumer> logger,
        IHostEnvironment hostEnvironment,
        IDataRepository<FootballPosition> positionsRepository,
        IDataRepository<GameStyle> gameStylesRepository,
        IDataRepository<StatCategory> statCategoriesRepository,
        IDataRepository<StatSkill> statSkillsRepository,
        IDataRepository<StatType> statTypesRepository,
        IDataRepository<WorkingFoot> workingFootsRepository,
        IPlayerRepository playerRepository)
    {
        _logger = logger;
        _hostEnvironment = hostEnvironment;
        _positionsRepository = positionsRepository;
        _gameStylesRepository = gameStylesRepository;
        _statCategoriesRepository = statCategoriesRepository;
        _statSkillsRepository = statSkillsRepository;
        _statTypesRepository = statTypesRepository;
        _workingFootsRepository = workingFootsRepository;
        _playerRepository = playerRepository;
    }

    public async Task Consume(ConsumeContext<DataInitializationEvent> context)
    {
        _logger.LogInformation("Content Received:", context.Message);

        DataInitializationEvent @event = context.Message;

        await _positionsRepository.ResetAsync(@event.FootballPositions.Select(v => v.MapToDataEntity<FootballPosition>()));

        await _gameStylesRepository.ResetAsync(@event.GameStyles.Select(v => v.MapToDataEntity<GameStyle>()));

        await _statCategoriesRepository.ResetAsync(@event.StatCategories.Select(v => v.MapToDataEntity<StatCategory>()));

        await _statSkillsRepository.ResetAsync(@event.StatSkills.Select(v => v.MapToDataEntity<StatSkill>()));

        await _statTypesRepository.ResetAsync(@event.StatTypes.Select(v => v.MapToDataEntity()));

        await _workingFootsRepository.ResetAsync(@event.WorkingFoots.Select(v => v.MapToDataEntity<WorkingFoot>()));

        if (_hostEnvironment.IsDevelopment())
        {
            await _playerRepository.AddSeedPlayersAsync();
        }
    }
}

public class DataInitializationEventDefinition : ConsumerDefinition<DataInitializationEventConsumer>
{
    public DataInitializationEventDefinition()
    {
        EndpointName = "sfc.players.queue";
    }

    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<DataInitializationEventConsumer> consumerConfigurator,
            IRegistrationContext context)
    {
        endpointConfigurator.ConfigureConsumeTopology = false;

        if (endpointConfigurator is IRabbitMqReceiveEndpointConfigurator rmq)
        {
            rmq.AutoDelete = true;

            rmq.DiscardFaultedMessages();

            Exchange exchange = Exchange.List.First(exch => exch.Key == typeof(DataInitializationEvent)).Value;

            rmq.Bind(exchange.Name, x =>
            {
                x.AutoDelete = true;
                x.RoutingKey = DataInitiator.Init.BuildDataExchangeRoutingKey();
                x.ExchangeType = exchange.Type;
            });

            rmq.Bind(exchange.Name, x =>
            {
                x.AutoDelete = true;
                x.RoutingKey = DataInitiator.Players.BuildDataExchangeRoutingKey();
                x.ExchangeType = exchange.Type;
            });
        }
    }
}

