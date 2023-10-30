using System.Reflection;

using MassTransit;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using SFC.Data.Contracts.Configuration;
using SFC.Data.Contracts.Events;
using SFC.Players.Infrastructure.Settings;

namespace SFC.Players.Infrastructure.Extensions;
public static class MassTransitExtensions
{
    public static IServiceCollection AddMassTransit(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddMassTransit(masTransitConfigure =>
        {
            masTransitConfigure.AddConsumers(Assembly.GetExecutingAssembly());

            masTransitConfigure.UsingRabbitMq((context, rabbitMqConfigure) =>
            {
                RabbitMqSettings settings = configuration
                    .GetSection(RabbitMqSettings.SECTION_KEY)
                    .Get<RabbitMqSettings>()!;

                rabbitMqConfigure.Host(settings.Host, settings.Port, "/", settings.Name, h =>
                {
                    h.Username(settings.Username);
                    h.Password(settings.Password);
                });

                rabbitMqConfigure.UseRetries(settings.Retry);

                rabbitMqConfigure.AddExchange<DataRequireEvent>();                

                rabbitMqConfigure.ConfigureEndpoints(context);
            });
        });
    }

    private static void AddExchange<T>(this IRabbitMqBusFactoryConfigurator configure) where T : class
    {
        Exchange exchange = Exchange.List.First(exch => exch.Key == typeof(T)).Value;

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
}
