using SFC.Players.Application.Common.Mappings;
using SFC.Players.Application.Features.Players.Common.Dto;
using SFC.Players.Application.Models.Players.Create;

namespace SFC.Players.Application.Features.Players.Commands.Create;

public class CreatePlayerDto : BasePlayerDto, IMapFrom<CreatePlayerModel> { }
