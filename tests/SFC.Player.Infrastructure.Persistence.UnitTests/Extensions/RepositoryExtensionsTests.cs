using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SFC.Player.Infrastructure.Persistence.Extensions;
using SFC.Player.Application.Interfaces.Persistence;
using SFC.Player.Infrastructure.Persistence.Repositories.Data;

namespace SFC.Player.Infrastructure.Persistence.UnitTests.Extensions;
public class RepositoryExtensionsTests
{
    [Fact]
    [Trait("Persistence", "Extensions")]
    public void Persistence_Extensions_Repository_ShouldRegisterCacheIndependent()
    {
        // Arrange
        Dictionary<string, string> initialData = GetConfiguration(false);

        IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(initialData!)
            .Build();
        IServiceCollection services = new ServiceCollection();

        // Act
        IServiceCollection assertedServices = services.AddRepositories(configuration);

        // Assert
        Assert.NotNull(assertedServices.FirstOrDefault(s => s.ServiceType == typeof(IRepository<,>)));
        Assert.NotNull(assertedServices.FirstOrDefault(s => s.ServiceType == typeof(IUserRepository)));
        Assert.NotNull(assertedServices.FirstOrDefault(s => s.ServiceType == typeof(IPlayerRepository)));
    }

    [Fact]
    [Trait("Persistence", "Extensions")]
    public void Persistence_Extensions_Repository_ShouldRegisterForEnabledCache()
    {
        // Arrange
        Dictionary<string, string> initialData = GetConfiguration(true);

        IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(initialData!)
            .Build();
        IServiceCollection services = new ServiceCollection();

        // Act
        IServiceCollection assertedServices = services.AddRepositories(configuration);

        // Assert
        Assert.NotNull(assertedServices.FirstOrDefault(s => s.ServiceType == typeof(IDataRepository<>)
            && s.ImplementationType == typeof(DataCacheRepository<>)));
        Assert.NotNull(assertedServices.FirstOrDefault(s => s.ImplementationType == typeof(DataRepository<>)));
        Assert.NotNull(assertedServices.FirstOrDefault(s => s.ImplementationType == typeof(StatTypeRepository)));
        Assert.NotNull(assertedServices.FirstOrDefault(s => s.ServiceType == typeof(IStatTypeRepository)
            && s.ImplementationType == typeof(StatTypeCacheRepository)));
    }

    [Fact]
    [Trait("Persistence", "Extensions")]
    public void Persistence_Extensions_Repository_ShouldRegisterForВшіфидувCache()
    {
        // Arrange
        Dictionary<string, string> initialData = GetConfiguration(false);

        IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(initialData!)
            .Build();
        IServiceCollection services = new ServiceCollection();

        // Act
        IServiceCollection assertedServices = services.AddRepositories(configuration);

        // Assert
        Assert.NotNull(assertedServices.FirstOrDefault(s => s.ServiceType == typeof(IDataRepository<>)
            && s.ImplementationType == typeof(DataRepository<>)));
        Assert.NotNull(assertedServices.FirstOrDefault(s => s.ServiceType == typeof(IStatTypeRepository)
            && s.ImplementationType == typeof(StatTypeRepository)));
    }

    private static Dictionary<string, string> GetConfiguration(bool enabled)
    {
        return new()
        {
            {"Cache:Enabled", enabled.ToString()},
            {"Cache:InstanceName", "SFC.Data"},
            {"Cache:AbsoluteExpirationInMinutes", "15"},
            {"Cache:SlidingExpirationInMinutes", "45"},
            {"Cache:Refresh:Cron", "*/15 * * * *"}
        };
    }
}
