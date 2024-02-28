using MediatR;

using Microsoft.EntityFrameworkCore;

using Moq;

using SFC.Players.Application.Interfaces.Common;
using SFC.Players.Application.Interfaces.Identity;
using SFC.Players.Domain.Entities;
using SFC.Players.Domain.Entities.Data;
using SFC.Players.Infrastructure.Persistence.Extensions;
using SFC.Players.Infrastructure.Persistence.Interceptors;

namespace SFC.Players.Infrastructure.Persistence.UnitTests.Extensions;
public class ContextExtensionsTests
{
    private readonly DbContextOptions<PlayersDbContext> dbContextOptions;

    public ContextExtensionsTests()
    {
        dbContextOptions = new DbContextOptionsBuilder<PlayersDbContext>()
            .UseInMemoryDatabase($"ContextExtensionsTestsDb_{DateTime.Now.ToFileTimeUtc()}")
            .Options;
    }

    [Fact]
    [Trait("Persistence", "Extensions")]
    public async Task Persistence_Extensions_Context_ShouldClearAllTable()
    {
        // Arrange
        DbContext context = CreateDbContext();
        PlayersDbContext playerDbContext = (PlayersDbContext)context;
        await playerDbContext.FootballPositions.AddAsync(new FootballPosition { Id = 0, Title = "Goalkeeper" });
        await context.SaveChangesAsync();

        Assert.True(await playerDbContext.FootballPositions.AnyAsync());

        // Act
        context.Clear<FootballPosition>();
        await context.SaveChangesAsync();

        // Assert
        Assert.False(await playerDbContext.FootballPositions.AnyAsync());
    }

    [Fact]
    [Trait("Persistence", "Extensions")]
    public async Task Persistence_Extensions_Context_ShouldClearEmptyTable()
    {
        // Arrange
        DbContext context = CreateDbContext();
        PlayersDbContext playerDbContext = (PlayersDbContext)context;

        Assert.False(await playerDbContext.FootballPositions.AnyAsync());

        // Act
        context.Clear<FootballPosition>();
        await context.SaveChangesAsync();

        // Assert
        Assert.False(await playerDbContext.FootballPositions.AnyAsync());
    }

    [Fact]
    [Trait("Persistence", "Extensions")]
    public void Persistence_Extensions_Context_ShouldSetStateForPlayerStatType()
    {
        // Arrange
        DbContext context = CreateDbContext();
        PlayersDbContext playerDbContext = (PlayersDbContext)context;
        ICollection<PlayerStat> stats = new List<PlayerStat> {
            new() {
                Id = 1,
                Value = 50,
                Type = new StatType()
            }
        };

        // Act
        context.SetPlayerStats(stats);

        // Assert
        foreach (PlayerStat stat in stats)
        {
            Assert.Equal(EntityState.Unchanged, context.Entry(stat.Type).State);
        }
    }

    [Fact]
    [Trait("Persistence", "Extensions")]
    public void Persistence_Extensions_Context_ShouldSetDefinedStateForPlayerStatType()
    {
        // Arrange
        DbContext context = CreateDbContext();
        PlayersDbContext playerDbContext = (PlayersDbContext)context;
        ICollection<PlayerStat> stats = new List<PlayerStat> {
            new() {
                Id = 1,
                Value = 50,
                Type = new StatType()
            }
        };

        // Act
        context.SetPlayerStats(stats, EntityState.Modified);

        // Assert
        foreach (PlayerStat stat in stats)
        {
            Assert.Equal(EntityState.Modified, context.Entry(stat.Type).State);
        }
    }

    private PlayersDbContext CreateDbContext()
    {
        Mock<IUserService> userServiceMock = new();
        Mock<IDateTimeService> dateTimeServiceMock = new();
        Mock<IMediator> mediatorMock = new();
        AuditableEntitySaveChangesInterceptor interceptor = new(userServiceMock.Object, dateTimeServiceMock.Object);

        return new(dbContextOptions, mediatorMock.Object, interceptor);
    }
}
