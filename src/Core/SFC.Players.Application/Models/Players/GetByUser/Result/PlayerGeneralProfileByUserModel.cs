using SFC.Players.Application.Common.Mappings;
using SFC.Players.Application.Features.Players.Common.Dto;
using SFC.Players.Application.Features.Players.Queries.GetByUser.Dto;

namespace SFC.Players.Application.Models.Players.GetByUser.Result;
public class PlayerGeneralProfileByUserModel : IMapFrom<PlayerGeneralProfileByUserDto>
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Photo { get; set; }
}
