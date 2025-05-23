namespace SFC.Player.Messages.Models.Player;
public class PlayerAvailability
{
    public TimeSpan? From { get; set; }

    public TimeSpan? To { get; set; }

    public ICollection<PlayerAvailableDay> Days { get; init; } = [];
}
