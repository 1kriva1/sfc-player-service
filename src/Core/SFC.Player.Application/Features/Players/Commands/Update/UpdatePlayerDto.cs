using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Players.Common.Dto;
using SFC.Player.Application.Models.Players.Update;

namespace SFC.Player.Application.Features.Players.Commands.Update;
public class UpdatePlayerDto : BasePlayerDto, IMapFrom<UpdatePlayerModel> { }
