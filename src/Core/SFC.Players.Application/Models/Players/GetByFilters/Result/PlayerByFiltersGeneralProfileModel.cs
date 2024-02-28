using SFC.Players.Application.Common.Mappings;
using SFC.Players.Application.Features.Players.Queries.GetByFilters.Dto.Result;
using SFC.Players.Application.Models.Players.Common;

namespace SFC.Players.Application.Models.Players.GetByFilters.Result;
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
