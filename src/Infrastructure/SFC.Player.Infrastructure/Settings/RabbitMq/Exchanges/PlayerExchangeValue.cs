using SFC.Player.Infrastructure.Settings.RabbitMq.Exchanges.Common.Data;
using SFC.Player.Infrastructure.Settings.RabbitMq.Exchanges.Common.Domain;

namespace SFC.Player.Infrastructure.Settings.RabbitMq.Exchanges;
public class PlayerExchangeValue
{
    public DataExchange<PlayerDataDependentExchange> Data { get; set; } = default!;

    public PlayerDomainExchange Domain { get; set; } = default!;
}

public class PlayerDataDependentExchange
{
    public DataDependentExchange Data { get; set; } = default!;
}

public class PlayerDomainExchange
{
    public DomainExchange<PlayerDomainEventsExchange> Player { get; set; } = default!;
}

public class PlayerDomainEventsExchange
{
    public Exchange Created { get; set; } = default!;

    public Exchange Updated { get; set; } = default!;
}
