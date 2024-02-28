using SFC.Players.Application.Common.Mappings;
using SFC.Players.Application.Features.Players.Common.Dto;

namespace SFC.Players.Application.Models.Players.Common;
public class PlayerGeneralProfileModel : IMapFrom<PlayerGeneralProfileDto>
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Photo { get; set; }

    public string? Biography { get; set; }

    public DateTime? Birthday { get; set; }

    public string City { get; set; } = null!;

    public bool FreePlay { get; set; }

    public IEnumerable<string> Tags { get; set; } = Array.Empty<string>();

    public PlayerAvailabilityModel Availability { get; set; } = null!;
}
