using SFC.Player.Application.Common.Enums;
using SFC.Player.Application.Features.Common.Base;

namespace SFC.Player.Application.Features.Player.Commands.Create;
public class CreatePlayerCommand : Request<CreatePlayerViewModel>
{
    public override RequestId RequestId { get => RequestId.CreatePlayer; }

    public CreatePlayerDto Player { get; set; } = null!;
}
