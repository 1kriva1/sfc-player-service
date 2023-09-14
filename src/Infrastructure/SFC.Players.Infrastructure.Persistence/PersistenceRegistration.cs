using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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
        services.AddScoped<IPlayersDbContext>(provider => provider.GetRequiredService<PlayersDbContext>());
        services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
        services.AddScoped<IPlayerRepository, PlayerRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
    }

    public static async Task ResetDatabaseAsync(this WebApplication app)
    {
        using IServiceScope scope = app.Services.CreateScope();

        try
        {
            PlayersDbContext? context = scope.ServiceProvider.GetService<PlayersDbContext>();

            if (context != null)
            {
                await context.Database.EnsureDeletedAsync();
                await context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            ILogger logger = scope.ServiceProvider.GetRequiredService<ILogger>();

            logger.LogError(ex, "An error occurred while migrating the database.");
        }
    }
}
