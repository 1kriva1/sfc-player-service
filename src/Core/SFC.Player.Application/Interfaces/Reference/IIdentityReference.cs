using SFC.Player.Application.Common.Dto.Identity;
using SFC.Player.Domain.Entities.Identity;

namespace SFC.Player.Application.Interfaces.Reference;
public interface IIdentityReference : IReference<User, Guid, UserDto> { }
