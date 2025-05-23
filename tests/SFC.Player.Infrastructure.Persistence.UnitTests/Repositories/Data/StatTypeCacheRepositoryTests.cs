//using MediatR;

//using Microsoft.AspNetCore.Http;
//using Microsoft.EntityFrameworkCore;

//using Moq;

//using SFC.Player.Application.Common.Constants;
//using SFC.Player.Application.Interfaces.Cache;
//using SFC.Player.Application.Interfaces.Common;
//using SFC.Player.Domain.Entities.Data;
//using SFC.Player.Infrastructure.Constants;
//using SFC.Player.Infrastructure.Persistence.Constants;
//using SFC.Player.Infrastructure.Persistence.Contexts;
//using SFC.Player.Infrastructure.Persistence.Interceptors;
//using SFC.Player.Infrastructure.Persistence.Repositories.Data;

//namespace SFC.Player.Infrastructure.Persistence.UnitTests.Repositories.Data;
//public class StatTypeCacheRepositoryTests
//{
//    private readonly DbContextOptions<DataDbContext> _dbContextOptions;
//    private readonly Mock<ICache> _cacheMock = new();
//    private Mock<StatTypeRepository> _repositoryMock = new();

//    public StatTypeCacheRepositoryTests()
//    {
//        _dbContextOptions = new DbContextOptionsBuilder<DataDbContext>()
//            .UseInMemoryDatabase($"StatTypeCacheRepositoryTestsDb_{DateTime.Now.ToFileTimeUtc()}").Options;
//    }

//    [Fact]
//    [Trait("Persistence", "Repository")]
//    public async Task Persistence_CacheRepository_Data_StatType_ShouldListAllEntitiesFromDatabase()
//    {
//        // Arrange
//        StatTypeCacheRepository repository = CreateRepository();
//        StatType entityFirst = new() { Id = 1, Title = "Acceleration" };
//        StatType entitySecond = new() { Id = 2, Title = "Speed" };
//        string cacheKey = $"{CacheConstants.DATA_CACHE_INSTANCE_NAME}:{typeof(StatType).Name}";
//        IReadOnlyList<StatType>? list = new List<StatType>
//        {
//            entityFirst,
//            entitySecond
//        };
//        _cacheMock.Setup(c => c.TryGet(cacheKey, out list)).Returns(false);
//        _repositoryMock.Setup(c => c.ListAllAsync()).ReturnsAsync(list);

//        // Act       
//        IReadOnlyList<StatType> result = await repository.ListAllAsync();

//        // Assert
//        Assert.Equal(2, result.Count);
//        Assert.Equal(entityFirst.Id, result[0].Id);
//        Assert.Equal(entitySecond.Id, result[1].Id);
//        _repositoryMock.Verify(c => c.ListAllAsync(), Times.Once);
//        _cacheMock.Verify(c => c.TryGet(cacheKey, out list), Times.Once);
//    }

//    [Fact]
//    [Trait("Persistence", "Repository")]
//    public async Task Persistence_CacheRepository_Data_StatType_ShouldListAllEntitiesFromCache()
//    {
//        // Arrange
//        StatTypeCacheRepository repository = CreateRepository();
//        StatType entityFirst = new() { Id = 1, Title = "Acceleration" };
//        StatType entitySecond = new() { Id = 2, Title = "Speed" };
//        string cacheKey = $"{CacheConstants.DATA_CACHE_INSTANCE_NAME}:{typeof(StatType).Name}";
//        IReadOnlyList<StatType> list = new List<StatType>
//        {
//            entityFirst,
//            entitySecond
//        };
//        _cacheMock.Setup(c => c.TryGet(cacheKey, out list)).Returns(true);

//        // Act
//        IReadOnlyList<StatType> result = await repository.ListAllAsync();

//        // Assert
//        Assert.Equal(2, result.Count);
//        Assert.Equal(entityFirst.Id, result[0].Id);
//        Assert.Equal(entitySecond.Id, result[1].Id);
//        _repositoryMock.Verify(c => c.ListAllAsync(), Times.Never);
//        _cacheMock.Verify(c => c.TryGet(cacheKey, out list), Times.Once);
//    }

