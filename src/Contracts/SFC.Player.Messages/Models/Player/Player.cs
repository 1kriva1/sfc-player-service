using SFC.Player.Messages.Models.Common;

namespace SFC.Player.Messages.Models.Player;
public class Player: Auditable
{
    public long Id { get; set; }

    public Guid UserId { get; set; }

    public required PlayerGeneralProfile GeneralProfile { get; set; }

    public PlayerFootballProfile? FootballProfile { get; set; }

    public PlayerAvailability? Availability { get; set; }

    public required PlayerStatPoints Points { get; set; }

    public PlayerPhoto? Photo { get; set; }

    public IEnumerable<PlayerTag>? Tags { get; init; }

    public IEnumerable<PlayerStat>? Stats { get; init; }
}
