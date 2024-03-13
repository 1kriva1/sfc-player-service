using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using SFC.Player.Domain.Entities;
using MediatR;
using Moq;
using SFC.Player.Infrastructure.Persistence.Interceptors;
using SFC.Player.Application.Interfaces.Identity;
using SFC.Player.Application.Interfaces.Common;
using SFC.Player.Domain.Entities.Data;
using PlayerEntity = SFC.Player.Domain.Entities.Player;

namespace SFC.Player.Infrastructure.Persistence.UnitTests.Interceptors;
public class AuditableEntitySaveChangesInterceptorTests
{
    private readonly Mock<IUserService> userServiceMock = new();
    private readonly Mock<IDateTimeService> dateTimeServiceMock = new();
    private readonly DbContextOptions<PlayerDbContext> dbContextOptions;

    public AuditableEntitySaveChangesInterceptorTests()
    {
        dbContextOptions = new DbContextOptionsBuilder<PlayerDbContext>()
            .UseInMemoryDatabase($"PlayersDbContextTestsDb_{DateTime.Now.ToFileTimeUtc()}")
            .Options;
    }

    [Fact]
    [Trait("Persistence", "Interceptor")]
    public async Task Persistence_Interceptor_ShouldFillAuditableFieldsForEntityCreation()
    {
        // Arrange
        Guid userId = Guid.NewGuid();
        DateTime now = DateTime.UtcNow;
        userServiceMock.Setup(m => m.UserId).Returns(userId);
        dateTimeServiceMock.Setup(m => m.Now).Returns(now);
        PlayerEntity entity = new()
        {
            GeneralProfile = new PlayerGeneralProfile
            {
                FirstName = "First Name",
                LastName = "Last Name",
                City = "City"
            }
        };
        PlayerDbContext context = CreateDbContext();

        // Act
        EntityEntry<PlayerEntity> addResult = await context.Players.AddAsync(entity);
        await context.SaveChangesAsync();
        PlayerEntity? player = await context.Players.FindAsync(addResult.Entity.Id);

        // Assert
        Assert.Equal(userId, player?.CreatedBy);
        Assert.Equal(userId, player?.LastModifiedBy);
        Assert.Equal(now, player?.CreatedDate);
        Assert.Equal(now, player?.LastModifiedDate);
    }

    [Fact]
    [Trait("Persistence", "Interceptor")]
    public async Task Persistence_DbContext_ShouldFillAuditableFieldsForEntityModification()
    {
        // Arrange
        Guid userId = Guid.NewGuid();
        DateTime now = DateTime.UtcNow;
        userServiceMock.Setup(m => m.UserId).Returns(userId);
        dateTimeServiceMock.Setup(m => m.Now).Returns(now);
        PlayerEntity entity = new()
        {
            GeneralProfile = new PlayerGeneralProfile
            {
                FirstName = "First Name",
                LastName = "Last Name",
                City = "City"
            }
        };
        PlayerDbContext context = CreateDbContext();

        // Act
        EntityEntry<PlayerEntity> addResult = await context.Players.AddAsync(entity);
        await context.SaveChangesAsync();

        Guid userIdUpdated = Guid.NewGuid();
        DateTime nowUpdated = DateTime.UtcNow;
        userServiceMock.Setup(m => m.UserId).Returns(userIdUpdated);
        dateTimeServiceMock.Setup(m => m.Now).Returns(nowUpdated);
        entity.GeneralProfile.FirstName = "New First Name";
        context.Entry(entity).State = EntityState.Modified;
        await context.SaveChangesAsync();

        PlayerEntity? player = await context.Players.FindAsync(addResult.Entity.Id);

        // Assert
        Assert.Equal(userId, player?.CreatedBy);
        Assert.Equal(userIdUpdated, player?.LastModifiedBy);
        Assert.Equal(now, player?.CreatedDate);
        Assert.Equal(nowUpdated, player?.LastModifiedDate);
    }

    [Fact]
    [Trait("Persistence", "Interceptor")]
    public async Task Persistence_Interceptor_ShouldFillBaseDataEntity()
    {
        // Arrange
        DateTime now = DateTime.UtcNow;
        dateTimeServiceMock.Setup(m => m.Now).Returns(now);
        FootballPosition entity = new() { Id = 0, Title = "Goalkeeper" };
        PlayerDbContext context = CreateDbContext();

        // Act
        EntityEntry<FootballPosition> addResult = await context.FootballPositions.AddAsync(entity);
        await context.SaveChangesAsync();
        FootballPosition? player = await context.FootballPositions.FindAsync(addResult.Entity.Id);

        // Assert
        Assert.Equal(now, player?.CreatedDate);
    }

    private PlayerDbContext CreateDbContext()
    {
        Mock<IMediator> mediatorMock = new();
        AuditableEntitySaveChangesInterceptor interceptor = new(userServiceMock.Object, dateTimeServiceMock.Object);

        return new(dbContextOptions, mediatorMock.Object, interceptor);
    }
}
