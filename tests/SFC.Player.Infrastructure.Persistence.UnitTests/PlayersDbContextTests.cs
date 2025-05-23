//using MediatR;

//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.ChangeTracking;

//using Moq;
//using SFC.Player.Application.Interfaces.Common;
//using SFC.Player.Domain.Entities;
//using SFC.Player.Domain.Entities.Data;
//using SFC.Player.Domain.Enums;
//using SFC.Player.Infrastructure.Persistence.Interceptors;

//using PlayerEntity = SFC.Player.Domain.Entities.Player;
//using SFC.Player.Application.Interfaces.Identity;
//using SFC.Player.Infrastructure.Persistence.Constants;
//using SFC.Player.Infrastructure.Persistence.Contexts;
//using SFC.Player.Messages.Models.Player;

//namespace SFC.Player.Infrastructure.Persistence.UnitTests;
//public class PlayersDbContextTests
//{
//    private readonly Mock<IUserService> _userServiceMock = new();
//    private readonly Mock<IDateTimeService> dateTimeServiceMock = new();
//    private readonly DbContextOptions<PlayerDbContext> dbContextOptions;

//    public PlayersDbContextTests()
//    {
//        dbContextOptions = new DbContextOptionsBuilder<PlayerDbContext>()
//            .UseInMemoryDatabase($"PlayersDbContextTestsDb_{DateTime.Now.ToFileTimeUtc()}")
//            .Options;
//    }

//    [Fact]
//    [Trait("Persistence", "DbContext")]
//    public void Persistence_DbContext_ShouldHasCorrectDefaultSchema()
//    {
//        PlayerDbContext context = CreateDbContext();

//        string? defaultSchema = context.Model.GetDefaultSchema();

//        Assert.Equal(DatabaseConstants.DEFAULT_SCHEMA_NAME, defaultSchema);
//    }

//    [Fact]
//    [Trait("Persistence", "DbContext")]
//    public async Task Persistence_DbContext_ShouldHandlePlayerEntity()
//    {
//        // Arrange
//        PlayerEntity entity = new()
//        {
//            GeneralProfile = new PlayerGeneralProfile
//            {
//                FirstName = "First Name",
//                LastName = "Last Name",
//                City = "City"
//            }
//        };
//        PlayerDbContext context = CreateDbContext();

//        // Act
//        EntityEntry<PlayerEntity> addResult = await context.Set<PlayerEntity>().AddAsync(entity);
//        await context.SaveChangesAsync();
//        PlayerEntity? player = await context.Set<PlayerEntity>().FindAsync(addResult.Entity.Id);

//        // Assert
//        Assert.NotNull(player);
//    }

//    [Fact]
//    [Trait("Persistence", "DbContext")]
//    public async Task Persistence_DbContext_ShouldHandlePlayerGeneralProfileEntity()
//    {
//        // Arrange
//        PlayerGeneralProfile entity = new()
//        {
//            FirstName = "First Name",
//            LastName = "Last Name",
//            City = "City",
//            Player = new PlayerEntity { Id = 1 }
//        };
//        PlayerDbContext context = CreateDbContext();

//        // Act
//        EntityEntry<PlayerGeneralProfile> addResult = await context.Set<PlayerGeneralProfile>().AddAsync(entity);
//        await context.SaveChangesAsync();
//        PlayerGeneralProfile? playerGeneralProfile = await context.Set<PlayerGeneralProfile>().FindAsync(addResult.Entity.Id);

//        // Assert
//        Assert.NotNull(playerGeneralProfile);
//    }

//    [Fact]
//    [Trait("Persistence", "DbContext")]
//    public async Task Persistence_DbContext_ShouldHandlePlayerFootballProfileEntity()
//    {
//        // Arrange
//        PlayerFootballProfile entity = new()
//        {
//            PositionId = 2,
//            Player = new PlayerEntity { Id = 1 }
//        };
//        PlayerDbContext context = CreateDbContext();

//        // Act
//        EntityEntry<PlayerFootballProfile> addResult = await context.Set<PlayerFootballProfile>().AddAsync(entity);
//        await context.SaveChangesAsync();
//        PlayerFootballProfile? playerFootballProfile = await context.Set<PlayerFootballProfile>().FindAsync(addResult.Entity.Id);

//        // Assert
//        Assert.NotNull(playerFootballProfile);
//    }

//    [Fact]
//    [Trait("Persistence", "DbContext")]
//    public async Task Persistence_DbContext_ShouldHandlePlayerAvailabilityEntity()
//    {
//        // Arrange
//        PlayerAvailability entity = new()
//        {
//            From = TimeSpan.MinValue,
//            To = TimeSpan.MaxValue,
//            Player = new PlayerEntity { Id = 1 }
//        };
//        PlayerDbContext context = CreateDbContext();

//        // Act
//        EntityEntry<PlayerAvailability> addResult = await context.Set<PlayerAvailability>().AddAsync(entity);
//        await context.SaveChangesAsync();
//        PlayerAvailability? playerAvailability = await context.Set<PlayerAvailability>().FindAsync(addResult.Entity.Id);

//        // Assert
//        Assert.NotNull(playerAvailability);
//    }

