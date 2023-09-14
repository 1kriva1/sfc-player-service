using SFC.Players.Application.Common.Mappings;
using SFC.Players.Application.Models.Players.Common.Models;

namespace SFC.Players.Application.Models.Players.Common;
public class PlayerProfileDto: IMapFrom<PlayerProfileModel>
{
    public PlayerGeneralProfileDto General { get; set; } = null!;

    public PlayerFootballProfileDto Football { get; set; } = null!;
}