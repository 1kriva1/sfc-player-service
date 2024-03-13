using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Player.Queries.GetByFilters.Dto.Result;
using SFC.Player.Application.Features.Player.Common;

namespace SFC.Player.Application.Features.Player.GetByFilters.Result;
public class PlayerByFiltersGeneralProfileModel : IMapFrom<PlayerByFiltersGeneralProfileDto>
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Photo { get; set; }

    public DateTime? Birthday { get; set; }

    public string City { get; set; } = null!;

    public bool FreePlay { get; set; }

    public IEnumerable<string> Tags { get; set; } = new List<string>();

    public PlayerAvailabilityModel Availability { get; set; } = null!;
}