//    [Fact]
//    [Trait("Persistence", "DbContext")]
//    public async Task Persistence_DbContext_ShouldHandlePlayerAvailableDayEntity()
//    {
//        // Arrange
//        PlayerAvailableDay entity = new()
//        {
//            Day = DayOfWeek.Friday,
//            Availability = new PlayerAvailability
//            {
//                Player = new PlayerEntity { Id = 1 }
//            }
//        };
//        PlayerDbContext context = CreateDbContext();

//        // Act
//        EntityEntry<PlayerAvailableDay> addResult = await context.Set<PlayerAvailableDay>().AddAsync(entity);
//        await context.SaveChangesAsync();
//        PlayerAvailableDay? availableDay = await context.Set<PlayerAvailableDay>().FindAsync(addResult.Entity.Id);

//        // Assert
//        Assert.NotNull(availableDay);
//    }

//    [Fact]
//    [Trait("Persistence", "DbContext")]
//    public async Task Persistence_DbContext_ShouldHandlePlayerStatPointsEntity()
//    {
//        // Arrange
//        PlayerStatPoints entity = new()
//        {
//            Available = 3,
//            Player = new PlayerEntity { Id = 1 }
//        };
//        PlayerDbContext context = CreateDbContext();

//        // Act
//        EntityEntry<PlayerStatPoints> addResult = await context.Set<PlayerStatPoints>().AddAsync(entity);
//        await context.SaveChangesAsync();
//        PlayerStatPoints? playerStatPoints = await context.Set<PlayerStatPoints>().FindAsync(addResult.Entity.Id);

//        // Assert
//        Assert.NotNull(playerStatPoints);
//    }

//    [Fact]
//    [Trait("Persistence", "DbContext")]
//    public async Task Persistence_DbContext_ShouldHandlePlayerStatEntity()
//    {
//        // Arrange
//        PlayerStat entity = new()
//        {
//            Value = 99,
//            Type = new StatType { Id = 1},
//            Player = new PlayerEntity { Id = 1 }
//        };
//        PlayerDbContext context = CreateDbContext();

//        // Act
//        EntityEntry<PlayerStat> addResult = await context.Set<PlayerStat>().AddAsync(entity);
//        await context.SaveChangesAsync();
//        PlayerStat? playerStat = await context.Set<PlayerStat>().FindAsync(addResult.Entity.Id);

//        // Assert
//        Assert.NotNull(playerStat);
//    }

//    [Fact]
//    [Trait("Persistence", "DbContext")]
//    public async Task Persistence_DbContext_ShouldHandlePlayerTagEntity()
//    {
//        // Arrange
//        PlayerTag entity = new()
//        {
//            Value = "Test tag",
//            Player = new PlayerEntity { Id = 1 }
//        };
//        PlayerDbContext context = CreateDbContext();

//        // Act
//        EntityEntry<PlayerTag> addResult = await context.Set<PlayerTag>().AddAsync(entity);
//        await context.SaveChangesAsync();
//        PlayerTag? playerTag = await context.Set<PlayerTag>().FindAsync(addResult.Entity.Id);

//        // Assert
//        Assert.NotNull(playerTag);
//    }

//    [Fact]
//    [Trait("Persistence", "DbContext")]
//    public async Task Persistence_DbContext_ShouldHandlePlayerPhotoEntity()
//    {
//        // Arrange
//        PlayerPhoto entity = new()
//        {
//            Extension = PhotoExtension.Jpg,
//            Name = "Test name",
//            Size = 10,
//            Source = new byte[10],
//            Player = new PlayerEntity { Id = 1 }
//        };
//        PlayerDbContext context = CreateDbContext();

//        // Act
//        EntityEntry<PlayerPhoto> addResult = await context.Set<PlayerPhoto>().AddAsync(entity);
//        await context.SaveChangesAsync();
//        PlayerPhoto? playerPhoto = await context.Set<PlayerPhoto>().FindAsync(addResult.Entity.Id);

//        // Assert
//        Assert.NotNull(playerPhoto);
//    }

//    //[Fact]
//    //[Trait("Persistence", "DbContext")]
//    //public async Task Persistence_DbContext_ShouldHandleUserEntity()
//    //{
//    //    // Arrange
//    //    Guid userId = Guid.NewGuid();
//    //    User entity = new()
//    //    {
//    //        Id = userId,
//    //        Player = new PlayerEntity { Id = 1 }
//    //    };
//    //    PlayerDbContext context = CreateDbContext();

//    //    // Act
//    //    await context.Set<PlayerGeneralProfile>().AddAsync(entity);
//    //    await context.SaveChangesAsync();
//    //    PlayerUser? user = await context.Set<PlayerGeneralProfile>().FindAsync(userId);

//    //    // Assert
//    //    Assert.NotNull(user);
//    //}

//    //[Fact]
//    //[Trait("Persistence", "DbContext")]
//    //public async Task Persistence_DbContext_ShouldHandleIdentityUserEntity()
//    //{
//    //    // Arrange
//    //    Guid userId = Guid.NewGuid();
//    //    IdentityUser entity = new()
//    //    {
//    //        Id = userId,
//    //        PlayerUser = new PlayerUser { Id = userId }
//    //    };
//    //    PlayerDbContext context = CreateDbContext();

