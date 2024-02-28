using SFC.Players.Application.Common.Mappings;
using SFC.Players.Application.Features.Players.Common.Dto;

namespace SFC.Players.Application.Models.Players.Common;
public class PlayerProfileModel : IMapFrom<PlayerProfileDto>
{
    public PlayerGeneralProfileModel General { get; set; } = null!;

    public PlayerFootballProfileModel Football { get; set; } = null!;
}