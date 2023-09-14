namespace SFC.Players.Domain.Entities;
public class User : BasePlayerEntity
{
    public Guid UserId { get; set; }

    public IdentityUser? IdentityUser { get; set; }
}
