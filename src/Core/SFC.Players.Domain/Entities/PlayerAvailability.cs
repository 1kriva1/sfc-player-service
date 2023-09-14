namespace SFC.Players.Domain.Entities;
public class PlayerAvailability : BasePlayerEntity
{
    public TimeSpan? From { get; set; }

    public TimeSpan? To { get; set; }

    public ICollection<PlayerAvailableDay> Days { get; set; } = new List<PlayerAvailableDay>();
}
