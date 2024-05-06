using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Players.Common.Dto;
using SFC.Player.Application.Models.Players.Create;

namespace SFC.Player.Application.Features.Players.Commands.Create;

public class CreatePlayerDto : BasePlayerDto, IMapFrom<CreatePlayerModel> { }
