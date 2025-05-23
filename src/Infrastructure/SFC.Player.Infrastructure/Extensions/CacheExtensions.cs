using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using SFC.Player.Application.Common.Settings;
using SFC.Player.Application.Interfaces.Cache;
using SFC.Player.Infrastructure.Cache;

namespace SFC.Player.Infrastructure.Extensions;
public static class CacheExtensions
{
    public static IServiceCollection AddCache(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<CacheSettings>(configuration.GetSection(CacheSettings.SectionKey));
        services.AddScoped<ICache, RedisCache>();
        return services;
    }
}
