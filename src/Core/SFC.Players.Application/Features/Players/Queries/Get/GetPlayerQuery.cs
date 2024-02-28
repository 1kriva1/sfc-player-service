using SFC.Players.Application.Common.Enums;
using SFC.Players.Application.Features.Common.Base;
using SFC.Players.Application.Features.Players.Common.Models;

namespace SFC.Players.Application.Features.Players.Queries.Get;

public class GetPlayerQuery : Request<GetPlayerViewModel>, IPlayerRelatedRequest
{
    public override RequestId RequestId { get => RequestId.GetPlayer; }

    public long PlayerId { get; set; }
}
