using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Player.Queries.GetByFilters.Dto.Result;

namespace SFC.Player.Application.Features.Player.GetByFilters.Result;
public class PlayerByFiltersProfileModel : IMapFrom<PlayerByFiltersProfileDto>
{
    public PlayerByFiltersGeneralProfileModel General { get; set; } = null!;

    public PlayerByFiltersFootballProfileModel Football { get; set; } = null!;
}
