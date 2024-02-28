using SFC.Players.Application.Common.Mappings;
using SFC.Players.Application.Features.Players.Queries.GetByUser.Dto;

namespace SFC.Players.Application.Models.Players.GetByUser.Result;
public class PlayerProfileByUserModel : IMapFrom<PlayerProfileByUserDto>
{
    public PlayerGeneralProfileByUserModel General { get; set; } = null!;

    public PlayerFootballProfileByUserModel Football { get; set; } = null!;
}
