namespace SFC.Players.Domain.Entities;
public class Player : BaseAuditableEntity
{
    public PlayerGeneralProfile GeneralProfile { get; set; } = null!;

    public PlayerFootballProfile FootballProfile { get; set; } = null!;
   
    public PlayerAvailability Availability { get; set; } = null!;
    
    public PlayerStatPoints Points { get; set; } = null!;   

    public PlayerPhoto Photo { get; set; } = null!;

    public User User { get; set; } = null!;

    public ICollection<PlayerTag> Tags { get; } = new List<PlayerTag>();

    public ICollection<PlayerStat> Stats { get; } = new List<PlayerStat>();   
}
