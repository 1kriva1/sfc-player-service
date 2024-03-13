using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using SFC.Player.Application.Common.Settings;
using SFC.Player.Application.Interfaces.Persistence;
using SFC.Player.Infrastructure.Persistence.Repositories;
using SFC.Player.Infrastructure.Persistence.Repositories.Data;

namespace SFC.Player.Infrastructure.Persistence.Extensions;
public static class RepositoryExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
        services.AddScoped<IPlayerRepository, PlayerRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        CacheSettings? settings = configuration
           .GetSection(CacheSettings.SECTION_KEY)
           .Get<CacheSettings>();

        if (settings?.Enabled ?? false)
        {
            services.AddScoped(typeof(DataRepository<>));
            services.AddScoped<StatTypeRepository>();
            services.AddScoped(typeof(IDataRepository<>), typeof(DataCacheRepository<>));            
            services.AddScoped<IStatTypeRepository, StatTypeCacheRepository>();
        }
        else
        {
            services.AddScoped(typeof(IDataRepository<>), typeof(DataRepository<>));
            services.AddScoped<IStatTypeRepository, StatTypeRepository>();
        }

        return services;
    }
}
