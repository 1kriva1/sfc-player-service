using SFC.Player.Application.Common.Enums;
using SFC.Player.Application.Features.Common.Base;
using SFC.Player.Application.Features.Player.Queries.Find.Dto.Filters;

namespace SFC.Player.Application.Features.Player.Queries.Find;
public class GetPlayersQuery : BasePaginationRequest<GetPlayersViewModel, GetPlayersFilterDto>
{
    public override RequestId RequestId { get => RequestId.GetPlayers; }
}
