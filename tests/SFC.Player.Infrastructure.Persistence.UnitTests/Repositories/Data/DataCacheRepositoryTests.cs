using MediatR;

using Microsoft.EntityFrameworkCore;

using Moq;

using SFC.Player.Application.Common.Constants;
using SFC.Player.Application.Interfaces.Cache;
using SFC.Player.Application.Interfaces.Common;
using SFC.Player.Application.Interfaces.Identity;
using SFC.Player.Domain.Entities.Data;
using SFC.Player.Infrastructure.Persistence.Interceptors;
using SFC.Player.Infrastructure.Persistence.Repositories.Data;

namespace SFC.Player.Infrastructure.Persistence.UnitTests.Repositories.Data;
public class DataCacheRepositoryTests
{
    private readonly DbContextOptions<PlayerDbContext> _dbContextOptions;
    private readonly Mock<ICache> _cacheMock = new();
    private Mock<DataRepository<FootballPosition>> _repositoryMock = new();

    public DataCacheRepositoryTests()
    {
        _dbContextOptions = new DbContextOptionsBuilder<PlayerDbContext>()
            .UseInMemoryDatabase($"DataCacheRepositoryTestsDb_{DateTime.Now.ToFileTimeUtc()}").Options;
    }

    [Fact]
    [Trait("Persistence", "Repository")]
    public async Task Persistence_CacheRepository_Data_ShouldListAllEntitiesFromDatabase()
    {
        // Arrange
        DataCacheRepository<FootballPosition> repository = CreateRepository();
        FootballPosition entityFirst = new() { Id = 1, Title = "Defender" };
        FootballPosition entitySecond = new() { Id = 2, Title = "Midfilder" };
        string cacheKey = $"{CacheConstants.DATA_CACHE_INSTANCE_NAME}:{typeof(FootballPosition).Name}";
        IReadOnlyList<FootballPosition>? list = new List<FootballPosition>
        {
            entityFirst,
            entitySecond
        };
        _cacheMock.Setup(c => c.TryGet(cacheKey, out list)).Returns(false);
        _repositoryMock.Setup(c => c.ListAllAsync()).ReturnsAsync(list);

        // Act       
        IReadOnlyList<FootballPosition> result = await repository.ListAllAsync();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal(entityFirst.Id, result[0].Id);
        Assert.Equal(entitySecond.Id, result[1].Id);
        _repositoryMock.Verify(c => c.ListAllAsync(), Times.Once);
        _cacheMock.Verify(c => c.TryGet(cacheKey, out list), Times.Once);
    }

    [Fact]
    [Trait("Persistence", "Repository")]
    public async Task Persistence_CacheRepository_Data_ShouldListAllEntitiesFromCache()
    {
        // Arrange
        DataCacheRepository<FootballPosition> repository = CreateRepository();
        FootballPosition entityFirst = new() { Id = 1, Title = "Defender" };
        FootballPosition entitySecond = new() { Id = 2, Title = "Midfilder" };
        string cacheKey = $"{CacheConstants.DATA_CACHE_INSTANCE_NAME}:{typeof(FootballPosition).Name}";
        IReadOnlyList<FootballPosition> list = new List<FootballPosition>
        {
            entityFirst,
            entitySecond
        };
        _cacheMock.Setup(c => c.TryGet(cacheKey, out list)).Returns(true);

        // Act
        IReadOnlyList<FootballPosition> result = await repository.ListAllAsync();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal(entityFirst.Id, result[0].Id);
        Assert.Equal(entitySecond.Id, result[1].Id);
        _repositoryMock.Verify(c => c.ListAllAsync(), Times.Never);
        _cacheMock.Verify(c => c.TryGet(cacheKey, out list), Times.Once);
    }

    [Fact]
    [Trait("Persistence", "Repository")]
    public async Task Persistence_CacheRepository_Data_ShouldNotFoundEntitiesInDatabaseAndCache()
    {
        // Arrange
        string cacheKey = $"{CacheConstants.DATA_CACHE_INSTANCE_NAME}:{typeof(FootballPosition).Name}";
        DataCacheRepository<FootballPosition> repository = CreateRepository();
        IReadOnlyList<FootballPosition> list = new List<FootballPosition>();
        _cacheMock.Setup(c => c.TryGet(cacheKey, out list)).Returns(false);
        _repositoryMock.Setup(c => c.ListAllAsync()).ReturnsAsync(list);

        // Act
        IReadOnlyList<FootballPosition> result = await repository.ListAllAsync();

        // Assert
        Assert.Empty(result);
        _repositoryMock.Verify(c => c.ListAllAsync(), Times.Once);
        _cacheMock.Verify(c => c.TryGet(cacheKey, out list), Times.Once);
    }

