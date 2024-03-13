using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Player.Common.Dto;
using SFC.Player.Application.Features.Player.Queries.GetByUser.Dto;

namespace SFC.Player.Application.Features.Player.GetByUser.Result;
public class PlayerGeneralProfileByUserModel : IMapFrom<PlayerGeneralProfileByUserDto>
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Photo { get; set; }
}
