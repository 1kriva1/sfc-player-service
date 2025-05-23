using MediatR;

using SFC.Player.Application.Interfaces.Player;
using SFC.Player.Domain.Events.Player;

namespace SFC.Player.Application.Features.Player.Notifications.PlayerCreated;
public class PlayerCreatedNotificationHandler(IPlayerService notification) : INotificationHandler<PlayerCreatedEvent>
{
    private readonly IPlayerService _notification = notification;

    public Task Handle(PlayerCreatedEvent notification, CancellationToken cancellationToken)
    {
        return _notification.NotifyPlayerCreatedAsync(notification.Player, cancellationToken);
    }
}
