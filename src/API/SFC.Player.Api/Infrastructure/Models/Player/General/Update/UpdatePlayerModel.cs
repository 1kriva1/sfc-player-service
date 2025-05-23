using SFC.Player.Api.Infrastructure.Models.Player.General.Common;
using SFC.Player.Application.Common.Mappings.Interfaces;
using SFC.Player.Application.Features.Player.Commands.Update;

namespace SFC.Player.Api.Infrastructure.Models.Player.General.Update;

/// <summary>
/// **Update** player model.
/// </summary>
public class UpdatePlayerModel : BasePlayerModel, IMapTo<UpdatePlayerDto> { }
