namespace SFC.Player.Application.Interfaces.Cache;
public interface ICache
{
    Task<T> GetAsync<T>(string key, CancellationToken token = default);

    bool TryGet<T>(string key, out T value);

    Task<bool> ExistsAsync(string key, CancellationToken token = default);

    Task SetAsync<T>(string key, T value, TimeSpan? expiry = null, CancellationToken token = default);

    Task DeleteAsync(string key, CancellationToken token = default);

    Task<T> GetOrSetAsync<T>(string key, Func<T> factory, TimeSpan? expiry = null, CancellationToken token = default);
}
