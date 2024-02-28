using MediatR;

using Microsoft.EntityFrameworkCore;

using Moq;

using SFC.Players.Application.Interfaces.Common;
using SFC.Players.Application.Interfaces.Identity;
using SFC.Players.Domain.Entities.Data;
using SFC.Players.Infrastructure.Persistence.Interceptors;

using SFC.Players.Infrastructure.Persistence.Repositories;

namespace SFC.Players.Infrastructure.Persistence.UnitTests.Repositories;
public class DataRepositoryTests
{
    private readonly DbContextOptions<PlayersDbContext> dbContextOptions;

    public DataRepositoryTests()
    {
        dbContextOptions = new DbContextOptionsBuilder<PlayersDbContext>()
            .UseInMemoryDatabase($"UserRepositoryTestsDb_{DateTime.Now.ToFileTimeUtc()}")
        .Options;
    }

    [Fact]
    [Trait("Persistence", "Repository")]
    public async Task Persistence_Repository_Data_ShouldFindDataEntityById()
    {
        // Arrange
        DataRepository<FootballPosition> repository = CreateRepository();
        FootballPosition entity = new()
        {
            Id = 0,
            Title = "Title"
        };

        // Act
        await repository.AddAsync(entity);
        bool result = await repository.AnyAsync(entity.Id);

        // Assert
        Assert.True(result);
    }

    [Fact]
    [Trait("Persistence", "Repository")]
    public async Task Persistence_Repository_User_ShouldNotFindDataEntityById()
    {
        // Arrange
        DataRepository<FootballPosition> repository = CreateRepository();
        FootballPosition entity = new()
        {
            Id = 0,
            Title = "Title"
        };

        // Act
        await repository.AddAsync(entity);
        bool result = await repository.AnyAsync(1);

        // Assert
        Assert.False(result);
    }

    [Fact]
    [Trait("Persistence", "Repository")]
    public async Task Persistence_Repository_User_ShouldResetDataEntities()
    {
        // Arrange
        DataRepository<FootballPosition> repository = CreateRepository();
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

    private DataRepository<FootballPosition> CreateRepository()
    {
        Mock<IMediator> mediatorMock = new();

        Mock<IUserService> userServiceMock = new();

        Mock<IDateTimeService> dateTimeServiceMock = new();

        Mock<AuditableEntitySaveChangesInterceptor> interceptorMock = new(userServiceMock.Object, dateTimeServiceMock.Object);

        PlayersDbContext context = new(dbContextOptions, mediatorMock.Object, interceptorMock.Object);

        return new DataRepository<FootballPosition>(context);
    }
}
