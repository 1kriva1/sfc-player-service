using MediatR;

using Microsoft.EntityFrameworkCore;

using Moq;

using SFC.Players.Application.Interfaces.Common;
using SFC.Players.Application.Interfaces.Identity;
using SFC.Players.Domain.Entities;
using SFC.Players.Infrastructure.Persistence.Interceptors;

using SFC.Players.Infrastructure.Persistence.Repositories;

namespace SFC.Players.Infrastructure.Persistence.UnitTests.Repositories;
public class UserRepositoryTests
{
    private readonly DbContextOptions<PlayersDbContext> dbContextOptions;

    public UserRepositoryTests()
    {
        dbContextOptions = new DbContextOptionsBuilder<PlayersDbContext>()
            .UseInMemoryDatabase($"UserRepositoryTestsDb_{DateTime.Now.ToFileTimeUtc()}")
        .Options;
    }

    [Fact]
    [Trait("Persistence", "Repository")]
    public async Task Persistence_Repository_User_ShouldFindUserEntityByUserId()
    {
        // Arrange
        UserRepository repository = CreateRepository();
        Guid userId = Guid.NewGuid();
        User entity = new()
        {
            UserId = userId,
            Player = new Player { Id = 1 }
        };

        // Act
        await repository.AddAsync(entity);
        bool result = await repository.AnyAsync(userId);

        // Assert
        Assert.True(result);
    }

    [Fact]
    [Trait("Persistence", "Repository")]
    public async Task Persistence_Repository_User_ShouldNotFindUserEntityByUserId()
    {
        // Arrange
        UserRepository repository = CreateRepository();
        Guid userId = Guid.NewGuid();
        User entity = new()
        {
            UserId = userId,
            Player = new Player { Id = 1 }
        };

        // Act
        await repository.AddAsync(entity);
        bool result = await repository.AnyAsync(Guid.NewGuid());

        // Assert
        Assert.False(result);
    }

    [Fact]
    [Trait("Persistence", "Repository")]
    public async Task Persistence_Repository_User_ShouldFindUserEntityByUserIdAndPlayerId()
    {
        // Arrange
        UserRepository repository = CreateRepository();
        Guid userId = Guid.NewGuid();
        User entity = new()
        {
            UserId = userId,
            Player = new Player { Id = 1 }
        };

        // Act
        await repository.AddAsync(entity);
        bool result = await repository.AnyAsync(1, userId);

        // Assert
        Assert.True(result);
    }

    [Fact]
    [Trait("Persistence", "Repository")]
    public async Task Persistence_Repository_User_ShouldNotFindUserEntityByUserIdAndPlayerId()
    {
        // Arrange
        UserRepository repository = CreateRepository();
        Guid userId = Guid.NewGuid();
        User entity = new()
        {
            UserId = userId,
            Player = new Player { Id = 1 }
        };

        // Act
        await repository.AddAsync(entity);
        bool resultByInvalidPlayerId = await repository.AnyAsync(2, userId);
        bool resultByInvalidUserId = await repository.AnyAsync(1, Guid.NewGuid());
        bool resultByInvalidPlayerAndUserId = await repository.AnyAsync(2, Guid.NewGuid());

        // Assert
        Assert.False(resultByInvalidPlayerId);
        Assert.False(resultByInvalidUserId);
        Assert.False(resultByInvalidPlayerAndUserId);
    }

    private UserRepository CreateRepository()
    {
        Mock<IMediator> mediatorMock = new();

        Mock<IUserService> userServiceMock = new();

        Mock<IDateTimeService> dateTimeServiceMock = new();

        Mock<AuditableEntitySaveChangesInterceptor> interceptorMock = new(userServiceMock.Object, dateTimeServiceMock.Object);

        PlayersDbContext context = new(dbContextOptions, mediatorMock.Object, interceptorMock.Object);

        return new UserRepository(context);
    }
}
