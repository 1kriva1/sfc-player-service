using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using SFC.Player.Infrastructure.Extensions;

namespace SFC.Player.Infrastructure.UnitTests.Extensions;
public class RedisExtensionsTests
{
    [Fact]
    [Trait("Extension", "Redis")]
    public void Extension_Redis_ShouldRegisterCoreServices()
    {
        // Arrange
        Dictionary<string, string> initialData = new()
        {
            {"ConnectionStrings:Redis", "127.0.0.1:6379,abortConnect=false,connectTimeout=30000,responseTimeout=30000"}
        };
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(initialData!)
            .Build();
        IServiceCollection services = new ServiceCollection();

        // Act
        IServiceCollection assertedServices = services.AddRedis(configuration);
        using IServiceScope scope = assertedServices.BuildServiceProvider().CreateScope();

        // Assert
        Assert.NotNull(scope.ServiceProvider.GetService<IDistributedCache>());
    }
}
