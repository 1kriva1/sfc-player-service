using SFC.Players.Application.Common.Mappings;
using SFC.Players.Application.Features.Players.Queries.GetByFilters.Dto.Result;

namespace SFC.Players.Application.Models.Players.GetByFilters.Result;
public class PlayerByFiltersProfileModel : IMapFrom<PlayerByFiltersProfileDto>
{
    public PlayerByFiltersGeneralProfileModel General { get; set; } = null!;

    public PlayerByFiltersFootballProfileModel Football { get; set; } = null!;
}
