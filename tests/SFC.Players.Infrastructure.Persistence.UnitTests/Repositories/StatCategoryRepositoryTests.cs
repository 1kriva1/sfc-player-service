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
public class StatCategoryRepositoryTests
{
    private readonly DbContextOptions<PlayersDbContext> dbContextOptions;

    public StatCategoryRepositoryTests()
    {
        dbContextOptions = new DbContextOptionsBuilder<PlayersDbContext>()
            .UseInMemoryDatabase($"UserRepositoryTestsDb_{DateTime.Now.ToFileTimeUtc()}")
        .Options;
    }

    [Fact]
    [Trait("Persistence", "Repository")]
    public async Task Persistence_Repository_StatCategory_ShouldReturnCountByIds()
    {
        // Arrange
        IStatCategoryRepository repository = CreateRepository();
        StatCategory entity = new()
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

    private PlayersDbContext CreateContext()
    {
        Mock<IMediator> mediatorMock = new();

        Mock<IUserService> userServiceMock = new();

        Mock<IDateTimeService> dateTimeServiceMock = new();

        Mock<AuditableEntitySaveChangesInterceptor> interceptorMock = new(userServiceMock.Object, dateTimeServiceMock.Object);

        return new(dbContextOptions, mediatorMock.Object, interceptorMock.Object);
    }

    private IStatCategoryRepository CreateRepository()
    {
        PlayersDbContext context = CreateContext();

        return new StatCategoryRepository(context);
    }

    private DataRepository<StatCategory> CreateDataRepository()
    {
        PlayersDbContext context = CreateContext();

        return new DataRepository<StatCategory>(context);
    }
}
