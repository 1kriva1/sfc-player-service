using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using SFC.Players.Application;
using SFC.Players.Application.Interfaces.Identity;
using SFC.Players.Application.Interfaces.Persistence;
using SFC.Players.Infrastructure.Persistence.Interceptors;

namespace SFC.Players.Infrastructure.Persistence.UnitTests;
public class PersistenceRegistrationTests
{
    private class UserServiceTest : IUserService { public Guid UserId => throw new NotImplementedException(); }

    private readonly WebApplicationBuilder _builder = WebApplication.CreateBuilder();

    [Fact]
    [Trait("Registration", "Servises")]
    public void PersistenceRegistration_Execute_ServicesAreRegistered()
    {
        // Arrange
        IConfiguration configuration = new ConfigurationBuilder()
           .AddInMemoryCollection(
               new KeyValuePair<string, string?>[1] { new KeyValuePair<string, string?>("ConnectionString", "Value") })
           .Build();
        _builder.Services.AddApplicationServices();
        _builder.AddInfrastructureServices();
        _builder.Services.AddTransient<IUserService, UserServiceTest>();
        _builder.Services.AddPersistenceServices(configuration);

        using WebApplication application = _builder.Build();

        // Act
        ServiceProvider serviceProvider = _builder.Services.BuildServiceProvider();

        // Assert
        Assert.NotNull(serviceProvider.GetService<AuditableEntitySaveChangesInterceptor>());
        Assert.NotNull(serviceProvider.GetService<IPlayersDbContext>());
        Assert.NotNull(serviceProvider.GetService<IPlayerRepository>());
        Assert.NotNull(serviceProvider.GetService<IUserRepository>());
        Assert.NotNull(serviceProvider.GetService<IStatCategoryRepository>());
        Assert.NotNull(serviceProvider.GetService<IStatTypeRepository>());
    }
}
