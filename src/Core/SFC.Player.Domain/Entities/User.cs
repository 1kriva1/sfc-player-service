using IdentityUser = SFC.Player.Domain.Entities.Identity.User;

namespace SFC.Player.Domain.Entities;
public class User: BaseEntity<Guid>
{
    public Player Player { get; set; } = null!;

    public IdentityUser IdentityUser { get; set; } = default!;
}
