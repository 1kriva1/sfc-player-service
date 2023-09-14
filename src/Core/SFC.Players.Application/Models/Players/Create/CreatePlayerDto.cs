using SFC.Players.Application.Common.Mappings;
using SFC.Players.Application.Models.Players.Common;

namespace SFC.Players.Application.Models.Players.Create;

public class CreatePlayerDto : BasePlayerDto, IMapFrom<CreatePlayerModel> { }
