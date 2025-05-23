namespace SFC.Player.Messages.Models.Player;
public class PlayerGeneralProfile
{
    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public string? Biography { get; set; }

    public DateTime? Birthday { get; set; }

    public required string City { get; set; }

    public bool FreePlay { get; set; }
}
