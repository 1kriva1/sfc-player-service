using SFC.Players.Application.Common.Mappings;
using SFC.Players.Application.Features.Players.Queries.GetByUser.Dto;

namespace SFC.Players.Application.Models.Players.GetByUser.Result;
public class PlayerFootballProfileByUserModel : IMapFrom<PlayerFootballProfileByUserDto>
{
    public int? Position { get; set; }
}
