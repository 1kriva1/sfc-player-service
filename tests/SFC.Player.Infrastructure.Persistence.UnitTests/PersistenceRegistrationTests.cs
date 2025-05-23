//using Microsoft.AspNetCore.Builder;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;

//using SFC.Player.Application;
//using SFC.Player.Application.Interfaces.Persistence;
//using SFC.Player.Application.Interfaces.Persistence.Context;
//using SFC.Player.Infrastructure.Persistence.Interceptors;

//namespace SFC.Player.Infrastructure.Persistence.UnitTests;
//public class PersistenceRegistrationTests
//{
//    private readonly WebApplicationBuilder _builder = WebApplication.CreateBuilder();

//    [Fact]
//    [Trait("Registration", "Servises")]
//    public void PersistenceRegistration_Execute_ServicesAreRegistered()
//    {
//        // Arrange
//        IConfiguration configuration = new ConfigurationBuilder()
//           .AddInMemoryCollection(
//               new KeyValuePair<string, string?>[1] { new("ConnectionString", "Value") })
//           .Build();
//        _builder.Services.AddApplicationServices();
//        _builder.AddInfrastructureServices();;
//        _builder.Services.AddPersistenceServices(configuration);

//        using WebApplication application = _builder.Build();

//        // Act
//        ServiceProvider serviceProvider = _builder.Services.BuildServiceProvider();

//        // Assert
//        Assert.NotNull(serviceProvider.GetService<AuditableEntitySaveChangesInterceptor>());
//        Assert.NotNull(serviceProvider.GetService<IPlayerDbContext>());
//    }
//}
