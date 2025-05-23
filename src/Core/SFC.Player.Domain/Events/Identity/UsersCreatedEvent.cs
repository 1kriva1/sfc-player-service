using SFC.Player.Domain.Common;
using SFC.Player.Domain.Entities.Identity;

namespace SFC.Player.Domain.Events.Identity;
public class UsersCreatedEvent(IEnumerable<User> users) : BaseEvent
{
    public IEnumerable<User> Users { get; } = users;
}
