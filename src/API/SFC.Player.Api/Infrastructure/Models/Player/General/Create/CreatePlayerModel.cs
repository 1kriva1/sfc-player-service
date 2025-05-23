using SFC.Player.Api.Infrastructure.Models.Player.General.Common;
using SFC.Player.Application.Common.Mappings.Interfaces;
using SFC.Player.Application.Features.Player.Commands.Create;

namespace SFC.Player.Api.Infrastructure.Models.Player.General.Create;

/// <summary>
/// **Create** player model.
/// </summary>
public class CreatePlayerModel : BasePlayerModel, IMapTo<CreatePlayerDto> { }
