using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Player.Common.Dto;

namespace SFC.Player.Application.Features.Player.Common;
public class PlayerProfileModel : IMapFrom<PlayerProfileDto>
{
    public PlayerGeneralProfileModel General { get; set; } = null!;

    public PlayerFootballProfileModel Football { get; set; } = null!;
}