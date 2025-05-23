//using MediatR;

//using Microsoft.AspNetCore.Http;
//using Microsoft.EntityFrameworkCore;

//using Moq;

//using SFC.Player.Application.Common.Enums;
//using SFC.Player.Application.Features.Common.Models;
//using SFC.Player.Application.Features.Common.Models.Filters;
//using SFC.Player.Application.Features.Common.Models.Paging;
//using SFC.Player.Application.Features.Common.Models.Sorting;
//using SFC.Player.Application.Interfaces.Common;
//using SFC.Player.Domain.Entities;
//using SFC.Player.Domain.Entities.Data;
//using SFC.Player.Domain.Entities.Identity;
//using SFC.Player.Domain.Enums;
//using SFC.Player.Infrastructure.Persistence.Contexts;
//using SFC.Player.Infrastructure.Persistence.Interceptors;

//using SFC.Player.Infrastructure.Persistence.Repositories;
//using SFC.Player.Messages.Models.Player;

//using PlayerEntity = SFC.Player.Domain.Entities.Player;

//namespace SFC.Player.Infrastructure.Persistence.UnitTests.Repositories;
//public class PlayerRepositoryTests
//{
//    private readonly DbContextOptions<PlayerDbContext> _dbContextOptions;
//    private static readonly StatType StatType = new() { Id = 1 };

//    public PlayerRepositoryTests()
//    {
//        _dbContextOptions = new DbContextOptionsBuilder<PlayerDbContext>()
//            .UseInMemoryDatabase($"PlayerRepositoryTestsDb_{DateTime.Now.ToFileTimeUtc()}")
//        .Options;
//    }

//    [Fact]
//    [Trait("Persistence", "Repository")]
//    public async Task Persistence_Repository_Player_ShouldAddEntity()
//    {
//        // Arrange
//        PlayerRepository repository = CreateRepository();
//        Guid userId = Guid.NewGuid();
//        PlayerEntity entity = GetNewPlayer(userId);

//        // Act
//        await repository.AddAsync(entity);
//        PlayerEntity? player = await repository.GetByUserIdAsync(userId);

//        // Assert
//        Assert.NotNull(player);
//    }

//    [Fact]
//    [Trait("Persistence", "Repository")]
//    public async Task Persistence_Repository_Player_ShouldGetByIdEntity()
//    {
//        // Arrange
//        PlayerRepository repository = CreateRepository();
//        PlayerEntity entity = GetNewPlayer(Guid.NewGuid());

//        // Act
//        await repository.AddAsync(entity);
//        PlayerEntity? player = await repository.GetByIdAsync(1);

//        // Assert
//        Assert.NotNull(player);
//        Assert.Equal(entity.GeneralProfile.FirstName, player.GeneralProfile.FirstName);
//        Assert.Equal(entity.FootballProfile.PositionId, player.FootballProfile.PositionId);
//        Assert.Equal(entity.Availability.From, player.Availability.From);
//        Assert.Equal(entity.Availability.To, player.Availability.To);
//        Assert.Equal(entity.Photo.Extension, player.Photo.Extension);
//        Assert.Equal(entity.Photo.Name, player.Photo.Name);
//        Assert.Equal(entity.Photo.Size, player.Photo.Size);
//        Assert.Equal(entity.Photo.Source, player.Photo.Source);
//        Assert.Equal(entity.Points.Available, player.Points.Available);
//        Assert.Equal(entity.Points.Used, player.Points.Used);
//        Assert.Equal(entity.User.Id, player.User.Id);
//        Assert.Single(player.Tags);
//        Assert.Equal(entity.Tags.First().Value, player.Tags.First().Value);
//        Assert.Single(player.Stats);

//        PlayerStat stat = player.Stats.First();
//        PlayerStat entityStat = entity.Stats.First();

//        Assert.Equal(entityStat.Value, stat.Value);
//        Assert.Equal(entityStat.Type, stat.Type);
//        Assert.Single(player.Availability.Days);
//        Assert.Equal(entity.Availability.Days.First().Day, player.Availability.Days.First().Day);
//    }

//    [Fact]
//    [Trait("Persistence", "Repository")]
//    public async Task Persistence_Repository_Player_ShouldNotFindGetByIdEntity()
//    {
//        // Arrange
//        PlayerRepository repository = CreateRepository();
//        PlayerEntity entity = GetNewPlayer(Guid.NewGuid());

//        // Act
//        await repository.AddAsync(entity);
//        PlayerEntity? player = await repository.GetByIdAsync(2);

//        // Assert
//        Assert.Null(player);
//    }

//    [Fact]
//    [Trait("Persistence", "Repository")]
//    public async Task Persistence_Repository_Player_ShouldGetByUserIdEntity()
//    {
//        // Arrange
//        PlayerRepository repository = CreateRepository();
//        Guid userId = Guid.NewGuid();
//        PlayerEntity entity = GetNewPlayer(userId);

//        // Act
//        await repository.AddAsync(entity);
//        PlayerEntity? player = await repository.GetByUserIdAsync(userId);

