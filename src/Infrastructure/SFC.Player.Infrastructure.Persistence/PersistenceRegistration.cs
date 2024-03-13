using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using SFC.Player.Application.Interfaces.Persistence;
using SFC.Player.Infrastructure.Persistence.Extensions;
using SFC.Player.Infrastructure.Persistence.Interceptors;

namespace SFC.Player.Infrastructure.Persistence;

public static class PersistenceRegistration
{
    public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<PlayerDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Database"),
            b => b.MigrationsAssembly(typeof(PlayerDbContext).Assembly.FullName)));
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();
        services.AddScoped<IPlayersDbContext, PlayerDbContext>();
        services.AddRepositories(configuration);
    }
}
