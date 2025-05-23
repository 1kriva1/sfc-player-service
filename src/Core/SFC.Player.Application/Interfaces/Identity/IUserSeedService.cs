namespace SFC.Player.Application.Interfaces.Identity;
public interface IUserSeedService
{
    Task SendRequireUsersSeedAsync(CancellationToken cancellationToken = default);
}