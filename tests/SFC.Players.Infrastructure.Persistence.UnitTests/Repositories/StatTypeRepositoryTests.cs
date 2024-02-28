using MediatR;

using Microsoft.EntityFrameworkCore;

using Moq;

using SFC.Players.Application.Interfaces.Common;
using SFC.Players.Application.Interfaces.Identity;
using SFC.Players.Application.Interfaces.Persistence;
using SFC.Players.Domain.Entities.Data;
using SFC.Players.Infrastructure.Persistence.Interceptors;

using SFC.Players.Infrastructure.Persistence.Repositories;

namespace SFC.Players.Infrastructure.Persistence.UnitTests.Repositories;
public class StatTypeRepositoryTests
{
    private readonly DbContextOptions<PlayersDbContext> dbContextOptions;

    public StatTypeRepositoryTests()
    {
        dbContextOptions = new DbContextOptionsBuilder<PlayersDbContext>()
            .UseInMemoryDatabase($"UserRepositoryTestsDb_{DateTime.Now.ToFileTimeUtc()}")
        .Options;
    }

    [Fact]
    [Trait("Persistence", "Repository")]
    public async Task Persistence_Repository_StatType_ShouldReturnCount()
    {
        // Arrange
        IStatTypeRepository repository = CreateRepository();
        StatType entity = new()
        {
            Id = 2,
            Title = "Title"
        };

        // Act
        await CreateDataRepository().AddAsync(entity);
        int result = await repository.CountAsync();

        // Assert
        Assert.Equal(1, result);
    }

    [Fact]
    [Trait("Persistence", "Repository")]
    public async Task Persistence_Repository_StatType_ShouldReturnCountByIds()
    {
        // Arrange
        IStatTypeRepository repository = CreateRepository();
        StatType entity = new()
        {
            Id = 2,
            Title = "Title"
        };

        // Act
        await CreateDataRepository().AddAsync(entity);
        int result = await repository.CountAsync(new int[] { 1, 2, 3 });

        // Assert
        Assert.Equal(1, result);
    }

    [Fact]
    [Trait("Persistence", "Repository")]
    public async Task Persistence_Repository_StatType_ShouldReturnAllEntities()
    {
        // Arrange
        IStatTypeRepository repository = CreateRepository();
        StatType entity = new()
        {
            Id = 2,
            Title = "Title"
        };

        // Act
        await CreateDataRepository().AddAsync(entity);
        IReadOnlyList<StatType> result = await repository.ListAllAsync();

        // Assert
        Assert.Single(result);
    }

    [Fact]
    [Trait("Persistence", "Repository")]
    public async Task Persistence_Repository_StatType_ShouldReturnAllAsNoTracking()
    {
        // Arrange
        PlayersDbContext context = CreateContext();
        IStatTypeRepository repository = new StatTypeRepository(context);
        StatType entity = new()
        {
            Id = 2,
            Title = "Title"
        };

        // Act
        await CreateDataRepository().AddRangeAsync(
            new StatType() { Id = 1, Title = "Title1" },
            new StatType() { Id = 2, Title = "Title2" }
        );
        IReadOnlyList<StatType> result = await repository.ListAllAsync();

        // Assert
        foreach (StatType statType in result)
        {
            Assert.Equal(EntityState.Detached, context.Entry(statType).State);
        }        
    }

    private PlayersDbContext CreateContext()
    {
        Mock<IMediator> mediatorMock = new();

        Mock<IUserService> userServiceMock = new();

        Mock<IDateTimeService> dateTimeServiceMock = new();

        Mock<AuditableEntitySaveChangesInterceptor> interceptorMock = new(userServiceMock.Object, dateTimeServiceMock.Object);

        return new(dbContextOptions, mediatorMock.Object, interceptorMock.Object);
    }

    private IStatTypeRepository CreateRepository()
    {
        PlayersDbContext context = CreateContext();

        return new StatTypeRepository(context);
    }

    private DataRepository<StatType> CreateDataRepository()
    {
        PlayersDbContext context = CreateContext();

        return new DataRepository<StatType>(context);
    }
}
