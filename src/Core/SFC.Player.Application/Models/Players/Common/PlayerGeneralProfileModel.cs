using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Player.Common.Dto;

namespace SFC.Player.Application.Features.Player.Common;
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
