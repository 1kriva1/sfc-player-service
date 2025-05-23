using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using SFC.Player.Application.Common.Settings;
using SFC.Player.Application.Interfaces.Persistence.Repository.Common;
using SFC.Player.Application.Interfaces.Persistence.Repository.Data;
using SFC.Player.Application.Interfaces.Persistence.Repository.Identity;
using SFC.Player.Application.Interfaces.Persistence.Repository.Metadata;
using SFC.Player.Application.Interfaces.Persistence.Repository.Player;
using SFC.Player.Infrastructure.Persistence.Repositories.Common;
using SFC.Player.Infrastructure.Persistence.Repositories.Data;
using SFC.Player.Infrastructure.Persistence.Repositories.Data.Cache;
using SFC.Player.Infrastructure.Persistence.Repositories.Identity;
using SFC.Player.Infrastructure.Persistence.Repositories.Metadata;
using SFC.Player.Infrastructure.Persistence.Repositories.Player;

namespace SFC.Player.Infrastructure.Persistence.Extensions;
public static class RepositoryExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(typeof(IRepository<,,>), typeof(Repository<,,>));
        services.AddScoped<IMetadataRepository, MetadataRepository>();
        services.AddScoped<IPlayerRepository, PlayerRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        CacheSettings? cacheSettings = configuration
           .GetSection(CacheSettings.SectionKey)
           .Get<CacheSettings>();

        if (cacheSettings?.Enabled ?? false)
        {
            // data
            services.AddScoped<FootballPositionRepository>();
            services.AddScoped<GameStyleRepository>();
            services.AddScoped<StatCategoryRepository>();
            services.AddScoped<StatSkillRepository>();
            services.AddScoped<StatTypeRepository>();
            services.AddScoped<WorkingFootRepository>();
            services.AddScoped<IFootballPositionRepository, FootballPositionCacheRepository>();
            services.AddScoped<IGameStyleRepository, GameStyleCacheRepository>();
            services.AddScoped<IStatCategoryRepository, StatCategoryCacheRepository>();
            services.AddScoped<IStatSkillRepository, StatSkillCacheRepository>();
            services.AddScoped<IStatTypeRepository, StatTypeCacheRepository>();
            services.AddScoped<IWorkingFootRepository, WorkingFootCacheRepository>();
        }
        else
        {
            // data
            services.AddScoped<IFootballPositionRepository, FootballPositionRepository>();
            services.AddScoped<IGameStyleRepository, GameStyleRepository>();
            services.AddScoped<IStatCategoryRepository, StatCategoryRepository>();
            services.AddScoped<IStatSkillRepository, StatSkillRepository>();
            services.AddScoped<IStatTypeRepository, StatTypeRepository>();
            services.AddScoped<IWorkingFootRepository, WorkingFootRepository>();
        }

        return services;
    }
}
