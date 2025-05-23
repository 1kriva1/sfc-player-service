namespace SFC.Player.Application.Interfaces.Player;
public interface IPlayerService
{
    Task NotifyPlayerCreatedAsync(PlayerEntity player, CancellationToken cancellationToken = default);

    Task NotifyPlayerUpdatedAsync(PlayerEntity player, CancellationToken cancellationToken = default);
}
