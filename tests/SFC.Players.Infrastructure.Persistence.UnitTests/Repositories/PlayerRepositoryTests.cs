using MediatR;

using Microsoft.EntityFrameworkCore;

using Moq;

using SFC.Players.Application.Interfaces.Common;
using SFC.Players.Application.Interfaces.Identity;
using SFC.Players.Domain.Entities;
using SFC.Players.Domain.Enums;
using SFC.Players.Infrastructure.Persistence.Interceptors;

using SFC.Players.Infrastructure.Persistence.Repositories;

namespace SFC.Players.Infrastructure.Persistence.UnitTests.Repositories;
public class PlayerRepositoryTests
{
    private readonly DbContextOptions<PlayersDbContext> dbContextOptions;

    public PlayerRepositoryTests()
    {
        dbContextOptions = new DbContextOptionsBuilder<PlayersDbContext>()
            .UseInMemoryDatabase($"PlayerRepositoryTestsDb_{DateTime.Now.ToFileTimeUtc()}")
        .Options;
    }

    [Fact]
    [Trait("Persistence", "Repository")]
    public async Task Persistence_Repository_Player_ShouldGetByIdEntity()
    {
        // Arrange
        PlayerRepository repository = CreateRepository();
        Player entity = GetNewPlayer(Guid.NewGuid());

        // Act
        await repository.AddAsync(entity);
        Player? player = await repository.GetByIdAsync(1);

        // Assert
        Assert.NotNull(player);
        Assert.Equal(entity.GeneralProfile.FirstName, player.GeneralProfile.FirstName);
        Assert.Equal(entity.FootballProfile.Position, player.FootballProfile.Position);
        Assert.Equal(entity.Availability.From, player.Availability.From);
        Assert.Equal(entity.Availability.To, player.Availability.To);
        Assert.Equal(entity.Photo.Extension, player.Photo.Extension);
        Assert.Equal(entity.Photo.Name, player.Photo.Name);
        Assert.Equal(entity.Photo.Size, player.Photo.Size);
        Assert.Equal(entity.Photo.Source, player.Photo.Source);
        Assert.Equal(entity.Points.Available, player.Points.Available);
        Assert.Equal(entity.Points.Used, player.Points.Used);
        Assert.Equal(entity.User.UserId, player.User.UserId);
        Assert.Single(player.Tags);
        Assert.Equal(entity.Tags.First().Value, player.Tags.First().Value);
        Assert.Single(player.Stats);
        PlayerStat stat = player.Stats.First();
        PlayerStat entityStat = entity.Stats.First();
        Assert.Equal(entityStat.Value, stat.Value);
        Assert.Equal(entityStat.Category, stat.Category);
        Assert.Equal(entityStat.Type, stat.Type);
        Assert.Single(player.Availability.Days);
        Assert.Equal(entity.Availability.Days.First().Day, player.Availability.Days.First().Day);
    }

    [Fact]
    [Trait("Persistence", "Repository")]
    public async Task Persistence_Repository_Player_ShouldNotFindGetByIdEntity()
    {
        // Arrange
        PlayerRepository repository = CreateRepository();
        Player entity = GetNewPlayer(Guid.NewGuid());

        // Act
        await repository.AddAsync(entity);
        Player? player = await repository.GetByIdAsync(2);

        // Assert
        Assert.Null(player);
    }

    [Fact]
    [Trait("Persistence", "Repository")]
    public async Task Persistence_Repository_Player_ShouldGetByUserIdEntity()
    {
        // Arrange
        PlayerRepository repository = CreateRepository();
        Guid userId = Guid.NewGuid();
        Player entity = GetNewPlayer(userId);

        // Act
        await repository.AddAsync(entity);
        Player? player = await repository.GetByUserIdAsync(userId);

        // Assert
        Assert.NotNull(player);
        Assert.Equal(entity.GeneralProfile.FirstName, player.GeneralProfile.FirstName);
        Assert.Equal(entity.FootballProfile.Position, player.FootballProfile.Position);
        Assert.Equal(entity.Photo.Extension, player.Photo.Extension);
        Assert.Equal(entity.Photo.Name, player.Photo.Name);
        Assert.Equal(entity.Photo.Size, player.Photo.Size);
        Assert.Equal(entity.Photo.Source, player.Photo.Source);
        Assert.Equal(entity.User.UserId, player.User.UserId);
    }

    [Fact]
    [Trait("Persistence", "Repository")]
    public async Task Persistence_Repository_Player_ShouldNotFindGetByUserIdEntity()
    {
        // Arrange
        PlayerRepository repository = CreateRepository();
        Guid userId = Guid.NewGuid();
        Player entity = GetNewPlayer(userId);

        // Act
        await repository.AddAsync(entity);
        Player? player = await repository.GetByUserIdAsync(Guid.NewGuid());

        // Assert
        Assert.Null(player);
    }

    private PlayerRepository CreateRepository()
    {
        Mock<IMediator> mediatorMock = new();

        Mock<IUserService> userServiceMock = new();

        Mock<IDateTimeService> dateTimeServiceMock = new();

        Mock<AuditableEntitySaveChangesInterceptor> interceptorMock = new(userServiceMock.Object, dateTimeServiceMock.Object);

        PlayersDbContext context = new(dbContextOptions, mediatorMock.Object, interceptorMock.Object);

        return new PlayerRepository(context);
    }

    private static Player GetNewPlayer(Guid userId)
    {
        Player entity = new()
        {
            GeneralProfile = new PlayerGeneralProfile
            {
                FirstName = "First Name",
                LastName = "Last Name",
                City = "City"
            },
            FootballProfile = new PlayerFootballProfile
            {
                Position = FootballPosition.Forward
            },
            Availability = new PlayerAvailability
            {
                From = TimeSpan.MinValue,
                To = TimeSpan.MaxValue
            },
            Photo = new PlayerPhoto
            {
                Extension = PhotoExtension.Jpg,
                Name = "Photo",
                Size = 10,
                Source = new byte[10]
            },
            Points = new PlayerStatPoints
            {
                Available = 3,
                Used = 1
            },
            User = new User
            {
                UserId = userId
            }
        };
        entity.Tags.Add(new PlayerTag { Value = "Tag 1" });
        PlayerStat entityStat = new() { Category = StatCategory.Pace, Type = StatType.Acceleration, Value = 99 };
        entity.Stats.Add(entityStat);
        entity.Availability.Days.Add(new PlayerAvailableDay { Day = DayOfWeek.Friday });

        return entity;
    }
}