//    [Fact]
//    [Trait("Persistence", "Repository")]
//    public async Task Persistence_CacheRepository_Data_StatType_ShouldNotFoundEntitiesInDatabaseAndCache()
//    {
//        // Arrange
//        string cacheKey = $"{CacheConstants.DATA_CACHE_INSTANCE_NAME}:{typeof(StatType).Name}";
//        StatTypeCacheRepository repository = CreateRepository();
//        IReadOnlyList<StatType> list = new List<StatType>();
//        _cacheMock.Setup(c => c.TryGet(cacheKey, out list)).Returns(false);
//        _repositoryMock.Setup(c => c.ListAllAsync()).ReturnsAsync(list);

//        // Act
//        IReadOnlyList<StatType> result = await repository.ListAllAsync();

//        // Assert
//        Assert.Empty(result);
//        _repositoryMock.Verify(c => c.ListAllAsync(), Times.Once);
//        _cacheMock.Verify(c => c.TryGet(cacheKey, out list), Times.Once);
//    }

//    [Fact]
//    [Trait("Persistence", "Repository")]
//    public async Task Persistence_CacheRepository_Data_StatType_ShouldReturnCountFromDatabase()
//    {
//        // Arrange
//        StatTypeCacheRepository repository = CreateRepository();
//        StatType entityFirst = new() { Id = 1, Title = "Acceleration" };
//        StatType entitySecond = new() { Id = 2, Title = "Speed" };
//        string cacheKey = $"{CacheConstants.DATA_CACHE_INSTANCE_NAME}:{typeof(StatType).Name}";
//        IReadOnlyList<StatType>? list = new List<StatType>
//        {
//            entityFirst,
//            entitySecond
//        };
//        _cacheMock.Setup(c => c.TryGet(cacheKey, out list)).Returns(false);
//        _repositoryMock.Setup(c => c.CountAsync()).ReturnsAsync(2);

//        // Act       
//        int result = await repository.CountAsync();

//        // Assert
//        Assert.Equal(2, result);
//        _repositoryMock.Verify(c => c.CountAsync(), Times.Once);
//        _cacheMock.Verify(c => c.TryGet(cacheKey, out list), Times.Once);
//    }

//    [Fact]
//    [Trait("Persistence", "Repository")]
//    public async Task Persistence_CacheRepository_Data_StatType_ShouldReturnCountFromCache()
//    {
//        // Arrange
//        StatTypeCacheRepository repository = CreateRepository();
//        StatType entityFirst = new() { Id = 1, Title = "Acceleration" };
//        StatType entitySecond = new() { Id = 2, Title = "Speed" };
//        string cacheKey = $"{CacheConstants.DATA_CACHE_INSTANCE_NAME}:{typeof(StatType).Name}";
//        IReadOnlyList<StatType> list = new List<StatType>
//        {
//            entityFirst,
//            entitySecond
//        };
//        _cacheMock.Setup(c => c.TryGet(cacheKey, out list)).Returns(true);

//        // Act
//        int result = await repository.CountAsync();

//        // Assert
//        Assert.Equal(2, result);
//        _repositoryMock.Verify(c => c.CountAsync(), Times.Never);
//        _cacheMock.Verify(c => c.TryGet(cacheKey, out list), Times.Once);
//    }

//    private StatTypeCacheRepository CreateRepository(bool mockRepository = true)
//    {
//        Mock<IMediator> mediatorMock = new();

//        Mock<IHttpContextAccessor> httpContextAccessorMock = new();

//        Mock<IDateTimeService> dateTimeServiceMock = new();

//        Mock<AuditableEntitySaveChangesInterceptor> interceptorMock = new(httpContextAccessorMock.Object, dateTimeServiceMock.Object);

//        DataDbContext context = new(_dbContextOptions, mediatorMock.Object, interceptorMock.Object);

//        StatTypeRepository repository;

//        if (mockRepository)
//        {
//            _repositoryMock = new(context);
//            repository = _repositoryMock.Object;
//        }
//        else
//        {
//            repository = new StatTypeRepository(context);
//        }

//        return new StatTypeCacheRepository(repository, _cacheMock.Object);
//    }
//}
