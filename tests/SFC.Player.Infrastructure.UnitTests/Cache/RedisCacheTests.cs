using System.Text;
using System.Text.Json;

using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;

using Moq;

using SFC.Player.Application.Common.Settings;
using SFC.Player.Application.Interfaces.Cache;
using SFC.Player.Infrastructure.Cache;

namespace SFC.Player.Infrastructure.UnitTests.Cache;
public class RedisCacheTests
{
    private readonly Mock<IOptions<CacheSettings>> _cacheSettingsMock = new();

    public RedisCacheTests()
    {
        _cacheSettingsMock.Setup(x => x.Value).Returns(new CacheSettings { AbsoluteExpirationInMinutes = 15, SlidingExpirationInMinutes = 45 });
    }

    [Fact]
    [Trait("Cache", "Redis")]
    public async Task Cache_Redis_ShouldSetWithDefaultExpiry()
    {
        // Arrange
        Mock<IDistributedCache> cacheMock = new();
        string key = "set_key";
        object value = new { Id = 1, Value = "Test" };

        // Act
        ICache cache = new RedisCache(cacheMock.Object, _cacheSettingsMock.Object);
        await cache.SetAsync(key, value);

        // Assert
        cacheMock.Verify(m => m.SetAsync(key, It.IsAny<byte[]>(), It.IsAny<DistributedCacheEntryOptions>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    [Trait("Cache", "Redis")]
    public async Task Cache_Redis_ShouldSetWithDefinedExpiry()
    {
        // Arrange
        Mock<IDistributedCache> cacheMock = new();
        string key = "set_key";
        object value = new { Id = 1, Value = "Test" };
        TimeSpan expiry = TimeSpan.FromSeconds(5);

        // Act
        ICache cache = new RedisCache(cacheMock.Object, _cacheSettingsMock.Object);
        await cache.SetAsync(key, value, expiry);

        // Assert
        cacheMock.Verify(m => m.SetAsync(key, It.IsAny<byte[]>(), It.IsAny<DistributedCacheEntryOptions>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    [Trait("Cache", "Redis")]
    public async Task Cache_Redis_ShouldGetValue()
    {
        // Arrange
        Mock<IDistributedCache> cacheMock = new();
        string key = "set_key";
        CancellationToken cancelToken = CancellationToken.None;
        int assertResult = 5;
        cacheMock.Setup(m => m.GetAsync(key, cancelToken)).ReturnsAsync(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(assertResult)));

        // Act
        ICache cache = new RedisCache(cacheMock.Object, _cacheSettingsMock.Object);
        int result = await cache.GetAsync<int>(key, cancelToken);

        // Assert
        Assert.Equal(assertResult, result);
    }

    [Fact]
    [Trait("Cache", "Redis")]
    public async Task Cache_Redis_ShouldGetNullValue()
    {
        // Arrange
        Mock<IDistributedCache> cacheMock = new();
        string key = "set_key";
        CancellationToken cancelToken = CancellationToken.None;
        cacheMock.Setup(m => m.GetAsync(key, cancelToken)).ReturnsAsync((byte[]?)null);

        // Act
        ICache cache = new RedisCache(cacheMock.Object, _cacheSettingsMock.Object);
        int? result = await cache.GetAsync<int?>(key, cancelToken);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    [Trait("Cache", "Redis")]
    public async Task Cache_Redis_ShouldDelete()
    {
        // Arrange
        Mock<IDistributedCache> cacheMock = new();
        string key = "set_key";

        // Act
        ICache cache = new RedisCache(cacheMock.Object, _cacheSettingsMock.Object);
        await cache.DeleteAsync(key);

        // Assert
        cacheMock.Verify(m => m.RemoveAsync(key, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    [Trait("Cache", "Redis")]
    public async Task Cache_Redis_ShouldValueExist()
    {
        // Arrange
        Mock<IDistributedCache> cacheMock = new();
        string key = "set_key";
        CancellationToken cancelToken = CancellationToken.None;
        cacheMock.Setup(m => m.GetAsync(key, cancelToken)).ReturnsAsync(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(5)));

        // Act
        ICache cache = new RedisCache(cacheMock.Object, _cacheSettingsMock.Object);
        bool result = await cache.ExistsAsync(key, cancelToken);

        // Assert
        Assert.True(result);
    }

    [Fact]
    [Trait("Cache", "Redis")]
    public async Task Cache_Redis_ShouldValueNotExist()
    {
        // Arrange
        Mock<IDistributedCache> cacheMock = new();
        string key = "set_key";
        CancellationToken cancelToken = CancellationToken.None;
        cacheMock.Setup(m => m.GetAsync(key, cancelToken)).ReturnsAsync((byte[]?)null);

        // Act
        ICache cache = new RedisCache(cacheMock.Object, _cacheSettingsMock.Object);
        bool result = await cache.ExistsAsync(key, cancelToken);

        // Assert
        Assert.False(result);
    }

    [Fact]
    [Trait("Cache", "Redis")]
    public void Cache_Redis_ShouldTryGetNotNullValue()
    {
        // Arrange
        Mock<IDistributedCache> cacheMock = new();
        string key = "set_key";
        int assertResult = 5;
        cacheMock.Setup(m => m.Get(key)).Returns(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(assertResult)));

        // Act
        ICache cache = new RedisCache(cacheMock.Object, _cacheSettingsMock.Object);
        bool result = cache.TryGet<int>(key, out int value);

        // Assert
        Assert.True(result);
        Assert.Equal(assertResult, value);
    }

    [Fact]
    [Trait("Cache", "Redis")]
    public void Cache_Redis_ShouldTryGetNullValue()
    {
        // Arrange
        Mock<IDistributedCache> cacheMock = new();
        string key = "set_key";
        cacheMock.Setup(m => m.Get(key)).Returns((byte[]?)null);

        // Act
        ICache cache = new RedisCache(cacheMock.Object, _cacheSettingsMock.Object);
        bool result = cache.TryGet<int?>(key, out int? value);

        // Assert
        Assert.False(result);
        Assert.Null(value);
    }

    [Fact]
    [Trait("Cache", "Redis")]
    public async Task Cache_Redis_ShouldGetValueWithoutSet()
    {
        // Arrange
        Mock<IDistributedCache> cacheMock = new();
        string key = "set_key";
        CancellationToken cancelToken = CancellationToken.None;
        int assertValue = 5;
        cacheMock.Setup(m => m.Get(key)).Returns(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(assertValue)));

        // Act
        ICache cache = new RedisCache(cacheMock.Object, _cacheSettingsMock.Object);
        int result = await cache.GetOrSetAsync(key, () => assertValue, null, cancelToken);

        // Assert
        Assert.Equal(assertValue, result);
        cacheMock.Verify(m => m.SetAsync(key, It.IsAny<byte[]>(), It.IsAny<DistributedCacheEntryOptions>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    [Trait("Cache", "Redis")]
    public async Task Cache_Redis_ShouldGetValueWithSet()
    {
        // Arrange
        Mock<IDistributedCache> cacheMock = new();
        string key = "set_key";
        CancellationToken cancelToken = CancellationToken.None;
        int assertValue = 5;
        cacheMock.Setup(m => m.Get(key)).Returns((byte[]?)null);

        // Act
        ICache cache = new RedisCache(cacheMock.Object, _cacheSettingsMock.Object);
        int result = await cache.GetOrSetAsync(key, () => assertValue, null, cancelToken);

        // Assert
        Assert.Equal(assertValue, result);
        cacheMock.Verify(m => m.SetAsync(key, It.IsAny<byte[]>(), It.IsAny<DistributedCacheEntryOptions>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
