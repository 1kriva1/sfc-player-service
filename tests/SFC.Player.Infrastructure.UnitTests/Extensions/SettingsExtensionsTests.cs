using Microsoft.Extensions.Configuration;

using SFC.Player.Application.Common.Settings;
using SFC.Player.Infrastructure.Extensions;
using SFC.Player.Infrastructure.Settings;

namespace SFC.Player.Infrastructure.UnitTests.Extensions;
public class SettingsExtensionsTests
{
    [Fact]
    [Trait("Extension", "Settings")]
    public void Extension_Settings_ShouldReturnUseAuthentication()
    {
        // Arrange
        Dictionary<string, string> initialData = new() { { "Authentication", "true" } };
        ConfigurationManager configuration = new();
        configuration.AddInMemoryCollection(initialData!);

        // Act
        bool result = configuration.UseAuthentication();

        // Assert
        Assert.True(result);
    }

    [Fact]
    [Trait("Extension", "Settings")]
    public void Extension_Settings_ShouldGetIdentitySettings()
    {
        // Arrange
        string authority = "https://localhost:7266",
            audience = "sfc.player",
            claimType = "scope",
            claimValue = "sfc.player.full";
        Dictionary<string, string> initialData = new()
        {
            {"Identity:Authority", authority},
            {"Identity:Audience", audience},
            {$"Identity:RequireClaims:{claimType}:0", claimValue}
        };

        IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(initialData!)
            .Build();

        // Act
        IdentitySettings result = configuration.GetIdentitySettings();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(authority, result.Authority);
        Assert.Equal(audience, result.Audience);
        Assert.Single(result.RequireClaims);

        KeyValuePair<string, IEnumerable<string>> claim = result.RequireClaims.First();
        Assert.Equal(claimType, claim.Key);
        Assert.Single(claim.Value);
        Assert.Equal(claimValue, claim.Value.First());
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
