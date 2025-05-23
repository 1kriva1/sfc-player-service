using SFC.Player.Application.Common.Dto.Common;
using SFC.Player.Application.Common.Mappings.Interfaces;
using SFC.Player.Domain.Entities.Identity;

namespace SFC.Player.Application.Common.Dto.Identity;
public class UserDto : AuditableDto, IMapTo<User>
{
    public Guid Id { get; set; }
}