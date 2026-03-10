using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using SFC.Player.Infrastructure.Settings;

using StackExchange.Redis;

namespace SFC.Player.Infrastructure.Extensions;
public static class RedisExtensions
{
    public static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration)
    {
        RedisSettings settings = configuration.GetRedisSettings();

        return services.AddStackExchangeRedisCache(options =>
        {
            options.InstanceName = $"{settings.InstanceName}:";
            options.ConfigurationOptions = new ConfigurationOptions
            {
                EndPoints = { configuration.GetConnectionString("Redis")! },
                User = settings.User,
                Password = settings.Password
            };
        });
    }

    public static RedisCache GetRedisCache(string instanceName, IConfiguration configuration)
    {
        RedisSettings settings = configuration.GetRedisSettings();

        RedisCacheOptions redisOptions = new()
        {
            InstanceName = $"{instanceName}:",
            ConfigurationOptions = new ConfigurationOptions
            {
                EndPoints = { configuration.GetConnectionString("Redis")! },
                User = settings.User,
                Password = settings.Password
            }
        };

        return new RedisCache(Options.Create(redisOptions));
    }
}