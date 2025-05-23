using System.Reflection;
using Microsoft.EntityFrameworkCore;
using SFC.Player.Infrastructure.Persistence.Constants;
using SFC.Player.Infrastructure.Persistence.Interceptors;
using SFC.Player.Application.Interfaces.Persistence.Context;
using SFC.Player.Infrastructure.Persistence.Configurations.Identity;
using SFC.Player.Domain.Entities.Player;
using SFC.Player.Infrastructure.Persistence.Configurations.Data;
using SFC.Player.Infrastructure.Persistence.Configurations.Player;
using SFC.Player.Infrastructure.Persistence.Configurations.Metadata;
using Microsoft.Extensions.Hosting;

namespace SFC.Player.Infrastructure.Persistence.Contexts;
public class PlayerDbContext(
    DbContextOptions<PlayerDbContext> options,
    IHostEnvironment hostEnvironment,
    AuditableEntitySaveChangesInterceptor auditableInterceptor,
    UserEntitySaveChangesInterceptor userEntityInterceptor,
    DispatchDomainEventsSaveChangesInterceptor eventsInterceptor)
    : BaseDbContext<PlayerDbContext>(options, eventsInterceptor), IPlayerDbContext
{
    private readonly IHostEnvironment _hostEnvironment = hostEnvironment;
    private readonly AuditableEntitySaveChangesInterceptor _auditableInterceptor = auditableInterceptor;
    private readonly UserEntitySaveChangesInterceptor _userEntityInterceptor = userEntityInterceptor;

    public IQueryable<PlayerEntity> Players => Set<PlayerEntity>();

    public IQueryable<PlayerGeneralProfile> GeneralProfiles => Set<PlayerGeneralProfile>();

    public IQueryable<PlayerFootballProfile> FootballProfiles => Set<PlayerFootballProfile>();

    public IQueryable<PlayerAvailability> Availability => Set<PlayerAvailability>();

    public IQueryable<PlayerAvailableDay> AvailableDays => Set<PlayerAvailableDay>();

    public IQueryable<PlayerStatPoints> Points => Set<PlayerStatPoints>();

    public IQueryable<PlayerStat> Stats => Set<PlayerStat>();

    public IQueryable<PlayerTag> Tags => Set<PlayerTag>();

    public IQueryable<PlayerPhoto> Photos => Set<PlayerPhoto>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.HasDefaultSchema(DatabaseConstants.DefaultSchemaName);

        MetadataDbContext.ApplyMetadataConfigurations(modelBuilder, _hostEnvironment.IsDevelopment());

        DataDbContext.ApplyDataConfigurations(modelBuilder);

        IdentityDbContext.ApplyIdentityConfigurations(modelBuilder, Database.IsSqlServer());

        ApplyPlayerConfigurations(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableInterceptor);
        optionsBuilder.AddInterceptors(_userEntityInterceptor);
        base.OnConfiguring(optionsBuilder);
    }

    private static void ApplyPlayerConfigurations(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PlayerAvailabilityConfiguration());

        modelBuilder.ApplyConfiguration(new PlayerAvailableDayConfiguration());

        modelBuilder.ApplyConfiguration(new PlayerConfiguration());

        modelBuilder.ApplyConfiguration(new PlayerFootballProfileConfiguration());

        modelBuilder.ApplyConfiguration(new PlayerGeneralProfileConfiguration());

        modelBuilder.ApplyConfiguration(new PlayerPhotoConfiguration());

        modelBuilder.ApplyConfiguration(new PlayerPointsConfiguration());

        modelBuilder.ApplyConfiguration(new PlayerStatConfiguration());

        modelBuilder.ApplyConfiguration(new PlayerTagConfiguration());
    }
}
