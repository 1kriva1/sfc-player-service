using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Moq;

using SFC.Players.Application.Interfaces.Common;
using SFC.Players.Application.Interfaces.Identity;
using SFC.Players.Infrastructure.Persistence;
using SFC.Players.Infrastructure.Persistence.Interceptors;
using SFC.Players.Infrastructure.Services.Hosted;

namespace SFC.Players.Infrastructure.UnitTests.Services.Hosted;
public class DatabaseResetHostedServiceTests
{
    private readonly Mock<ILogger<DatabaseResetHostedService>> _loggerMock = new();
    private readonly DbContextOptions<PlayersDbContext> dbContextOptions;

    public DatabaseResetHostedServiceTests()
    {
        dbContextOptions = new DbContextOptionsBuilder<PlayersDbContext>()
           .UseInMemoryDatabase($"DatabaseResetHostedServiceTestsDb_{DateTime.Now.ToFileTimeUtc()}")
           .Options;
    }

    [Fact]
    [Trait("Service", "DatabaseResetHosted")]
    public async Task Service_Hosted_DatabaseReset_ShouldCreateDatabase()
    {
        // Arrange
        IServiceCollection services = new ServiceCollection();
        PlayersDbContext context = CreateDbContext();
        services.AddSingleton(context);
        Mock<IHostEnvironment> hostEnvironmentMock = new();
        hostEnvironmentMock.Setup(m => m.EnvironmentName).Returns("Development");

        IHostedService service = new DatabaseResetHostedService(_loggerMock.Object, services.BuildServiceProvider(), hostEnvironmentMock.Object);

        // Act
        await service.StartAsync(new CancellationToken());

        // Assert
        Assert.True(context.Database.CanConnect());
    }

    private PlayersDbContext CreateDbContext()
    {
        Mock<IMediator> mediatorMock = new();
        Mock<IUserService> userServiceMock = new();
        Mock<IDateTimeService> dateTimeServiceMock = new();
        AuditableEntitySaveChangesInterceptor interceptor = new(userServiceMock.Object, dateTimeServiceMock.Object);

        return new(dbContextOptions, mediatorMock.Object, interceptor);
    }
}
