namespace SFC.Players.Application.Interfaces.Persistence;

public interface IPlayersDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}