using MediatR;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using SFC.Player.Application.Common.Settings;
using SFC.Player.Application.Interfaces.Cache;
using SFC.Player.Domain.Events;

namespace SFC.Player.Application.Features.Players.Notifications.PlayersByFilters;
public class PlayersByFiltersNotificationHandler : INotificationHandler<PlayersByFiltersEvent>
{
    private const int TOTAL = 10;
    private const int EXPIRATION_IN_MINUTES = 60;

    private readonly ILogger<PlayersByFiltersEvent> _logger;
    private readonly ICache _cache;
    private readonly IOptions<CacheSettings> _cacheConfig;
    private readonly string _hitPlayersKey;
    private readonly string _searchCountKey;
    private readonly decimal _excludeCoefficient;

    public PlayersByFiltersNotificationHandler(ILogger<PlayersByFiltersEvent> logger, ICache cache, IOptions<CacheSettings> cacheConfig)
    {
        _logger = logger;
        _cache = cache;
        _cacheConfig = cacheConfig;
        _excludeCoefficient = (decimal)1 / TOTAL;
        _hitPlayersKey = $"{_cacheConfig.Value.InstanceName}:{typeof(HitPlayer).Name}";
        _searchCountKey = $"{_hitPlayersKey}:SearchCount";
    }

    public async Task Handle(PlayersByFiltersEvent notification, CancellationToken cancellationToken)
    {
        long searchCount = await _cache.GetAsync<long>(_searchCountKey, cancellationToken);

        List<HitPlayer> existingPlayers = (await _cache.GetAsync<IEnumerable<HitPlayer>>(_hitPlayersKey, cancellationToken))?.ToList() ?? new List<HitPlayer>();

        IEnumerable<HitPlayer> newPlayers = notification.Players.Where(np => !existingPlayers.Any(p => p.PlayerId == np.Id))
                                                                .Select(s => new HitPlayer { Hits = 1, PlayerId = s.Id });

        existingPlayers.Where(ep => notification.Players.Any(p => p.Id == ep.PlayerId))
                       .ToList()
                       .ForEach(player => player.Hits += 1);

        if (newPlayers.Any())
        {
            IEnumerable<HitPlayer> excludePlayers = existingPlayers.Where(p => (decimal)p.Hits / Math.Max(searchCount, 1) <= _excludeCoefficient)
                                                                   .Take(newPlayers.Count());

            existingPlayers = existingPlayers.Where(ep => !excludePlayers.Contains(ep))
                                             .ToList();
        }

        int missingCount = TOTAL - existingPlayers.Count;

        if (missingCount > 0 && newPlayers.Any())
        {
            IEnumerable<HitPlayer> includePlayers = newPlayers.Take(missingCount);
            existingPlayers.AddRange(includePlayers);
        }

        await _cache.SetAsync(_searchCountKey, ++searchCount, TimeSpan.FromMinutes(EXPIRATION_IN_MINUTES), cancellationToken);

        await _cache.SetAsync<IEnumerable<HitPlayer>>(_hitPlayersKey, existingPlayers, TimeSpan.FromMinutes(EXPIRATION_IN_MINUTES), cancellationToken);
    }
}
