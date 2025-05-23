using SFC.Player.Domain.Common;

namespace SFC.Player.Domain.Entities.Player;
public class PlayerAvailableDay : BaseEntity<long>
{
    public PlayerAvailability Availability { get; set; } = null!;

    public DayOfWeek Day { get; set; }
}
