using MassTransit;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using SFC.Data.Contracts.Configuration;
using SFC.Data.Contracts.Enums;
using SFC.Data.Contracts.Events;
using SFC.Data.Contracts.Extensions;
using SFC.Player.Application.Interfaces.Persistence;
using SFC.Player.Domain.Entities.Data;
using SFC.Player.Infrastructure.Extensions;

namespace SFC.Player.Infrastructure.Consumers;
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
        DataInitializationEvent @event = context.Message;

        FootballPosition[] footballPositions = await _positionsRepository.ResetAsync(@event.FootballPositions.Select(v => v.MapToDataEntity<FootballPosition>()));

        GameStyle[] gameStyles = await _gameStylesRepository.ResetAsync(@event.GameStyles.Select(v => v.MapToDataEntity<GameStyle>()));

        WorkingFoot[] workingFoots = await _workingFootsRepository.ResetAsync(@event.WorkingFoots.Select(v => v.MapToDataEntity<WorkingFoot>()));

        StatCategory[] categories = await _statCategoriesRepository.ResetAsync(@event.StatCategories.Select(v => v.MapToDataEntity<StatCategory>()));

        StatSkill[] skills = await _statSkillsRepository.ResetAsync(@event.StatSkills.Select(v => v.MapToDataEntity<StatSkill>()));

        StatType[] statTypes = await _statTypesRepository.ResetAsync(@event.StatTypes.Select(v => v.MapToDataEntity(categories, skills)));        

        if (_hostEnvironment.IsDevelopment())
        {
            await _playerRepository.AddSeedPlayersAsync(
                footballPositions,
                gameStyles,
                workingFoots,
                statTypes);
        }
    }
}

public class DataInitializationEventDefinition : ConsumerDefinition<DataInitializationEventConsumer>
{
    public DataInitializationEventDefinition()
    {
        EndpointName = "SFC.Player.queue";
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
                x.RoutingKey = DataInitiator.Player.BuildDataExchangeRoutingKey();
                x.ExchangeType = exchange.Type;
            });
        }
    }
}

