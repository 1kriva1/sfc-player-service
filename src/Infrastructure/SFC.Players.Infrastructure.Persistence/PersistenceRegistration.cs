using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using SFC.Players.Application.Interfaces.Persistence;
using SFC.Players.Infrastructure.Persistence.Interceptors;
using SFC.Players.Infrastructure.Persistence.Repositories;

namespace SFC.Players.Infrastructure.Persistence;

public static class PersistenceRegistration
{
    public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<PlayersDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("PlayersConnectionString"),
            b => b.MigrationsAssembly(typeof(PlayersDbContext).Assembly.FullName)));
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();
        services.AddScoped<IPlayersDbContext, PlayersDbContext>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped(typeof(IDataRepository<>), typeof(DataRepository<>));
        services.AddScoped<IPlayerRepository, PlayerRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IStatCategoryRepository, StatCategoryRepository>();
        services.AddScoped<IStatTypeRepository, StatTypeRepository>();
    }
}
