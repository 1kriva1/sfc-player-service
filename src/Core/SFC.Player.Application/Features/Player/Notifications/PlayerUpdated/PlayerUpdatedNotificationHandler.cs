using MediatR;

using SFC.Player.Application.Interfaces.Player;
using SFC.Player.Domain.Events.Player;

namespace SFC.Player.Application.Features.Player.Notifications.PlayerUpdated;
public class PlayerUpdatedNotificationHandler(IPlayerService notification) : INotificationHandler<PlayerUpdatedEvent>
{
    private readonly IPlayerService _notification = notification;

    public Task Handle(PlayerUpdatedEvent notification, CancellationToken cancellationToken)
    {
        return _notification.NotifyPlayerUpdatedAsync(notification.Player, cancellationToken);
    }
}
