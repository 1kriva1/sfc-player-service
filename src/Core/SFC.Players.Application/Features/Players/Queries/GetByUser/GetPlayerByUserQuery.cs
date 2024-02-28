using SFC.Players.Application.Common.Enums;
using SFC.Players.Application.Features.Common.Base;

namespace SFC.Players.Application.Features.Players.Queries.Get;

public class GetPlayerByUserQuery : Request<GetPlayerByUserViewModel>
{
    public override RequestId RequestId { get => RequestId.GetPlayerByUser; }
}
