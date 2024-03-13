using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Player.Queries.GetByUser.Dto;

namespace SFC.Player.Application.Features.Player.GetByUser.Result;
public class PlayerFootballProfileByUserModel : IMapFrom<PlayerFootballProfileByUserDto>
{
    public int? Position { get; set; }
}
