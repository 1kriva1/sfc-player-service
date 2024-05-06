using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Models.Players.Find.Filters;

namespace SFC.Player.Application.Features.Players.Queries.Find.Dto.Filters;
public class GetPlayersProfileFilterDto : IMapFrom<GetPlayersProfileFilterModel>
{
    public GetPlayersGeneralProfileFilterDto General { get; set; } = default!;

    public GetPlayersFootballProfileFilterDto Football { get; set; } = default!;
}
