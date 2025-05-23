//using MediatR;

//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;
//using Microsoft.Extensions.Logging;

//using Moq;

//using SFC.Player.Application.Interfaces.Common;
//using SFC.Player.Application.Interfaces.Identity;
//using SFC.Player.Infrastructure.Persistence.Contexts;
//using SFC.Player.Infrastructure.Persistence.Interceptors;
//using SFC.Player.Infrastructure.Services.Hosted;

//namespace SFC.Player.Infrastructure.UnitTests.Services.Hosted;
//public class DatabaseResetHostedServiceTests
//{
//    private readonly Mock<ILogger<DatabaseResetHostedService>> _loggerMock = new();
//    private readonly DbContextOptions<PlayerDbContext> _dbContextOptions;

//    public DatabaseResetHostedServiceTests()
//    {
//        _dbContextOptions = new DbContextOptionsBuilder<PlayerDbContext>()
//           .UseInMemoryDatabase($"DatabaseResetHostedServiceTestsDb_{DateTime.Now.ToFileTimeUtc()}")
//           .Options;
//    }

//    [Fact]
//    [Trait("Service", "DatabaseResetHosted")]
//    public async Task Service_Hosted_DatabaseReset_ShouldCreateDatabase()
//    {
//        // Arrange
//        IServiceCollection services = new ServiceCollection();
//        PlayerDbContext context = CreateDbContext();
//        services.AddSingleton(context);
//        Mock<IHostEnvironment> hostEnvironmentMock = new();
//        hostEnvironmentMock.Setup(m => m.EnvironmentName).Returns("Development");

//        IHostedService service = new DatabaseResetHostedService(_loggerMock.Object, services.BuildServiceProvider(), hostEnvironmentMock.Object);

//        // Act
//        await service.StartAsync(new CancellationToken());

//        // Assert
//        Assert.True(context.Database.CanConnect());
//    }

//    private PlayerDbContext CreateDbContext()
//    {
//        Mock<IMediator> mediatorMock = new();
//        Mock<IUserService> userServiceMock = new();
//        Mock<IDateTimeService> dateTimeServiceMock = new();
//        AuditableEntitySaveChangesInterceptor interceptor = new(userServiceMock.Object, dateTimeServiceMock.Object);

//        return new(_dbContextOptions, mediatorMock.Object, interceptor);
//    }
//}
