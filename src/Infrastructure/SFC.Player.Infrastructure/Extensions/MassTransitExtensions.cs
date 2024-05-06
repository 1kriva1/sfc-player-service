using System.Reflection;

using MassTransit;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using SFC.Data.Messages.Enums;
using SFC.Data.Messages.Messages;
using SFC.Player.Infrastructure.Settings;

namespace SFC.Player.Infrastructure.Extensions;
public static class MassTransitExtensions
{
    #region Public

    public static IServiceCollection AddMassTransit(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddMassTransit(masTransitConfigure =>
        {
            masTransitConfigure.AddConsumers(Assembly.GetExecutingAssembly());

            masTransitConfigure.UsingRabbitMq((context, rabbitMqConfigure) =>
            {
                RabbitMqSettings settings = configuration.GetRabbitMqSettings();

                string rabbitMqConnectionString = configuration.GetConnectionString("RabbitMq")!;

                rabbitMqConfigure.Host(new Uri(rabbitMqConnectionString), settings.Name, h =>
                {
                    h.Username(settings.Username);
                    h.Password(settings.Password);
                });

                rabbitMqConfigure.UseRetries(settings.Retry);

                rabbitMqConfigure.AddExchanges(settings.Exchanges);

                rabbitMqConfigure.ConfigureEndpoints(context);
            });
        });
    }
    public static string BuildExchangeRoutingKey(this DataInitiator initiator, string key) => $"{key.ToLower()}.{initiator.ToString().ToLower()}";

    #endregion Public

    #region Private

    private static void AddExchanges(this IRabbitMqBusFactoryConfigurator configure, RabbitMqExchangesSettings exchangesSettings)
    {
        configure.AddExchange<DataRequireMessage>(exchangesSettings.Data.Value.Require);
    }

    private static void AddExchange<T>(this IRabbitMqBusFactoryConfigurator configure, Exchange exchange) where T : DataRequireMessage
    {
        configure.Message<T>(x => x.SetEntityName(exchange.Name));
        configure.Send<T>(x => x.UseRoutingKeyFormatter(context => exchange.RoutingKey));
        configure.Publish<T>(x =>
        {
            x.AutoDelete = true;
            x.ExchangeType = exchange.Type;
        });
    }

    private static void UseRetries(this IRabbitMqBusFactoryConfigurator configure, RabbitMqRetrySettings settings)
    {
        configure.UseDelayedRedelivery(r =>
            r.Intervals(settings.Intervals.Select(i => TimeSpan.FromMinutes(i)).ToArray()));
        configure.UseMessageRetry(r => r.Immediate(settings.Limit));
    }

    #endregion Private
}
