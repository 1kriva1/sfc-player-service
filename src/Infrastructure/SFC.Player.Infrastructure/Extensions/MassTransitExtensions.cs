using System;
using System.Globalization;
using System.Reflection;
using System.Windows.Input;

using MassTransit;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using SFC.Identity.Messages.Commands;
using SFC.Identity.Messages.Commands.User;
using SFC.Identity.Messages.Events;
using SFC.Player.Infrastructure.Constants;
using SFC.Player.Infrastructure.Settings.RabbitMq;
using SFC.Player.Messages.Commands.Common;
using SFC.Player.Messages.Commands.Data;
using SFC.Player.Messages.Commands.Player;
using SFC.Player.Messages.Events.Player.General;

namespace SFC.Player.Infrastructure.Extensions;
public static class MassTransitExtensions
{
    private const string EXCHANGE_ENDPOINT_SHORT_ADDRESS = "exchange";
    private const string EXCHANGE_ENDPOINT_AUTO_DELETE_PART = "autodelete";

    #region Public

    public static IServiceCollection AddMassTransit(this WebApplicationBuilder builder)
    {
        return builder.Services.AddMassTransit(masTransitConfigure =>
        {
            masTransitConfigure.AddConsumers(Assembly.GetExecutingAssembly());

            masTransitConfigure.UsingRabbitMq((context, rabbitMqConfigure) =>
            {
                RabbitMqSettings settings = builder.Configuration.GetRabbitMqSettings();

                string rabbitMqConnectionString = builder.Configuration.GetConnectionString("RabbitMq")!;

                rabbitMqConfigure.Host(new Uri(rabbitMqConnectionString), settings.Name, h =>
                {
                    h.Username(settings.Username);
                    h.Password(settings.Password);
                });

                rabbitMqConfigure.UseRetries(settings.Retry);

                rabbitMqConfigure.AddExchanges(builder.Environment, settings.Exchanges);

                rabbitMqConfigure.ConfigureEndpoints(context);

                MapEndpoints(settings.Exchanges, builder.Environment);
            });
        });
    }

    public static string BuildExchangeRoutingKey(this string initiator, string key)
        => $"{key.ToLower(CultureInfo.CurrentCulture)}.{initiator.ToString().ToLower(CultureInfo.CurrentCulture)}";

    #endregion Public

    #region Private

    private static void AddExchanges(
        this IRabbitMqBusFactoryConfigurator configure,
        IWebHostEnvironment environment,
        RabbitMqExchangesSettings exchangesSettings)
    {
        // "sfc.player.created"
        configure.AddExchange<PlayerCreated>(exchangesSettings.Player.Value.Domain.Player.Events.Created);

        // "sfc.player.updated"
        configure.AddExchange<PlayerUpdated>(exchangesSettings.Player.Value.Domain.Player.Events.Updated);

        if (environment.IsDevelopment())
        {
            // "sfc.player.players.seed"
            configure.AddExchange<SeedPlayers>(exchangesSettings.Player.Value.Domain.Player.Seed.Seed, exchangesSettings.Player.Key);

            // "sfc.player.players.seeded"
            configure.AddExchange<PlayersSeeded>(exchangesSettings.Player.Value.Domain.Player.Seed.Seeded);            
        }

        // exclude base command
        configure.Publish<InitiatorCommand>(p => p.Exclude = true);
    }

    private static void MapEndpoints(RabbitMqExchangesSettings exchangesSettings, IWebHostEnvironment environment)
    {
        // "sfc.player.data.require"
        EndpointConvention.Map<RequireData>(exchangesSettings.Player.Value.Data.Dependent.Data.RequireInitialize.GetExchangeEndpointUri());

        if (environment.IsDevelopment())
        {
            // "sfc.identity.users.seed.require"
            EndpointConvention.Map<RequireUsersSeed>(exchangesSettings.Identity.Value.Domain.User.Seed.RequireSeed.GetExchangeEndpointUri());
        }
    }

    private static void AddExchange<T>(this IRabbitMqBusFactoryConfigurator configure, Exchange exchange) where T : class
    {
        configure.Message<T>(x => x.SetEntityName(exchange.Name));
        configure.Publish<T>(x =>
        {
            x.AutoDelete = true;
            x.ExchangeType = exchange.Type;
        });
    }

    private static void AddExchange<T>(this IRabbitMqBusFactoryConfigurator configure, Exchange exchange, string key)
        where T : InitiatorCommand
    {
        configure.Message<T>(x => x.SetEntityName(exchange.Name));
        configure.Send<T>(x => x.UseRoutingKeyFormatter(context => context.Message.Initiator.BuildExchangeRoutingKey(key)));
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

    private static Uri GetExchangeEndpointUri(this Message exchange) =>
        new($"{EXCHANGE_ENDPOINT_SHORT_ADDRESS}:{exchange.Name}?{EXCHANGE_ENDPOINT_AUTO_DELETE_PART}={true}");

    #endregion Private
}
