using Microsoft.Extensions.Configuration;

using SFC.Player.Application.Common.Settings;
using SFC.Player.Infrastructure.Extensions;
using SFC.Player.Infrastructure.Settings;

namespace SFC.Player.Infrastructure.UnitTests.Extensions;
public class SettingsExtensionsTests
{
    [Fact]
    [Trait("Extension", "Settings")]
    public void Extension_Settings_ShouldGetJwtSettings()
    {
        // Arrange
        Dictionary<string, string> initialData = new()
        {
            {"Jwt:Key", "key"},
            {"Jwt:Issuer", "issuer"},
            {"Jwt:Audience", "audience"}
        };

        IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(initialData!)
            .Build();

        // Act
        JwtSettings result = configuration.GetJwtSettings();

        // Assert
        Assert.NotNull(result);
        Assert.Equal("key", result.Key);
        Assert.Equal("issuer", result.Issuer);
        Assert.Equal("audience", result.Audience);
    }

    [Fact]
    [Trait("Extension", "Settings")]
    public void Extension_Settings_ShouldGetRabbitMqSettings()
    {
        // Arrange
        Dictionary<string, string> initialData = new()
        {
            {"ConnectionStrings:RabbitMq", "rabbitmq://127.0.0.1:5672"},
            {"RabbitMq:Username", "guest"},
            {"RabbitMq:Password", "guest"},
            {"RabbitMq:Name", "SFC.Player"},
            {"RabbitMq:Retry:Limit", "5"},
            {"RabbitMq:Retry:Intervals:0", "15"},
            {"RabbitMq:Exchanges:Data:Key", "data"},
            {"RabbitMq:Exchanges:Data:Value:Init:Name", "sfc.data.init"},
            {"RabbitMq:Exchanges:Data:Value:Init:Type", "direct"},
            {"RabbitMq:Exchanges:Data:Value:Require:Name", "sfc.data.require"},
            {"RabbitMq:Exchanges:Data:Value:Require:Type", "direct"},
            {"RabbitMq:Exchanges:Data:Value:Require:RoutingKey", "DATA_REQUIRE"}
        };

        IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(initialData!)
            .Build();

        // Act
        RabbitMqSettings result = configuration.GetRabbitMqSettings();

        // Assert
        Assert.NotNull(result);
        Assert.Equal("SFC.Player", result.Name);
        Assert.Equal(5, result.Retry.Limit);
        Assert.Equal("data", result.Exchanges.Data.Key);
    }

    [Fact]
    [Trait("Extension", "Settings")]
    public void Extension_Settings_ShouldGetCacheSettings()
    {
        // Arrange
        Dictionary<string, string> initialData = new()
        {
            {"Cache:Enabled", "true"},
            {"Cache:AbsoluteExpirationInMinutes", "15"},
            {"Cache:InstanceName", "SFC.Player"}
        };

        IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(initialData!)
            .Build();

        // Act
        CacheSettings result = configuration.GetCacheSettings();

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Enabled);
        Assert.Equal(15, result.AbsoluteExpirationInMinutes);
        Assert.Equal("SFC.Player", result.InstanceName);
    }
}
