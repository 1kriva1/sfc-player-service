namespace SFC.Players.Domain.Entities;
public class IdentityUser : BaseAuditableEntity
{
    public Guid UserId { get; set; }

    public User? User { get; set; }
}
