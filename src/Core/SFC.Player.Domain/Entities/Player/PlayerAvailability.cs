namespace SFC.Player.Domain.Entities.Player;
public class PlayerAvailability : BasePlayerEntity
{
    public TimeSpan? From { get; set; }

    public TimeSpan? To { get; set; }

    public ICollection<PlayerAvailableDay> Days { get; init; } = [];
}
