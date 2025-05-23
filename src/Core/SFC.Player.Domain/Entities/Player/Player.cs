using SFC.Player.Domain.Common;
using SFC.Player.Domain.Common.Interfaces;

namespace SFC.Player.Domain.Entities.Player;
public class Player : BaseAuditableEntity<long>, IUserEntity
{
    public Guid UserId { get; set; }

    public PlayerGeneralProfile GeneralProfile { get; set; } = null!;

    public PlayerFootballProfile FootballProfile { get; set; } = null!;

    public PlayerAvailability Availability { get; set; } = null!;

    public PlayerStatPoints Points { get; set; } = null!;

    public PlayerPhoto Photo { get; set; } = null!;

    public ICollection<PlayerTag> Tags { get; } = [];

    public ICollection<PlayerStat> Stats { get; } = [];    
}
