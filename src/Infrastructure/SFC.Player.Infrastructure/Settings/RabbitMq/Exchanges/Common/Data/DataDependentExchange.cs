namespace SFC.Player.Infrastructure.Settings.RabbitMq.Exchanges.Common.Data;

public class DataDependentExchange
{
    public Exchange Initialize { get; set; } = default!;

    public Message RequireInitialize { get; set; } = default!;
}