using SFC.Player.Application.Common.Enums;
using SFC.Player.Application.Features.Common.Base;

namespace SFC.Player.Application.Features.Player.Commands.Update;

public class UpdatePlayerCommand : Request
{
    public override RequestId RequestId { get => RequestId.UpdatePlayer; }

    public long PlayerId { get; set; }

    public UpdatePlayerDto Player { get; set; } = null!;

    public UpdatePlayerCommand SetPlayerId(long id)
    {
        PlayerId = id;
        return this;
    }
}
