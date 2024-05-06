using MediatR;

using Microsoft.EntityFrameworkCore;

using Moq;

using SFC.Player.Application.Common.Enums;
using SFC.Player.Application.Features.Common.Models;
using SFC.Player.Application.Features.Common.Models.Filters;
using SFC.Player.Application.Features.Common.Models.Paging;
using SFC.Player.Application.Features.Common.Models.Sorting;
using SFC.Player.Application.Interfaces.Common;
using SFC.Player.Application.Interfaces.Identity;
using SFC.Player.Domain.Entities;
using SFC.Player.Infrastructure.Persistence.Interceptors;
using SFC.Player.Infrastructure.Persistence.Repositories;

using PlayerEntity = SFC.Player.Domain.Entities.Player;

namespace SFC.Player.Infrastructure.Persistence.UnitTests.Repositories;
public class RepositoryTests
{
    private readonly DbContextOptions<PlayerDbContext> _dbContextOptions;

    public RepositoryTests()
    {
        _dbContextOptions = new DbContextOptionsBuilder<PlayerDbContext>()
            .UseInMemoryDatabase($"RepositoryTestsDb_{DateTime.Now.ToFileTimeUtc()}")
            .Options;
    }

    [Fact]
    [Trait("Persistence", "Repository")]
    public async Task Persistence_Repository_ShouldAddEntity()
    {
        // Arrange
        Repository<PlayerGeneralProfile, long> repository = CreateRepository();
        PlayerGeneralProfile entity = new()
        {
            FirstName = "First name",
            LastName = "Last Name",
            City = "City",
            Player = new PlayerEntity { Id = 1 }
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
    public async Task Persistence_Repository_ShouldAddRangeOfEntities()
    {
        // Arrange
        Repository<PlayerGeneralProfile, long> repository = CreateRepository();
        PlayerGeneralProfile[] entities = {
            new()
            {
                FirstName = "First name first",
                LastName = "Last Name first",
                City = "City first",
                Player = new PlayerEntity { Id = 1 }
            },
            new()
            {
                FirstName = "First name second",
                LastName = "Last Name second",
                City = "City second",
                Player = new PlayerEntity { Id = 2 }
            }
        };

        // Act
        PlayerGeneralProfile[] result = await repository.AddRangeAsync(entities);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(entities.Length, result.Length);

        IReadOnlyList<PlayerGeneralProfile> generalProfiles = await repository.ListAllAsync();
        Assert.Equal(generalProfiles.Count, entities.Length);

        PlayerGeneralProfile? generalProfileFirst = await repository.GetByIdAsync(result[0].Id);
        Assert.NotNull(generalProfileFirst);
        Assert.Equal(entities[0].Player.Id, generalProfileFirst.Id);

        PlayerGeneralProfile? generalProfileSecond = await repository.GetByIdAsync(result[1].Id);
        Assert.NotNull(generalProfileSecond);
        Assert.Equal(entities[1].Player.Id, generalProfileSecond.Id);
    }

    [Fact]
    [Trait("Persistence", "Repository")]
    public async Task Persistence_Repository_ShouldUpdateEntity()
    {
        // Arrange
        Repository<PlayerGeneralProfile, long> repository = CreateRepository();
        PlayerGeneralProfile entity = new()
        {
            FirstName = "First name",
            LastName = "Last Name",
            City = "City",
            Player = new PlayerEntity { Id = 1 }
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
    public async Task Persistence_Repository_ShouldDeleteEntity()
    {
        // Arrange
        Repository<PlayerGeneralProfile, long> repository = CreateRepository();
        PlayerGeneralProfile entity = new()
        {
            FirstName = "First name",
            LastName = "Last Name",
            City = "City",
            Player = new PlayerEntity { Id = 1 }
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
    public async Task Persistence_Repository_ShouldGetByIdEntity()
    {
        // Arrange
        Repository<PlayerGeneralProfile, long> repository = CreateRepository();
        PlayerGeneralProfile entity = new()
        {
            FirstName = "First name",
            LastName = "Last Name",
            City = "City",
            Player = new PlayerEntity { Id = 1 }
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
    public async Task Persistence_Repository_ShouldNotFoundEntity()
    {
        // Arrange
        Repository<PlayerGeneralProfile, long> repository = CreateRepository();

        // Act
        PlayerGeneralProfile? generalProfile = await repository.GetByIdAsync(1);

        // Assert
        Assert.Null(generalProfile);
    }

    [Fact]
    [Trait("Persistence", "Repository")]
    public async Task Persistence_Repository_ShouldListAllEntities()
    {
        // Arrange
        Repository<PlayerGeneralProfile, long> repository = CreateRepository();
        PlayerGeneralProfile entityFirst = new()
        {
            FirstName = "First name 1",
            LastName = "Last Name 1",
            City = "City 1",
            Player = new PlayerEntity { Id = 1 }
        };
        PlayerGeneralProfile entitySecond = new()
        {
            FirstName = "First name 2",
            LastName = "Last Name 2",
            City = "City 2",
            Player = new PlayerEntity { Id = 2 }
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
    public async Task Persistence_Repository_ShouldNotFoundEntities()
    {
        // Arrange
        Repository<PlayerGeneralProfile, long> repository = CreateRepository();

        // Act
        IReadOnlyList<PlayerGeneralProfile> generalProfiles = await repository.ListAllAsync();

        // Assert
        Assert.Empty(generalProfiles);
    }

    [Fact]
    [Trait("Persistence", "Repository")]
    public async Task Persistence_Repository_ShouldFind()
    {
        // Arrange
        Repository<PlayerGeneralProfile, long> repository = CreateRepository();
        PlayerGeneralProfile entityFirst = new()
        {
            FirstName = "First name 1",
            LastName = "Last Name 1",
            City = "City 1",
            Player = new PlayerEntity { Id = 1 }
        };
        PlayerGeneralProfile entitySecond = new()
        {
            FirstName = "First name 2",
            LastName = "Last Name 2",
            City = "City 2",
            Player = new PlayerEntity { Id = 2 }
        };
        PlayerGeneralProfile entityThird = new()
        {
            FirstName = "First name 3",
            LastName = "Last Name 3",
            City = "City 1",
            Player = new PlayerEntity { Id = 3 }
        };
        FindParameters<PlayerGeneralProfile> parameters = new()
        {
            Filters = new Filters<PlayerGeneralProfile>(new Filter<PlayerGeneralProfile>[1] {
                new() {
                    Condition = true,
                    Expression = player =>player.City.Contains('1')
                }
            }),
            Pagination = new Pagination { Page = 1, Size = 2 },
            Sorting = new Sortings<PlayerGeneralProfile>(new Sorting<PlayerGeneralProfile, dynamic>[1] {
                new() {
                    Condition = true,
                    Direction = SortingDirection.Descending,
                    Expression = player=>player.City
                }
            })
        };

        // Act
        await repository.AddAsync(entityFirst);
        await repository.AddAsync(entitySecond);
        await repository.AddAsync(entityThird);
        PagedList<PlayerGeneralProfile> result = await repository.FindAsync(parameters);

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal(entityFirst.City, result[0].City!);
        Assert.Equal(entityThird.City, result[1].City!);
        Assert.Equal(1, result.CurrentPage);
        Assert.Equal(1, result.TotalPages);
        Assert.Equal(2, result.PageSize);
        Assert.Equal(2, result.TotalCount);
    }

    private Repository<PlayerGeneralProfile, long> CreateRepository()
    {
        Mock<IMediator> mediatorMock = new();

        Mock<IUserService> userServiceMock = new();

        Mock<IDateTimeService> dateTimeServiceMock = new();

        Mock<AuditableEntitySaveChangesInterceptor> interceptorMock = new(userServiceMock.Object, dateTimeServiceMock.Object);

        PlayerDbContext context = new(_dbContextOptions, mediatorMock.Object, interceptorMock.Object);

        return new Repository<PlayerGeneralProfile, long>(context);
    }
}
