using SFC.Player.Application.Common.Enums;
using SFC.Player.Application.Features.Common.Base;

namespace SFC.Player.Application.Features.Player.Queries.Get;

public class GetPlayerQuery : Request<GetPlayerViewModel>
{
    public override RequestId RequestId { get => RequestId.GetPlayer; }

    public long PlayerId { get; set; }
}
