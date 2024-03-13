using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Player.Queries.GetByUser.Dto;

namespace SFC.Player.Application.Features.Player.GetByUser.Result;
public class PlayerByUserModel : IMapFrom<PlayerByUserDto>
{
    public long Id { get; set; }

    public PlayerProfileByUserModel Profile { get; set; } = null!;
}
