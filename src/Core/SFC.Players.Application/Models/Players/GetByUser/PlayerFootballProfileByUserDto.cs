using SFC.Players.Application.Common.Mappings;
using SFC.Players.Domain.Entities;
using SFC.Players.Domain.Enums;

namespace SFC.Players.Application.Models.Players.GetByUser;
public class PlayerFootballProfileByUserDto: IMapFrom<PlayerFootballProfile>
{
    public FootballPosition? Position { get; set; }
}
