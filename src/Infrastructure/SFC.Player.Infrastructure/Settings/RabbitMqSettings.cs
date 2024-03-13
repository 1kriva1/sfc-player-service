namespace SFC.Player.Infrastructure.Settings;
public class RabbitMqSettings
{
    public const string SECTION_KEY = "RabbitMq";

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Name { get; set; } = null!;

    public RabbitMqRetrySettings Retry { get; set; } = default!;
}

public class RabbitMqRetrySettings
{
    public ushort Limit { get; set; }

    public IEnumerable<ushort> Intervals { get; set; } = Enumerable.Empty<ushort>();
}
