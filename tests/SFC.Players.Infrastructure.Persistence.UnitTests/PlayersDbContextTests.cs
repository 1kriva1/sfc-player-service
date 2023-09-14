﻿using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using Moq;

using SFC.Players.Application.Common.Constants;
using SFC.Players.Application.Interfaces.Common;
using SFC.Players.Application.Interfaces.Identity;
using SFC.Players.Domain.Entities;
using SFC.Players.Domain.Enums;
using SFC.Players.Infrastructure.Persistence.Interceptors;

namespace SFC.Players.Infrastructure.Persistence.UnitTests;
public class PlayersDbContextTests
{
    private readonly Mock<IUserService> userServiceMock = new();
    private readonly Mock<IDateTimeService> dateTimeServiceMock = new();
    private readonly DbContextOptions<PlayersDbContext> dbContextOptions;

    public PlayersDbContextTests()
    {
        dbContextOptions = new DbContextOptionsBuilder<PlayersDbContext>()
            .UseInMemoryDatabase($"PlayersDbContextTestsDb_{DateTime.Now.ToFileTimeUtc()}")
            .Options;
    }

    [Fact]
    [Trait("Persistence", "DbContext")]
    public void Persistence_DbContext_ShouldHasCorrectDefaultSchema()
    {
        PlayersDbContext context = CreateDbContext();

        string? defaultSchema = context.Model.GetDefaultSchema();

        Assert.Equal(DbConstants.DEFAULT_SCHEMA_NAME, defaultSchema);
    }   

    [Fact]
    [Trait("Persistence", "DbContext")]
    public async Task Persistence_DbContext_ShouldHandlePlayerEntity()
    {
        // Arrange
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
        Player? player = await context.Players.FindAsync(addResult.Entity.Id);

        // Assert
        Assert.NotNull(player);
    }

    [Fact]
    [Trait("Persistence", "DbContext")]
    public async Task Persistence_DbContext_ShouldHandlePlayerGeneralProfileEntity()
    {
        // Arrange
        PlayerGeneralProfile entity = new()
        {
            FirstName = "First Name",
            LastName = "Last Name",
            City = "City",
            Player = new Player { Id = 1 }
        };
        PlayersDbContext context = CreateDbContext();

        // Act
        EntityEntry<PlayerGeneralProfile> addResult = await context.GeneralProfiles.AddAsync(entity);
        PlayerGeneralProfile? playerGeneralProfile = await context.GeneralProfiles.FindAsync(addResult.Entity.Id);

        // Assert
        Assert.NotNull(playerGeneralProfile);
    }

    [Fact]
    [Trait("Persistence", "DbContext")]
    public async Task Persistence_DbContext_ShouldHandlePlayerFootballProfileEntity()
    {
        // Arrange
        PlayerFootballProfile entity = new()
        {
            Position = Domain.Enums.FootballPosition.Forward,
            Player = new Player { Id = 1 }
        };
        PlayersDbContext context = CreateDbContext();

        // Act
        EntityEntry<PlayerFootballProfile> addResult = await context.FootballProfiles.AddAsync(entity);
        PlayerFootballProfile? playerFootballProfile = await context.FootballProfiles.FindAsync(addResult.Entity.Id);

        // Assert
        Assert.NotNull(playerFootballProfile);
    }

    [Fact]
    [Trait("Persistence", "DbContext")]
    public async Task Persistence_DbContext_ShouldHandlePlayerAvailabilityEntity()
    {
        // Arrange
        PlayerAvailability entity = new()
        {
            From = TimeSpan.MinValue,
            To = TimeSpan.MaxValue,
            Player = new Player { Id = 1 }
        };
        PlayersDbContext context = CreateDbContext();

        // Act
        EntityEntry<PlayerAvailability> addResult = await context.Availability.AddAsync(entity);
        PlayerAvailability? playerAvailability = await context.Availability.FindAsync(addResult.Entity.Id);

        // Assert
        Assert.NotNull(playerAvailability);
    }

    [Fact]
    [Trait("Persistence", "DbContext")]
    public async Task Persistence_DbContext_ShouldHandlePlayerAvailableDayEntity()
    {
        // Arrange
        PlayerAvailableDay entity = new()
        {
            Day = DayOfWeek.Friday,
            Availability = new PlayerAvailability
            {
                Player = new Player { Id = 1 }
            }
        };
        PlayersDbContext context = CreateDbContext();

        // Act
        EntityEntry<PlayerAvailableDay> addResult = await context.AvailableDays.AddAsync(entity);
        PlayerAvailableDay? availableDay = await context.AvailableDays.FindAsync(addResult.Entity.Id);

        // Assert
        Assert.NotNull(availableDay);
    }

