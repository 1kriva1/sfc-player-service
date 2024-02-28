using SFC.Players.Application.Common.Mappings;
using SFC.Players.Application.Features.Players.Queries.GetByUser.Dto;

namespace SFC.Players.Application.Models.Players.GetByUser.Result;
public class PlayerByUserModel : IMapFrom<PlayerByUserDto>
{
    public long Id { get; set; }

    public PlayerProfileByUserModel Profile { get; set; } = null!;
}
