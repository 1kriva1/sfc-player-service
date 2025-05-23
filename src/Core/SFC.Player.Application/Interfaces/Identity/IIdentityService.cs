using SFC.Player.Application.Common.Dto.Identity;

namespace SFC.Player.Application.Interfaces.Identity;
public interface IIdentityService
{
    Task<UserDto?> GetUserAsync(Guid id, CancellationToken cancellationToken = default);
}
