using SFC.Player.Application.Common.Enums;
using SFC.Player.Application.Features.Common.Base;

namespace SFC.Player.Application.Features.Player.Queries.Get;

public class GetPlayerByUserQuery : Request<GetPlayerByUserViewModel>
{
    public override RequestId RequestId { get => RequestId.GetPlayerByUser; }
}
