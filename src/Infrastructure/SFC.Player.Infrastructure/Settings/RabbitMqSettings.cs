namespace SFC.Player.Infrastructure.Settings;
public class RabbitMqSettings
{
    public const string SECTION_KEY = "RabbitMq";

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Name { get; set; } = null!;

    public RabbitMqRetrySettings Retry { get; set; } = default!;

    public RabbitMqExchangesSettings Exchanges { get; set; } = default!;
}

public class RabbitMqRetrySettings
{
    public ushort Limit { get; set; }

    public IEnumerable<ushort> Intervals { get; set; } = Enumerable.Empty<ushort>();
}

#region Exchanges

public class RabbitMqExchangesSettings
{
    public ExchangeSetting Data { get; set; } = default!;
}

public class ExchangeSetting
{
    public string Key { get; set; } = default!;

    public ExchangeValue Value { get; set; } = default!;
}

public class ExchangeValue
{
    public Exchange Init { get; set; } = default!;

    public Exchange Require { get; set; } = default!;
}

public class Exchange
{
    public string Name { get; set; } = default!;

    public string Type { get; set; } = default!;

    public string? RoutingKey { get; set; }
}

#endregion Exchanges