    [Fact]
    [Trait("Persistence", "Repository")]
    public async Task Persistence_CacheRepository_Data_ShouldFindDataEntitiesInDatabase()
    {
        // Arrange
        DataCacheRepository<FootballPosition> repository = CreateRepository();
        string cacheKey = $"{CacheConstants.DATA_CACHE_INSTANCE_NAME}:{typeof(FootballPosition).Name}";
        FootballPosition entity = new() { Id = 0, Title = "Title" };
        IReadOnlyList<FootballPosition> list = new List<FootballPosition> { entity };
        _repositoryMock.Setup(c => c.AnyAsync(0)).ReturnsAsync(true);
        _cacheMock.Setup(c => c.TryGet(cacheKey, out list)).Returns(false);

        // Act
        bool result = await repository.AnyAsync(entity.Id);

        // Assert
        Assert.True(result);
        _repositoryMock.Verify(c => c.AnyAsync(0), Times.Once);
        _cacheMock.Verify(c => c.TryGet(cacheKey, out list), Times.Once);
    }

    [Fact]
    [Trait("Persistence", "Repository")]
    public async Task Persistence_CacheRepository_Data_ShouldFindDataEntitiesInCache()
    {
        // Arrange
        DataCacheRepository<FootballPosition> repository = CreateRepository();
        FootballPosition entity = new() { Id = 0, Title = "Title" };
        string cacheKey = $"{CacheConstants.DATA_CACHE_INSTANCE_NAME}:{typeof(FootballPosition).Name}";
        IReadOnlyList<FootballPosition> list = new List<FootballPosition> { entity };
        _cacheMock.Setup(c => c.TryGet(cacheKey, out list)).Returns(true);

        // Act
        bool result = await repository.AnyAsync(entity.Id);

        // Assert
        Assert.True(result);
        _repositoryMock.Verify(c => c.AnyAsync(0), Times.Never);
        _cacheMock.Verify(c => c.TryGet(cacheKey, out list), Times.Once);
    }

    [Fact]
    [Trait("Persistence", "Repository")]
    public async Task Persistence_CacheRepository_Data_ShouldNotFindDataEntitiesNotInDatabaseNotInCache()
    {
        // Arrange
        DataCacheRepository<FootballPosition> repository = CreateRepository();
        string cacheKey = $"{CacheConstants.DATA_CACHE_INSTANCE_NAME}:{typeof(FootballPosition).Name}";
        IReadOnlyList<FootballPosition> list = new List<FootballPosition>();
        _cacheMock.Setup(c => c.TryGet(cacheKey, out list)).Returns(false);
        _repositoryMock.Setup(c => c.AnyAsync(0)).ReturnsAsync(false);

        // Act
        bool result = await repository.AnyAsync(0);

        // Assert
        Assert.False(result);
        _repositoryMock.Verify(c => c.AnyAsync(0), Times.Once);
        _cacheMock.Verify(c => c.TryGet(cacheKey, out list), Times.Once);
    }

    [Fact]
    [Trait("Persistence", "Repository")]
    public async Task Persistence_CacheRepository_Data_ShouldResetDataEntities()
    {
        // Arrange
        DataCacheRepository<FootballPosition> repository = CreateRepository(false);
        FootballPosition entity = new()
        {
            Id = 0,
            Title = "Title"
        };
        FootballPosition resetEntity = new()
        {
            Id = 1,
            Title = "Title Reset"
        };

        await repository.AddAsync(entity);

        Assert.True(await repository.AnyAsync(entity.Id));

        // Act
        FootballPosition[] result = await repository.ResetAsync(new FootballPosition[] { resetEntity });

        // Assert
        Assert.Single(result);
        Assert.Equal(resetEntity.Id, result.FirstOrDefault()!.Id);
        Assert.False(await repository.AnyAsync(entity.Id));
        Assert.True(await repository.AnyAsync(resetEntity.Id));
    }

    private DataCacheRepository<FootballPosition> CreateRepository(bool mockRepository = true)
    {
        Mock<IMediator> mediatorMock = new();

        Mock<IUserService> userServiceMock = new();

        Mock<IDateTimeService> dateTimeServiceMock = new();

        Mock<AuditableEntitySaveChangesInterceptor> interceptorMock = new(userServiceMock.Object, dateTimeServiceMock.Object);

        PlayerDbContext context = new(_dbContextOptions, mediatorMock.Object, interceptorMock.Object);

        DataRepository<FootballPosition> repository;

        if (mockRepository)
        {
            _repositoryMock = new(context);
            repository = _repositoryMock.Object;
        }
        else
        {
            repository = new DataRepository<FootballPosition>(context);
        }        

        return new DataCacheRepository<FootballPosition>(repository, _cacheMock.Object);
    }
}
