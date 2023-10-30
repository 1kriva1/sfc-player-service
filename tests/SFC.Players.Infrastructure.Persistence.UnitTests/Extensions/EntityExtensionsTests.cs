using Microsoft.EntityFrameworkCore;

using Moq;

using SFC.Players.Application.Interfaces.Common;
using SFC.Players.Application.Interfaces.Identity;
using SFC.Players.Domain.Common;
using SFC.Players.Domain.Entities;
using SFC.Players.Infrastructure.Persistence.Interceptors;
using SFC.Players.Infrastructure.Persistence.Extensions;
using MediatR;
using SFC.Players.Domain.Entities.Data;

namespace SFC.Players.Infrastructure.Persistence.UnitTests.Extensions;
public class EntityExtensionsTests
{
    public static Guid USER_ID = Guid.Parse("{6313179F-7837-473A-A4D5-A5571B43E6A6}");

    private readonly Mock<IUserService> _userServiceMock = new();
    private readonly Mock<IDateTimeService> _dateTimeServiceMock = new();
    private readonly DbContextOptions<PlayersDbContext> dbContextOptions;

    public EntityExtensionsTests()
    {
        _userServiceMock.Setup(m => m.UserId).Returns(USER_ID);
        dbContextOptions = new DbContextOptionsBuilder<PlayersDbContext>()
            .UseInMemoryDatabase($"EntityExtensionsTestsDb_{DateTime.Now.ToFileTimeUtc()}")
            .Options;
    }

    [Fact]
    [Trait("Persistence", "Extensions")]
    public async Task Persistence_Extensions_Entity_ShouldSetAuditableBaseEntityForCreation()
    {
        // Arrange
        DateTime now = DateTime.UtcNow;
        long playerId = 1;
        _dateTimeServiceMock.Setup(m => m.Now).Returns(now);
        PlayersDbContext context = CreateDbContext();
        await context.Players.AddAsync(new Player
        {
            Id = playerId,
            GeneralProfile = new PlayerGeneralProfile()
            {
                FirstName = "First Name",
                LastName = "Last Name",
                City = "City"
            }
        });

        // Act
        context.ChangeTracker.Entries<BaseAuditableEntity>()
           .SetAuditable(_userServiceMock.Object, _dateTimeServiceMock.Object);
        await context.SaveChangesAsync();

        // Assert
        Player player = (await context.Players.FindAsync(playerId))!;

        Assert.Equal(USER_ID, player.CreatedBy);
        Assert.Equal(now, player.CreatedDate);
        Assert.Equal(USER_ID, player.LastModifiedBy);
        Assert.Equal(now, player.LastModifiedDate);
    }

    [Fact]
    [Trait("Persistence", "Extensions")]
    public async Task Persistence_Extensions_Entity_ShouldSetAuditableBaseEntityForModification()
    {
        // Arrange
        DateTime now = DateTime.UtcNow;
        long playerId = 1;
        _dateTimeServiceMock.Setup(m => m.Now).Returns(now);
        PlayersDbContext context = CreateDbContext();
        await context.Players.AddAsync(new Player
        {
            Id = playerId,
            GeneralProfile = new PlayerGeneralProfile()
            {
                FirstName = "First Name",
                LastName = "Last Name",
                City = "City"
            }
        });

        // Act
        context.ChangeTracker.Entries<BaseAuditableEntity>()
           .SetAuditable(_userServiceMock.Object, _dateTimeServiceMock.Object);
        await context.SaveChangesAsync();

        Guid userForUpdate = Guid.NewGuid();
        DateTime nowForUpdate = DateTime.UtcNow;
        _dateTimeServiceMock.Setup(m => m.Now).Returns(nowForUpdate);
        _userServiceMock.Setup(m => m.UserId).Returns(userForUpdate);

        Player player = (await context.Players.FindAsync(playerId))!;
        player.GeneralProfile.FirstName = "New First Name";

        context.Players.Update(player);
        await context.SaveChangesAsync();

        // Assert
        Player assertPlayer = (await context.Players.FindAsync(playerId))!;

        Assert.Equal(USER_ID, assertPlayer.CreatedBy);
        Assert.Equal(now, assertPlayer.CreatedDate);
        Assert.Equal(userForUpdate, assertPlayer.LastModifiedBy);
        Assert.Equal(nowForUpdate, assertPlayer.LastModifiedDate);
    }

    [Fact]
    [Trait("Persistence", "Extensions")]
    public async Task Persistence_Extensions_Entity_ShouldSetAuditableBaseDataEntity()
    {
        // Arrange
        DateTime now = DateTime.UtcNow;
        int positionId = 1;
        _dateTimeServiceMock.Setup(m => m.Now).Returns(now);
        PlayersDbContext context = CreateDbContext();
        await context.FootballPositions.AddAsync(new FootballPosition { Id = positionId, Title = "Goalkeeper" });

        // Act
        context.ChangeTracker.Entries<BaseDataEntity>()
           .SetAuditable(_dateTimeServiceMock.Object);
        await context.SaveChangesAsync();

        // Assert
        FootballPosition player = (await context.FootballPositions.FindAsync(positionId))!;

        Assert.Equal(now, player.CreatedDate);
    }

    private PlayersDbContext CreateDbContext()
    {
        Mock<IMediator> mediatorMock = new();
        AuditableEntitySaveChangesInterceptor interceptor = new(_userServiceMock.Object, _dateTimeServiceMock.Object);

        return new(dbContextOptions, mediatorMock.Object, interceptor);
    }
}