    [Fact]
    [Trait("Persistence", "DbContext")]
    public async Task Persistence_DbContext_ShouldHandlePlayerStatPointsEntity()
    {
        // Arrange
        PlayerStatPoints entity = new()
        {
            Available = 3,
            Player = new Player { Id = 1 }
        };
        PlayersDbContext context = CreateDbContext();

        // Act
        EntityEntry<PlayerStatPoints> addResult = await context.Points.AddAsync(entity);
        PlayerStatPoints? playerStatPoints = await context.Points.FindAsync(addResult.Entity.Id);

        // Assert
        Assert.NotNull(playerStatPoints);
    }

    [Fact]
    [Trait("Persistence", "DbContext")]
    public async Task Persistence_DbContext_ShouldHandlePlayerStatEntity()
    {
        // Arrange
        PlayerStat entity = new()
        {
            Value = 99,
            Category = StatCategory.Pace,
            Type = StatType.Acceleration,
            Player = new Player { Id = 1 }
        };
        PlayersDbContext context = CreateDbContext();

        // Act
        EntityEntry<PlayerStat> addResult = await context.Stats.AddAsync(entity);
        PlayerStat? playerStat = await context.Stats.FindAsync(addResult.Entity.Id);

        // Assert
        Assert.NotNull(playerStat);
    }

    [Fact]
    [Trait("Persistence", "DbContext")]
    public async Task Persistence_DbContext_ShouldHandlePlayerTagEntity()
    {
        // Arrange
        PlayerTag entity = new()
        {
            Value = "Test tag",
            Player = new Player { Id = 1 }
        };
        PlayersDbContext context = CreateDbContext();

        // Act
        EntityEntry<PlayerTag> addResult = await context.Tags.AddAsync(entity);
        PlayerTag? playerTag = await context.Tags.FindAsync(addResult.Entity.Id);

        // Assert
        Assert.NotNull(playerTag);
    }

    [Fact]
    [Trait("Persistence", "DbContext")]
    public async Task Persistence_DbContext_ShouldHandlePlayerPhotoEntity()
    {
        // Arrange
        PlayerPhoto entity = new()
        {
            Extension = PhotoExtension.Jpg,
            Name = "Test name",
            Size = 10,
            Source = new byte[10],
            Player = new Player { Id = 1 }
        };
        PlayersDbContext context = CreateDbContext();

        // Act
        EntityEntry<PlayerPhoto> addResult = await context.Photos.AddAsync(entity);
        PlayerPhoto? playerPhoto = await context.Photos.FindAsync(addResult.Entity.Id);

        // Assert
        Assert.NotNull(playerPhoto);
    }

    [Fact]
    [Trait("Persistence", "DbContext")]
    public async Task Persistence_DbContext_ShouldHandleUserEntity()
    {
        // Arrange
        Guid userId = Guid.NewGuid();
        User entity = new()
        {
            UserId = userId,
            Player = new Player { Id = 1 }
        };
        PlayersDbContext context = CreateDbContext();

        // Act
       await context.Users.AddAsync(entity);
        User? user = await context.Users.FindAsync(userId);

        // Assert
        Assert.NotNull(user);
    }

    [Fact]
    [Trait("Persistence", "DbContext")]
    public async Task Persistence_DbContext_ShouldHandleIdentityUserEntity()
    {
        // Arrange
        Guid userId = Guid.NewGuid();
        IdentityUser entity = new()
        {
            UserId = userId,
            User = new User { Id = 1, UserId = userId }
        };
        PlayersDbContext context = CreateDbContext();

        // Act
        await context.IdentityUsers.AddAsync(entity);
        IdentityUser? user = await context.IdentityUsers.FindAsync(userId);

        // Assert
        Assert.NotNull(user);
    }

    private PlayersDbContext CreateDbContext()
    {
        Mock<IMediator> mediatorMock = new();
        AuditableEntitySaveChangesInterceptor interceptor = new(userServiceMock.Object, dateTimeServiceMock.Object);

        return new(dbContextOptions, mediatorMock.Object, interceptor);
    }
}
