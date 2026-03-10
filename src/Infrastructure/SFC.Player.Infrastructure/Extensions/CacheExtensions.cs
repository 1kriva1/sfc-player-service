using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using SFC.Player.Application.Common.Settings;
using SFC.Player.Application.Interfaces.Cache;
using SFC.Player.Infrastructure.Cache;
using SFC.Player.Infrastructure.Persistence.Constants;

namespace SFC.Player.Infrastructure.Extensions;
public static class CacheExtensions
{
    public static IServiceCollection AddCache(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<CacheSettings>(configuration.GetSection(CacheSettings.SectionKey));

        services.AddScoped<ICache, RedisCache>();

        services.AddRelatedCacheInstances(configuration);

        return services;
    }

    private static void AddRelatedCacheInstances(this IServiceCollection services, IConfiguration configuration)
    {
        CacheSettings settings = configuration.GetCacheSettings();

        foreach (string relatedCache in settings.RelatedInstances)
        {
            services.AddRelatedCache(relatedCache, configuration);
        }
    }

    private static void AddRelatedCache(this IServiceCollection services, string instanceName, IConfiguration configuration)
    {
        switch (instanceName)
        {
            case CacheInstance.Data:
                services.AddCacheInstance<RedisDataCache>(CacheInstance.Data, configuration);
                break;
        }
    }

    private static void AddCacheInstance<T>(this IServiceCollection services, string instanceName, IConfiguration configuration)
        where T : class, ICache
    {
        services.AddKeyedScoped<ICache, T>(instanceName);

        services.AddKeyedSingleton<IDistributedCache>(instanceName,
            (services, _) => RedisExtensions.GetRedisCache(instanceName, configuration));
    }
}