using AutoMapper;

using SFC.Player.Api.Infrastructure.Models.Base;
using SFC.Player.Api.Infrastructure.Models.Player.General.Common;
using SFC.Player.Application.Common.Extensions;
using SFC.Player.Application.Common.Mappings.Interfaces;
using SFC.Player.Application.Features.Player.Queries.Get;

namespace SFC.Player.Api.Infrastructure.Models.Player.General.Get;

/// <summary>
/// **Get** player response model.
/// </summary>
public class GetPlayerResponse : BaseErrorResponse, IMapFrom<GetPlayerViewModel>
{
    /// <summary>
    /// Player model.
    /// </summary>
    public PlayerModel Player { get; set; } = null!;

    public void Mapping(Profile profile) => profile.CreateMap<GetPlayerViewModel, GetPlayerResponse>()
                                                   .IgnoreAllNonExisting();
}
