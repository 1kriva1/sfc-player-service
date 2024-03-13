namespace SFC.Player.Application.Features.Player.Common.Models;
public interface IPlayerRelatedRequest
{
    public long PlayerId { get; set; }

    public Guid UserId { get; }
}
