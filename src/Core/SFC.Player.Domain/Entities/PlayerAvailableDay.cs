namespace SFC.Player.Domain.Entities;
 public class PlayerAvailableDay : BaseEntity<long>
{
    public PlayerAvailability Availability { get; set; } = null!;

    public DayOfWeek Day { get; set; }
}
