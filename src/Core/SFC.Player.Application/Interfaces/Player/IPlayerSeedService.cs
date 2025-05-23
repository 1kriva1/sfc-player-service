namespace SFC.Player.Application.Interfaces.Player;
public interface IPlayerSeedService
{
    Task<IEnumerable<PlayerEntity>> GetSeedPlayersAsync();

    Task SeedPlayersAsync(CancellationToken cancellationToken = default);
}
