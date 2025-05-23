//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Options;

//using SFC.Player.Application.Common.Settings;
//using SFC.Player.Infrastructure.Cache;
//using SFC.Player.Infrastructure.Extensions;

//namespace SFC.Player.Infrastructure.UnitTests.Extensions;
//public class CacheExtensionsTests
//{
//    [Fact]
//    [Trait("Extension", "Cache")]
//    public void Extension_Cache_ShouldHaveDefinedSetting()
//    {
//        // Arrange
//        Dictionary<string, string> initialData = new()
//        {
//            {"Cache:Enabled", "true"},
//            {"Cache:InstanceName", "SFC.Player"},
//            {"Cache:AbsoluteExpirationInMinutes", "15"},
//            {"Cache:SlidingExpirationInMinutes", "45"}
//        };

//        IConfigurationRoot configuration = new ConfigurationBuilder()
//            .AddInMemoryCollection(initialData!)
//            .Build();
//        IServiceCollection services = new ServiceCollection();

//        // Act
//        IServiceCollection assertedServices = services.AddCache(configuration);

//        using IServiceScope scope = assertedServices.BuildServiceProvider().CreateScope();

//        IOptions<CacheSettings>? result = scope.ServiceProvider.GetService<IOptions<CacheSettings>>();

//        // Assert
//        Assert.NotNull(result);
//        Assert.True(result.Value.Enabled);
//        Assert.Equal("SFC.Player", result.Value.InstanceName);
//        Assert.Equal(15, result.Value.AbsoluteExpirationInMinutes);
//        Assert.Equal(45, result.Value.SlidingExpirationInMinutes);
//    }

//    [Fact]
//    [Trait("Extension", "Cache")]
//    public void Extension_Cache_ShouldRegisterCacheServices()
//    {
//        // Arrange
//        IConfigurationRoot configuration = new ConfigurationBuilder().Build();
//        IServiceCollection services = new ServiceCollection();

//        // Act
//        IServiceCollection assertedServices = services.AddCache(configuration);

//        // Assert
//        Assert.NotNull(assertedServices.FirstOrDefault(s => s.ImplementationType == typeof(RedisCache)));
//    }
//}
