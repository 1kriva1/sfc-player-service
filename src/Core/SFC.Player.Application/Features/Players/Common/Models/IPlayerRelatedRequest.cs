namespace SFC.Player.Application.Features.Players.Common.Models;
public interface IPlayerRelatedRequest
{
    public long PlayerId { get; set; }

    public Guid UserId { get; }
}
