using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Player.Queries.GetByUser.Dto;

namespace SFC.Player.Application.Features.Player.GetByUser.Result;
public class PlayerProfileByUserModel : IMapFrom<PlayerProfileByUserDto>
{
    public PlayerGeneralProfileByUserModel General { get; set; } = null!;

    public PlayerFootballProfileByUserModel Football { get; set; } = null!;
}