//        // Assert
//        Assert.NotNull(player);
//        Assert.Equal(entity.GeneralProfile.FirstName, player.GeneralProfile.FirstName);
//        Assert.Equal(entity.FootballProfile.PositionId, player.FootballProfile.PositionId);
//        Assert.Equal(entity.Photo.Extension, player.Photo.Extension);
//        Assert.Equal(entity.Photo.Name, player.Photo.Name);
//        Assert.Equal(entity.Photo.Size, player.Photo.Size);
//        Assert.Equal(entity.Photo.Source, player.Photo.Source);
//        Assert.Equal(entity.User.Id, player.User.Id);
//    }

//    [Fact]
//    [Trait("Persistence", "Repository")]
//    public async Task Persistence_Repository_Player_ShouldNotFindGetByUserIdEntity()
//    {
//        // Arrange
//        PlayerRepository repository = CreateRepository();
//        Guid userId = Guid.NewGuid();
//        PlayerEntity entity = GetNewPlayer(userId);

//        // Act
//        await repository.AddAsync(entity);
//        PlayerEntity? player = await repository.GetByUserIdAsync(Guid.NewGuid());

//        // Assert
//        Assert.Null(player);
//    }

//    [Fact]
//    [Trait("Persistence", "Repository")]
//    public async Task Persistence_Repository_Player_ShouldFind()
//    {
//        // Arrange
//        PlayerRepository repository = CreateRepository();
//        FindParameters<PlayerEntity> parameters = new()
//        {
//            Filters = new Filters<PlayerEntity>(new Filter<PlayerEntity>[1] {
//                new() {
//                    Condition = true,
//                    Expression = player =>player.FootballProfile.PositionId == 3 || player.FootballProfile.PositionId == 0
//                }
//            }),
//            Pagination = new Pagination { Page = 2, Size = 2 },
//            Sorting = new Sortings<PlayerEntity>(new Sorting<PlayerEntity, dynamic>[1] {
//                new() {
//                    Condition = true,
//                    Direction = SortingDirection.Descending,
//                    Expression = player=>player.FootballProfile.PositionId!
//                }
//            })
//        };

//        // Act
//        await repository.AddAsync(GetNewPlayer(Guid.NewGuid(), 1));
//        await repository.AddAsync(GetNewPlayer(Guid.NewGuid(), 2));
//        await repository.AddAsync(GetNewPlayer(Guid.NewGuid(), 3));
//        await repository.AddAsync(GetNewPlayer(Guid.NewGuid(), 0));
//        await repository.AddAsync(GetNewPlayer(Guid.NewGuid(), 3));
//        await repository.AddAsync(GetNewPlayer(Guid.NewGuid(), 3));

//        PagedList<PlayerEntity> result = await repository.FindAsync(parameters);

//        // Assert
//        Assert.Equal(2, result.Count);
//        Assert.Equal(3, result[0].FootballProfile.PositionId);
//        Assert.Equal(0, result[1].FootballProfile.PositionId);
//        Assert.Equal(2, result.CurrentPage);
//        Assert.Equal(2, result.TotalPages);
//        Assert.Equal(2, result.PageSize);
//        Assert.Equal(4, result.TotalCount);
//    }

//    private PlayerRepository CreateRepository()
//    {
//        Mock<IMediator> mediatorMock = new();

//        Mock<IHttpContextAccessor> httpContextAccessorMock = new();

//        Mock<IDateTimeService> dateTimeServiceMock = new();

//        Mock<AuditableEntitySaveChangesInterceptor> interceptorMock = new(httpContextAccessorMock.Object, dateTimeServiceMock.Object);

//        PlayerDbContext context = new(_dbContextOptions, mediatorMock.Object, interceptorMock.Object);

//        return new PlayerRepository(context);
//    }

//    private static PlayerEntity GetNewPlayer(Guid userId, int? position = 2)
//    {
//        PlayerEntity entity = new()
//        {
//            GeneralProfile = new PlayerGeneralProfile
//            {
//                FirstName = "First Name",
//                LastName = "Last Name",
//                City = "City"
//            },
//            FootballProfile = new PlayerFootballProfile
//            {
//                PositionId = position
//            },
//            Availability = new PlayerAvailability
//            {
//                From = TimeSpan.MinValue,
//                To = TimeSpan.MaxValue
//            },
//            Photo = new PlayerPhoto
//            {
//                Extension = PhotoExtension.Jpg,
//                Name = "Photo",
//                Size = 10,
//                Source = new byte[10]
//            },
//            Points = new PlayerStatPoints
//            {
//                Available = 3,
//                Used = 1
//            },
//            User = new User
//            {
//                Id = userId
//            }
//        };
//        entity.Tags.Add(new PlayerTag { Value = "Tag 1" });
//        PlayerStat entityStat = new()
//        {
//            Type = StatType,
//            Value = 99
//        };
//        entity.Stats.Add(entityStat);
//        entity.Availability.Days.Add(new PlayerAvailableDay { Day = DayOfWeek.Friday });

//        return entity;
//    }
//}