//    //    // Act
//    //    await context.Set<PlayerGeneralProfile>().AddAsync(entity);
//    //    await context.SaveChangesAsync();
//    //    IdentityUser? user = await context.Set<PlayerGeneralProfile>().FindAsync(userId);

//    //    // Assert
//    //    Assert.NotNull(user);
//    //}

//    //[Fact]
//    //[Trait("Persistence", "DbContext")]
//    //public async Task Persistence_DbContext_ShouldHandleGameStyleEntity()
//    //{
//    //    // Arrange
//    //    GameStyle entity = new()
//    //    {
//    //        Id = 0,
//    //        Title = "Title"
//    //    };
//    //    PlayerDbContext context = CreateDbContext();

//    //    // Act
//    //    await context.Set<PlayerGeneralProfile>().AddAsync(entity);
//    //    await context.SaveChangesAsync();
//    //    GameStyle? dataEntity = await context.Set<PlayerGeneralProfile>().FindAsync(entity.Id);

//    //    // Assert
//    //    Assert.NotNull(dataEntity);
//    //}

//    //[Fact]
//    //[Trait("Persistence", "DbContext")]
//    //public async Task Persistence_DbContext_ShouldHandleFootballPositionEntity()
//    //{
//    //    // Arrange
//    //    FootballPosition entity = new()
//    //    {
//    //        Id = 0,
//    //        Title = "Title"
//    //    };
//    //    PlayerDbContext context = CreateDbContext();

//    //    // Act
//    //    await context.Set<PlayerGeneralProfile>().AddAsync(entity);
//    //    await context.SaveChangesAsync();
//    //    FootballPosition? dataEntity = await context.Set<PlayerGeneralProfile>().FindAsync(entity.Id);

//    //    // Assert
//    //    Assert.NotNull(dataEntity);
//    //}

//    //[Fact]
//    //[Trait("Persistence", "DbContext")]
//    //public async Task Persistence_DbContext_ShouldHandleStatCategoryEntity()
//    //{
//    //    // Arrange
//    //    StatCategory entity = new()
//    //    {
//    //        Id = 0,
//    //        Title = "Title"
//    //    };
//    //    PlayerDbContext context = CreateDbContext();

//    //    // Act
//    //    await context.Set<PlayerGeneralProfile>().AddAsync(entity);
//    //    await context.SaveChangesAsync();
//    //    StatCategory? dataEntity = await context.Set<PlayerGeneralProfile>().FindAsync(entity.Id);

//    //    // Assert
//    //    Assert.NotNull(dataEntity);
//    //}

//    //[Fact]
//    //[Trait("Persistence", "DbContext")]
//    //public async Task Persistence_DbContext_ShouldHandleStatSkillEntity()
//    //{
//    //    // Arrange
//    //    StatSkill entity = new()
//    //    {
//    //        Id = 0,
//    //        Title = "Title"
//    //    };
//    //    PlayerDbContext context = CreateDbContext();

//    //    // Act
//    //    await context.StatSkills.AddAsync(entity);
//    //    await context.SaveChangesAsync();
//    //    StatSkill? dataEntity = await context.StatSkills.FindAsync(entity.Id);

//    //    // Assert
//    //    Assert.NotNull(dataEntity);
//    //}

//    //[Fact]
//    //[Trait("Persistence", "DbContext")]
//    //public async Task Persistence_DbContext_ShouldHandleStatTypeEntity()
//    //{
//    //    // Arrange
//    //    StatType entity = new()
//    //    {
//    //        Id = 0,
//    //        Title = "Title"
//    //    };
//    //    PlayerDbContext context = CreateDbContext();

//    //    // Act
//    //    await context.StatTypes.AddAsync(entity);
//    //    await context.SaveChangesAsync();
//    //    StatType? dataEntity = await context.StatTypes.FindAsync(entity.Id);

//    //    // Assert
//    //    Assert.NotNull(dataEntity);
//    //}

//    //[Fact]
//    //[Trait("Persistence", "DbContext")]
//    //public async Task Persistence_DbContext_ShouldHandleWorkingFootEntity()
//    //{
//    //    // Arrange
//    //    WorkingFoot entity = new()
//    //    {
//    //        Id = 0,
//    //        Title = "Title"
//    //    };
//    //    PlayerDbContext context = CreateDbContext();

//    //    // Act
//    //    await context.WorkingFoots.AddAsync(entity);
//    //    await context.SaveChangesAsync();
//    //    WorkingFoot? dataEntity = await context.WorkingFoots.FindAsync(entity.Id);

//    //    // Assert
//    //    Assert.NotNull(dataEntity);
//    //}

//    private PlayerDbContext CreateDbContext()
//    {
//        Mock<IMediator> mediatorMock = new();
//        AuditableEntitySaveChangesInterceptor interceptor = new(_userServiceMock.Object, dateTimeServiceMock.Object);

//        return new(dbContextOptions, mediatorMock.Object, interceptor);
//    }
//}
