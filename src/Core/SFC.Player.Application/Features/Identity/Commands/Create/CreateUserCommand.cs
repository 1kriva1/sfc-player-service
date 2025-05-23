using SFC.Player.Application.Common.Enums;
using SFC.Player.Application.Features.Common.Base;
using SFC.Player.Application.Common.Dto.Identity;

namespace SFC.Player.Application.Features.Identity.Commands.Create;
public class CreateUserCommand : Request
{
    public override RequestId RequestId { get => RequestId.CreateUser; }

    public UserDto User { get; set; } = null!;
}
