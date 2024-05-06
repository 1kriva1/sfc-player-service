using AutoMapper;

using SFC.Player.Application.Common.Extensions;
using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Players.Queries.Get;
using SFC.Player.Application.Models.Base;
using SFC.Player.Application.Models.Players.GetByUser.Result;

namespace SFC.Player.Application.Models.Players.GetByUser;

/// <summary>
/// **Get** player by user response model.
/// </summary>
public class GetPlayerByUserResponse :
    BaseErrorResponse, IMapFrom<GetPlayerByUserViewModel>
{
    public static GetPlayerByUserResponse SuccessResult => new();

    /// <summary>
    /// Player model.
    /// </summary>
    public PlayerModel? Player { get; set; } = null!;

    public void Mapping(Profile profile) => profile.CreateMap<GetPlayerByUserViewModel, GetPlayerByUserResponse>()
                                                   .IgnoreAllNonExisting();
}
