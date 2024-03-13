using System.Reflection;

using MediatR;

using Microsoft.EntityFrameworkCore;

using SFC.Player.Application.Common.Constants;
using SFC.Player.Application.Interfaces.Persistence;
using SFC.Player.Domain.Entities;
using SFC.Player.Domain.Entities.Data;
using SFC.Player.Domain.Entities.Identity;
using SFC.Player.Infrastructure.Persistence.Extensions;
using SFC.Player.Infrastructure.Persistence.Interceptors;

using PlayerUser = SFC.Player.Domain.Entities.User;
using IdentityUser = SFC.Player.Domain.Entities.Identity.User;
using PlayerEntity = SFC.Player.Domain.Entities.Player;
using System.Reflection.Emit;
using SFC.Player.Infrastructure.Persistence.Configurations.Identity;

namespace SFC.Player.Infrastructure.Persistence;

public class PlayerDbContext : DbContext, IPlayersDbContext
{
    private readonly IMediator _mediator;
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

    public PlayerDbContext(
        DbContextOptions<PlayerDbContext> options,
        IMediator mediator,
        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor)
        : base(options)
    {
        _mediator = mediator;
        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
    }

    public DbSet<PlayerEntity> Players => Set<PlayerEntity>();

    public DbSet<PlayerGeneralProfile> GeneralProfiles => Set<PlayerGeneralProfile>();

    public DbSet<PlayerFootballProfile> FootballProfiles => Set<PlayerFootballProfile>();

    public DbSet<PlayerAvailability> Availability => Set<PlayerAvailability>();

    public DbSet<PlayerAvailableDay> AvailableDays => Set<PlayerAvailableDay>();

    public DbSet<PlayerStatPoints> Points => Set<PlayerStatPoints>();

    public DbSet<PlayerStat> Stats => Set<PlayerStat>();

    public DbSet<PlayerTag> Tags => Set<PlayerTag>();

    public DbSet<PlayerPhoto> Photos => Set<PlayerPhoto>();

    public DbSet<PlayerUser> Users => base.Set<PlayerUser>();

    public DbSet<IdentityUser> IdentityUsers => base.Set<IdentityUser>();

    public DbSet<GameStyle> GameStyles => Set<GameStyle>();

    public DbSet<FootballPosition> FootballPositions => Set<FootballPosition>();

    public DbSet<StatCategory> StatCategories => Set<StatCategory>();

    public DbSet<StatSkill> StatSkills => Set<StatSkill>();

    public DbSet<StatType> StatTypes => Set<StatType>();

    public DbSet<WorkingFoot> WorkingFoots => Set<WorkingFoot>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema(DbConstants.DEFAULT_SCHEMA_NAME);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        builder.ApplyConfiguration(new UserConfiguration(Database.IsSqlServer()));

        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEvents(this);

        return await base.SaveChangesAsync(cancellationToken);
    }
}
