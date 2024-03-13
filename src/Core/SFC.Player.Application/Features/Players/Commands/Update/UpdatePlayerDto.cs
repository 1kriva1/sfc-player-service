using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Player.Common.Dto;
using SFC.Player.Application.Features.Player.Update;

namespace SFC.Player.Application.Features.Player.Commands.Update;
public class UpdatePlayerDto : BasePlayerDto, IMapFrom<UpdatePlayerModel> { }
