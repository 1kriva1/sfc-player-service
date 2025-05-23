namespace SFC.Player.Infrastructure.Settings.RabbitMq.Exchanges.Common.Domain;
public class DomainExchange<T>
{
    public T Events { get; set; } = default!;

    public DomainSeedExchange Seed { get; set; } = default!;
}