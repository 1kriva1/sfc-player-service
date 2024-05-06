using SFC.Player.Application.Models.Base;
using SFC.Player.Application.Models.Players.Find.Filters;

namespace SFC.Player.Application.Models.Players.Find;

/// <summary>
/// **Get** players request.
/// </summary>
public class GetPlayersRequest : BasePaginationRequest<GetPlayersFilterModel> { }
