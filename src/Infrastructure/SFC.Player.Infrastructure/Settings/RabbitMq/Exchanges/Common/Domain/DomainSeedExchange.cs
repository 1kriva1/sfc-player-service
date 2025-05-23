namespace SFC.Player.Infrastructure.Settings.RabbitMq.Exchanges.Common.Domain;

public class DomainSeedExchange
{
    public Exchange Seeded { get; set; } = default!;

    public Exchange Seed { get; set; } = default!;

    public Message RequireSeed { get; set; } = default!;
}