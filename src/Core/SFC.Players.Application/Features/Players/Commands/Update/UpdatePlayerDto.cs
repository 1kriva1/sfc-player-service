using SFC.Players.Application.Common.Mappings;
using SFC.Players.Application.Features.Players.Common.Dto;
using SFC.Players.Application.Models.Players.Update;

namespace SFC.Players.Application.Features.Players.Commands.Update;
public class UpdatePlayerDto : BasePlayerDto, IMapFrom<UpdatePlayerModel> { }
