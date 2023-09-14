using System.Reflection;

using MediatR;

using Microsoft.EntityFrameworkCore;

using SFC.Players.Application.Common.Constants;
using SFC.Players.Application.Interfaces.Persistence;
using SFC.Players.Domain.Entities;
using SFC.Players.Infrastructure.Persistence.Extensions;
using SFC.Players.Infrastructure.Persistence.Interceptors;

namespace SFC.Players.Infrastructure.Persistence;

public class PlayersDbContext : DbContext, IPlayersDbContext
{
    private readonly IMediator _mediator;
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

    public PlayersDbContext(
        DbContextOptions<PlayersDbContext> options,
        IMediator mediator,
        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor)
        : base(options)
    {
        _mediator = mediator;
        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
    }

    public DbSet<Player> Players => Set<Player>();

    public DbSet<PlayerGeneralProfile> GeneralProfiles => Set<PlayerGeneralProfile>();

    public DbSet<PlayerFootballProfile> FootballProfiles => Set<PlayerFootballProfile>();

    public DbSet<PlayerAvailability> Availability => Set<PlayerAvailability>();

    public DbSet<PlayerAvailableDay> AvailableDays => Set<PlayerAvailableDay>();

    public DbSet<PlayerStatPoints> Points => Set<PlayerStatPoints>();

    public DbSet<PlayerStat> Stats => Set<PlayerStat>();

    public DbSet<PlayerTag> Tags => Set<PlayerTag>();

    public DbSet<PlayerPhoto> Photos => Set<PlayerPhoto>();

    public virtual DbSet<User> Users => Set<User>();

    public DbSet<IdentityUser> IdentityUsers => Set<IdentityUser>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema(DbConstants.DEFAULT_SCHEMA_NAME);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

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
