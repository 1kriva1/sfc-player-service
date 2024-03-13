using Moq;
using SFC.Player.Domain.Events;
using SFC.Player.Application.Features.Players.Notifications.PlayersByFilters;
using Microsoft.Extensions.Logging;
using SFC.Player.Application.Interfaces.Cache;
using Microsoft.Extensions.Options;
using SFC.Player.Application.Common.Settings;
using PlayerEntity = SFC.Player.Domain.Entities.Player;

namespace SFC.Player.Application.UnitTests.Features.Players.Notifications.PlayersByFilters;
public class PlayersByFiltersNotificationHandlerTests
{
    private readonly Mock<ILogger<PlayersByFiltersEvent>> _loggerMock = new();
    private readonly Mock<ICache> _cacheMock = new();
    private readonly Mock<IOptions<CacheSettings>> _cacheSettingsMock = new();
    private readonly CacheSettings _cacheSettings = new() { InstanceName = "SFC.PlayerTest" };

    public PlayersByFiltersNotificationHandlerTests()
    {
        _cacheSettingsMock.Setup(x => x.Value).Returns(_cacheSettings);
    }

    [Fact]
    [Trait("Feature", "Notification")]
    public async Task Feature_Notification_PlayersByFilters_ShouldSetCacheValues()
    {
        // Arrange
        string cacheKey = $"{_cacheSettings.InstanceName}:{typeof(HitPlayer).Name}";
        PlayersByFiltersEvent @event = new(new List<PlayerEntity> { new() { Id = 0 }, new() { Id = 1 } });

        _cacheMock.Setup(c => c.GetAsync<long>($"{cacheKey}:SearchCount", It.IsAny<CancellationToken>()))
            .ReturnsAsync(0);
        _cacheMock.Setup(c => c.GetAsync<IEnumerable<HitPlayer>>($"{cacheKey}", It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<HitPlayer> { new() { PlayerId = 0, Hits = 2 } });

        PlayersByFiltersNotificationHandler handler = new(_loggerMock.Object, _cacheMock.Object, _cacheSettingsMock.Object);

        // Act
        await handler.Handle(@event, CancellationToken.None);

        // Assert
        _cacheMock.Verify(mock => mock.SetAsync<long>($"{cacheKey}:SearchCount", 1, TimeSpan.FromMinutes(60), It.IsAny<CancellationToken>()), Times.Once());
        _cacheMock.Verify(mock => mock.SetAsync<IEnumerable<HitPlayer>>(cacheKey, It.IsAny<IEnumerable<HitPlayer>>(), TimeSpan.FromMinutes(60), It.IsAny<CancellationToken>()), Times.Once());
    }
}
