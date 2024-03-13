using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Player.Common.Dto;
using SFC.Player.Application.Features.Player.Create;

namespace SFC.Player.Application.Features.Player.Commands.Create;

public class CreatePlayerDto : BasePlayerDto, IMapFrom<CreatePlayerModel> { }
