using MediatR;

using Microsoft.Extensions.Logging;

using SFC.Players.Domain.Events;

namespace SFC.Players.Application.Features.Players.Notifications;
public record PlayerCreatedNotificationHandler(ILogger<PlayerCreatedEvent> Logger) : INotificationHandler<PlayerCreatedEvent>
{
    public Task Handle(PlayerCreatedEvent notification, CancellationToken cancellationToken)
    {
        string message = $"Domain Event: {notification.GetType().Name}";

        Logger.LogInformation(message);

        return Task.CompletedTask;
    }
}
