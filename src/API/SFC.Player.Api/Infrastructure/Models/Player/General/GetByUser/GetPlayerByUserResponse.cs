using AutoMapper;

using SFC.Player.Api.Infrastructure.Models.Base;
using SFC.Player.Application.Common.Extensions;
using SFC.Player.Application.Common.Mappings.Interfaces;
using SFC.Player.Application.Features.Player.Queries.GetByUser;

namespace SFC.Player.Api.Infrastructure.Models.Player.General.GetByUser;

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
    public GetByUserPlayerModel? Player { get; set; } = null!;

    public void Mapping(Profile profile) => profile.CreateMap<GetPlayerByUserViewModel, GetPlayerByUserResponse>()
                                                   .IgnoreAllNonExisting();
}
