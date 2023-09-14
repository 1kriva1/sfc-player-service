using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using SFC.Players.Domain.Entities;
using MediatR;
using Moq;
using SFC.Players.Infrastructure.Persistence.Interceptors;
using SFC.Players.Application.Interfaces.Identity;
using SFC.Players.Application.Interfaces.Common;

namespace SFC.Players.Infrastructure.Persistence.UnitTests.Interceptors;
public class AuditableEntitySaveChangesInterceptorTests
{
    private readonly Mock<IUserService> userServiceMock = new();
    private readonly Mock<IDateTimeService> dateTimeServiceMock = new();
    private readonly DbContextOptions<PlayersDbContext> dbContextOptions;

    public AuditableEntitySaveChangesInterceptorTests()
    {
        dbContextOptions = new DbContextOptionsBuilder<PlayersDbContext>()
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
        Player entity = new()
        {
            GeneralProfile = new PlayerGeneralProfile
            {
                FirstName = "First Name",
                LastName = "Last Name",
                City = "City"
            }
        };
        PlayersDbContext context = CreateDbContext();

        // Act
        EntityEntry<Player> addResult = await context.Players.AddAsync(entity);
        await context.SaveChangesAsync();
        Player? player = await context.Players.FindAsync(addResult.Entity.Id);

        // Assert
        Assert.Equal(userId, player?.CreatedBy);
        Assert.Equal(userId, player?.LastModifiedBy);
        Assert.Equal(now, player?.CreatedDate);
        Assert.Equal(now, player?.LastModifiedDate);
    }

    [Fact]
    [Trait("Persistence", "DbContext")]
    public async Task Persistence_DbContext_ShouldFillAuditableFieldsForEntityModification()
    {
        // Arrange
        Guid userId = Guid.NewGuid();
        DateTime now = DateTime.UtcNow;
        userServiceMock.Setup(m => m.UserId).Returns(userId);
        dateTimeServiceMock.Setup(m => m.Now).Returns(now);
        Player entity = new()
        {
            GeneralProfile = new PlayerGeneralProfile
            {
                FirstName = "First Name",
                LastName = "Last Name",
                City = "City"
            }
        };
        PlayersDbContext context = CreateDbContext();

        // Act
        EntityEntry<Player> addResult = await context.Players.AddAsync(entity);
        await context.SaveChangesAsync();

        Guid userIdUpdated = Guid.NewGuid();
        DateTime nowUpdated = DateTime.UtcNow;
        userServiceMock.Setup(m => m.UserId).Returns(userIdUpdated);
        dateTimeServiceMock.Setup(m => m.Now).Returns(nowUpdated);
        entity.GeneralProfile.FirstName = "New First Name";
        context.Entry(entity).State = EntityState.Modified;
        await context.SaveChangesAsync();

        Player? player = await context.Players.FindAsync(addResult.Entity.Id);

        // Assert
        Assert.Equal(userId, player?.CreatedBy);
        Assert.Equal(userIdUpdated, player?.LastModifiedBy);
        Assert.Equal(now, player?.CreatedDate);
        Assert.Equal(nowUpdated, player?.LastModifiedDate);
    }

    private PlayersDbContext CreateDbContext()
    {
        Mock<IMediator> mediatorMock = new();
        AuditableEntitySaveChangesInterceptor interceptor = new(userServiceMock.Object, dateTimeServiceMock.Object);

        return new(dbContextOptions, mediatorMock.Object, interceptor);
    }
}
