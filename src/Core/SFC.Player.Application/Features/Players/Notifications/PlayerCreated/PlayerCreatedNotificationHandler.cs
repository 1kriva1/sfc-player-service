using MediatR;

using Microsoft.Extensions.Logging;

using SFC.Player.Domain.Events;

namespace SFC.Player.Application.Features.Players.Notifications.PlayerCreated;
public record PlayerCreatedNotificationHandler(ILogger<PlayerCreatedEvent> Logger) : INotificationHandler<PlayerCreatedEvent>
{
    public Task Handle(PlayerCreatedEvent notification, CancellationToken cancellationToken)
    {
        string message = $"Domain Event: {notification.GetType().Name}";

        Logger.LogInformation(message);

        return Task.CompletedTask;
    }
}
