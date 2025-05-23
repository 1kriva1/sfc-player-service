using SFC.Player.Application.Common.Dto.Identity;
using SFC.Player.Application.Common.Enums;
using SFC.Player.Application.Features.Common.Base;

namespace SFC.Player.Application.Features.Identity.Commands.CreateRange;
public class CreateUsersCommand : Request
{
    public override RequestId RequestId { get => RequestId.CreateUsers; }

    public IEnumerable<UserDto> Users { get; set; } = null!;
}
