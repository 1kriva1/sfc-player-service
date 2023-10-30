using MediatR;

using Microsoft.EntityFrameworkCore;

using Moq;

using SFC.Players.Application.Interfaces.Common;
using SFC.Players.Application.Interfaces.Identity;
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
    private PlayersDbContext CreateDbContext()
    {
        Mock<IUserService> userServiceMock = new();
        Mock<IDateTimeService> dateTimeServiceMock = new();
        Mock<IMediator> mediatorMock = new();
        AuditableEntitySaveChangesInterceptor interceptor = new(userServiceMock.Object, dateTimeServiceMock.Object);

        return new(dbContextOptions, mediatorMock.Object, interceptor);
    }
}
