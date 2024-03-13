using System.Text;
using System.Text.Json;

using Microsoft.Extensions.Caching.Distributed;

using Microsoft.Extensions.Options;

using SFC.Player.Application.Common.Settings;
using SFC.Player.Application.Interfaces.Cache;

namespace SFC.Player.Infrastructure.Cache;
public class RedisCache : ICache
{
    private readonly IDistributedCache _cache;
    private readonly DistributedCacheEntryOptions? _cacheOptions;

    public RedisCache(IDistributedCache cache, IOptions<CacheSettings> cacheConfig)
    {
        _cache = cache;
        _cacheOptions = new DistributedCacheEntryOptions
        {
            AbsoluteExpiration = DateTime.Now.AddMinutes(cacheConfig.Value.AbsoluteExpirationInMinutes),
            SlidingExpiration = TimeSpan.FromMinutes(cacheConfig.Value.SlidingExpirationInMinutes),
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(cacheConfig.Value.AbsoluteExpirationInMinutes)
        };
    }

    public Task DeleteAsync(string key, CancellationToken token = default)
    {
        return _cache.RemoveAsync(key, token);
    }

    public async Task<bool> ExistsAsync(string key, CancellationToken token = default)
    {
        string? json = await _cache.GetStringAsync(key, token);
        return json is not null;
    }

    public async Task<T> GetAsync<T>(string key, CancellationToken token = default)
    {
        string? json = await _cache.GetStringAsync(key, token);
        return json is null ? default! : JsonSerializer.Deserialize<T>(json)!;
    }

    public Task SetAsync<T>(string key, T value, TimeSpan? expiry = null, CancellationToken token = default)
    {
        string json = JsonSerializer.Serialize<T>(value);

        DistributedCacheEntryOptions? options = null;

        if (expiry is not null)
        {
            options = new DistributedCacheEntryOptions().SetAbsoluteExpiration(expiry.Value);
        }

        return _cache.SetStringAsync(key, json, options ?? _cacheOptions!, token);
    }

    public bool TryGet<T>(string key, out T value)
    {
        string? json = _cache.GetString(key);

        if (json is null)
        {
            value = default!;
            return false;
        }

        value = JsonSerializer.Deserialize<T>(json)!;

        return true;
    }

    public Task<T> GetOrSetAsync<T>(string key, Func<T> factory, TimeSpan? expiry = null, CancellationToken token = default)
    {
        if (TryGet<T>(key, out T result))
        {
            return Task.FromResult(result);
        }

        lock (TypeLock<T>.Lock)
        {
            if (TryGet(key, out result))
            {
                return Task.FromResult(result);
            }

            result = factory();

            SetAsync(key, result, expiry, token);

            return Task.FromResult(result);
        }
    }

    private static class TypeLock<T>
    {
        public static object Lock { get; } = new object();
    }
}
