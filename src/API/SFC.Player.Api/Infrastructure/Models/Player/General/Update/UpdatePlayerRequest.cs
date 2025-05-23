using AutoMapper;
using SFC.Player.Application.Common.Mappings.Interfaces;
using SFC.Player.Application.Features.Player.Commands.Update;
using SFC.Player.Application.Common.Extensions;

namespace SFC.Player.Api.Infrastructure.Models.Player.General.Update;

/// <summary>
/// **Update** player request.
/// </summary>
public class UpdatePlayerRequest : IMapTo<UpdatePlayerCommand>
{
    /// <summary>
    /// Player model.
    /// </summary>
    public UpdatePlayerModel Player { get; set; } = null!;

    public void Mapping(Profile profile) => profile.CreateMap<UpdatePlayerRequest, UpdatePlayerCommand>()
                                                   .IgnoreAllNonExisting();
}
