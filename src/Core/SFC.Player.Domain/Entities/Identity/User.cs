using SFC.Player.Domain.Common.Interfaces;

using PlayerUser = SFC.Player.Domain.Entities.User;

namespace SFC.Player.Domain.Entities.Identity;
public class User : BaseEntity<Guid>, IExternalAuditableEntity
{
    public PlayerUser PlayerUser { get; set; } = default!;

    public DateTime CreatedDate { get; set; }
}
