using MediatR;

using Microsoft.EntityFrameworkCore;

using Moq;

using SFC.Players.Application.Interfaces.Common;
using SFC.Players.Application.Interfaces.Identity;
using SFC.Players.Domain.Entities;
using SFC.Players.Infrastructure.Persistence.Interceptors;
using SFC.Players.Infrastructure.Persistence.Repositories;

namespace SFC.Players.Infrastructure.Persistence.UnitTests.Repositories;
public class BaseRepositoryTests
{
    private readonly DbContextOptions<PlayersDbContext> dbContextOptions;

    public BaseRepositoryTests()
    {
        dbContextOptions = new DbContextOptionsBuilder<PlayersDbContext>()
            .UseInMemoryDatabase($"BaseRepositoryTestsDb_{DateTime.Now.ToFileTimeUtc()}")
            .Options;
    }

    [Fact]
    [Trait("Persistence", "Repository")]
    public async Task Persistence_Repository_Base_ShouldAddEntity()
    {
        // Arrange
        BaseRepository<PlayerGeneralProfile> repository = CreateRepository();
        PlayerGeneralProfile entity = new()
        {
            FirstName = "First name",
            LastName = "Last Name",
            City = "City",
            Player = new Player { Id = 1 }
        };

        // Act
        PlayerGeneralProfile result = await repository.AddAsync(entity);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(entity.Player.Id, result.Id);

        IReadOnlyList<PlayerGeneralProfile> generalProfiles = await repository.ListAllAsync();
        Assert.Single(generalProfiles);

        PlayerGeneralProfile? generalProfile = await repository.GetByIdAsync(result.Id);
        Assert.NotNull(generalProfile);
        Assert.Equal(entity.Player.Id, generalProfile.Id);
    }

    [Fact]
    [Trait("Persistence", "Repository")]
    public async Task Persistence_Repository_Base_ShouldUpdateEntity()
    {
        // Arrange
        BaseRepository<PlayerGeneralProfile> repository = CreateRepository();
        PlayerGeneralProfile entity = new()
        {
            FirstName = "First name",
            LastName = "Last Name",
            City = "City",
            Player = new Player { Id = 1 }
        };

        // Act
        await repository.AddAsync(entity);

        entity.FirstName = "Updated First name";
        entity.LastName = "Updated Last name";
        entity.City = "Updated City";

        await repository.UpdateAsync(entity);

        // Assert
        PlayerGeneralProfile? generalProfile = await repository.GetByIdAsync(entity.Player.Id);
        Assert.NotNull(generalProfile);
        Assert.Equal(entity.Player.Id, generalProfile.Id);
        Assert.Equal("Updated First name", generalProfile.FirstName);
        Assert.Equal("Updated Last name", generalProfile.LastName);
        Assert.Equal("Updated City", generalProfile.City);
    }

    [Fact]
    [Trait("Persistence", "Repository")]
    public async Task Persistence_Repository_Base_ShouldDeleteEntity()
    {
        // Arrange
        BaseRepository<PlayerGeneralProfile> repository = CreateRepository();
        PlayerGeneralProfile entity = new()
        {
            FirstName = "First name",
            LastName = "Last Name",
            City = "City",
            Player = new Player { Id = 1 }
        };

        // Act
        await repository.AddAsync(entity);

        await repository.DeleteAsync(entity);

        // Assert
        PlayerGeneralProfile? generalProfile = await repository.GetByIdAsync(entity.Player.Id);
        Assert.Null(generalProfile);
    }

    [Fact]
    [Trait("Persistence", "Repository")]
    public async Task Persistence_Repository_Base_ShouldGetByIdEntity()
    {
        // Arrange
        BaseRepository<PlayerGeneralProfile> repository = CreateRepository();
        PlayerGeneralProfile entity = new()
        {
            FirstName = "First name",
            LastName = "Last Name",
            City = "City",
            Player = new Player { Id = 1 }
        };

        // Act
        await repository.AddAsync(entity);
        PlayerGeneralProfile? generalProfile = await repository.GetByIdAsync(entity.Player.Id);

        // Assert
        Assert.NotNull(generalProfile);
        Assert.Equal(entity.Player.Id, generalProfile.Id);
        Assert.Equal(entity.FirstName, generalProfile.FirstName);
        Assert.Equal(entity.LastName, generalProfile.LastName);
        Assert.Equal(entity.City, generalProfile.City);
    }

    [Fact]
    [Trait("Persistence", "Repository")]
    public async Task Persistence_Repository_Base_ShouldNotFoundEntity()
    {
        // Arrange
        BaseRepository<PlayerGeneralProfile> repository = CreateRepository();

        // Act
        PlayerGeneralProfile? generalProfile = await repository.GetByIdAsync(1);

        // Assert
        Assert.Null(generalProfile);
    }

    [Fact]
    [Trait("Persistence", "Repository")]
    public async Task Persistence_Repository_Base_ShouldListAllEntities()
    {
        // Arrange
        BaseRepository<PlayerGeneralProfile> repository = CreateRepository();
        PlayerGeneralProfile entityFirst = new()
        {
            FirstName = "First name 1",
            LastName = "Last Name 1",
            City = "City 1",
            Player = new Player { Id = 1 }
        };
        PlayerGeneralProfile entitySecond = new()
        {
            FirstName = "First name 2",
            LastName = "Last Name 2",
            City = "City 2",
            Player = new Player { Id = 2 }
        };

        // Act
        await repository.AddAsync(entityFirst);
        await repository.AddAsync(entitySecond);
        IReadOnlyList<PlayerGeneralProfile> generalProfiles = await repository.ListAllAsync();

        // Assert
        Assert.Equal(2, generalProfiles.Count);
        Assert.Equal(entityFirst.Player.Id, generalProfiles[0].Id);
        Assert.Equal(entitySecond.Player.Id, generalProfiles[1].Id);
    }

    [Fact]
    [Trait("Persistence", "Repository")]
    public async Task Persistence_Repository_Base_ShouldNotFoundEntities()
    {
        // Arrange
        BaseRepository<PlayerGeneralProfile> repository = CreateRepository();

        // Act
        IReadOnlyList<PlayerGeneralProfile> generalProfiles = await repository.ListAllAsync();

        // Assert
        Assert.Empty(generalProfiles);
    }

    private BaseRepository<PlayerGeneralProfile> CreateRepository()
    {
        Mock<IMediator> mediatorMock = new();

        Mock<IUserService> userServiceMock = new();

        Mock<IDateTimeService> dateTimeServiceMock = new();

        Mock<AuditableEntitySaveChangesInterceptor> interceptorMock = new(userServiceMock.Object, dateTimeServiceMock.Object);

        PlayersDbContext context = new(dbContextOptions, mediatorMock.Object, interceptorMock.Object);

        return new BaseRepository<PlayerGeneralProfile>(context);
    }
}
