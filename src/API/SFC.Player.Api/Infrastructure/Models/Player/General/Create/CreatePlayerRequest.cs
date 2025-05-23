using AutoMapper;

using SFC.Player.Application.Common.Mappings.Interfaces;
using SFC.Player.Application.Features.Player.Commands.Create;
using SFC.Player.Application.Common.Extensions;

namespace SFC.Player.Api.Infrastructure.Models.Player.General.Create;

/// <summary>
/// **Create** player request.
/// </summary>
public class CreatePlayerRequest : IMapTo<CreatePlayerCommand>
{
    /// <summary>
    /// Player model.
    /// </summary>
    public CreatePlayerModel Player { get; set; } = null!;

    public void Mapping(Profile profile) => profile.CreateMap<CreatePlayerRequest, CreatePlayerCommand>()
                                                   .IgnoreAllNonExisting();
}
