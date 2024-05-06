using MassTransit;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using SFC.Data.Messages.Enums;
using SFC.Data.Messages.Messages;
using SFC.Player.Application.Interfaces.Persistence;
using SFC.Player.Domain.Entities.Data;
using SFC.Player.Infrastructure.Extensions;
using SFC.Player.Infrastructure.Settings;

using Exchange = SFC.Player.Infrastructure.Settings.Exchange;

namespace SFC.Player.Infrastructure.Consumers;
public class DataInitializationMessageConsumer : IConsumer<DataInitializationMessage>
{
    private readonly ILogger<DataInitializationMessageConsumer> _logger;
    private readonly IHostEnvironment _hostEnvironment;
    private readonly IDataRepository<FootballPosition> _positionsRepository;
    private readonly IDataRepository<GameStyle> _gameStylesRepository;
    private readonly IDataRepository<StatCategory> _statCategoriesRepository;
    private readonly IDataRepository<StatSkill> _statSkillsRepository;
    private readonly IDataRepository<StatType> _statTypesRepository;
    private readonly IDataRepository<WorkingFoot> _workingFootsRepository;
    private readonly IPlayerRepository _playerRepository;

    public DataInitializationMessageConsumer(
        ILogger<DataInitializationMessageConsumer> logger,
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

    public async Task Consume(ConsumeContext<DataInitializationMessage> context)
    {
        DataInitializationMessage message = context.Message;

        FootballPosition[] footballPositions = await _positionsRepository.ResetAsync(message.FootballPositions.Select(v => v.MapToDataEntity<FootballPosition>()));

        GameStyle[] gameStyles = await _gameStylesRepository.ResetAsync(message.GameStyles.Select(v => v.MapToDataEntity<GameStyle>()));

        WorkingFoot[] workingFoots = await _workingFootsRepository.ResetAsync(message.WorkingFoots.Select(v => v.MapToDataEntity<WorkingFoot>()));

        StatCategory[] categories = await _statCategoriesRepository.ResetAsync(message.StatCategories.Select(v => v.MapToDataEntity<StatCategory>()));

        StatSkill[] skills = await _statSkillsRepository.ResetAsync(message.StatSkills.Select(v => v.MapToDataEntity<StatSkill>()));

        StatType[] statTypes = await _statTypesRepository.ResetAsync(message.StatTypes.Select(v => v.MapToDataEntity(categories, skills)));        

        if (_hostEnvironment.IsDevelopment())
        {
            await _playerRepository.SeedPlayersAsync(
                footballPositions,
                gameStyles,
                workingFoots,
                statTypes);
        }
    }
}

public class DataInitializationMessageDefinition : ConsumerDefinition<DataInitializationMessageConsumer>
{
    private readonly IConfiguration _configuration;

    public DataInitializationMessageDefinition(IConfiguration configuration)
    {
        EndpointName = "sfc.player.queue";
        _configuration = configuration;
    }

    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<DataInitializationMessageConsumer> consumerConfigurator,
            IRegistrationContext context)
    {
        endpointConfigurator.ConfigureConsumeTopology = false;

        if (endpointConfigurator is IRabbitMqReceiveEndpointConfigurator rmq)
        {
            rmq.AutoDelete = true;

            rmq.DiscardFaultedMessages();

            RabbitMqSettings settings = _configuration.GetRabbitMqSettings();

            Exchange exchange = settings.Exchanges.Data.Value.Init;

            rmq.Bind(exchange.Name, x =>
            {
                x.AutoDelete = true;
                x.RoutingKey = DataInitiator.Init.BuildExchangeRoutingKey(settings.Exchanges.Data.Key);
                x.ExchangeType = exchange.Type;
            });

            rmq.Bind(exchange.Name, x =>
            {
                x.AutoDelete = true;
                x.RoutingKey = DataInitiator.Player.BuildExchangeRoutingKey(settings.Exchanges.Data.Key);
                x.ExchangeType = exchange.Type;
            });
        }
    }
}

