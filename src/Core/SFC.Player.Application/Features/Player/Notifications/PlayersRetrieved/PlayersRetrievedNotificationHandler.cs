using MediatR;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using SFC.Player.Application.Common.Settings;
using SFC.Player.Application.Features.Player.Notifications.GetPlayers;
using SFC.Player.Application.Interfaces.Cache;
using SFC.Player.Domain.Events.Player;

namespace SFC.Player.Application.Features.Player.Notifications.PlayersRetrieved;
public class PlayersRetrievedNotificationHandler : INotificationHandler<PlayersRetrievedEvent>
{
    private const int TOTAL = 10;
    private const int EXPIRATION_IN_MINUTES = 60;

    private readonly ILogger<PlayersRetrievedEvent> _logger;
    private readonly ICache _cache;
    private readonly IOptions<CacheSettings> _cacheConfig;
    private readonly string _hitPlayersKey;
    private readonly string _searchCountKey;
    private readonly decimal _excludeCoefficient;

    public PlayersRetrievedNotificationHandler(ILogger<PlayersRetrievedEvent> logger, ICache cache, IOptions<CacheSettings> cacheConfig)
    {
        _logger = logger;
        _cache = cache;
        _cacheConfig = cacheConfig;
        _excludeCoefficient = (decimal)1 / TOTAL;
        _hitPlayersKey = $"{_cacheConfig.Value.InstanceName}:{typeof(HitPlayer).Name}";
        _searchCountKey = $"{_hitPlayersKey}:SearchCount";
    }

    public async Task Handle(PlayersRetrievedEvent notification, CancellationToken cancellationToken)
    {
        long searchCount = await _cache.GetAsync<long>(_searchCountKey, cancellationToken).ConfigureAwait(true);

        List<HitPlayer> existingPlayers = (await _cache.GetAsync<IEnumerable<HitPlayer>>(_hitPlayersKey, cancellationToken).ConfigureAwait(true))?.ToList()
            ?? [];

        IEnumerable<HitPlayer> newPlayers = notification.Players.Where(np => !existingPlayers.Any(p => p.PlayerId == np.Id))
                                                                .Select(s => new HitPlayer { Hits = 1, PlayerId = s.Id });

        existingPlayers.Where(ep => notification.Players.Any(p => p.Id == ep.PlayerId))
                       .ToList()
                       .ForEach(player => player.Hits += 1);

#pragma warning disable CA1851 // Possible multiple enumerations of 'IEnumerable' collection
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
#pragma warning restore CA1851 // Possible multiple enumerations of 'IEnumerable' collection

        await _cache.SetAsync(_searchCountKey, ++searchCount, TimeSpan.FromMinutes(EXPIRATION_IN_MINUTES), cancellationToken).ConfigureAwait(false);

        await _cache.SetAsync<IEnumerable<HitPlayer>>(_hitPlayersKey, existingPlayers, TimeSpan.FromMinutes(EXPIRATION_IN_MINUTES), cancellationToken).ConfigureAwait(false);
    }
}
