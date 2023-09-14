namespace SFC.Players.Domain.Entities;
 public class PlayerAvailableDay : BaseEntity
{
    public PlayerAvailability Availability { get; set; } = null!;

    public DayOfWeek Day { get; set; }
}
