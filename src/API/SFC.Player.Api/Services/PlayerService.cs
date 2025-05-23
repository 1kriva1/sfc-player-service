using AutoMapper;

using Grpc.Core;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using SFC.Player.Application.Features.Player.Queries.Find;
using SFC.Player.Application.Features.Player.Queries.Get;
using SFC.Player.Infrastructure.Constants;

using SFC.Player.Api.Infrastructure.Extensions;
using SFC.Player.Contracts.Headers;

using static SFC.Player.Contracts.Services.PlayerService;
using SFC.Player.Contracts.Messages.Player.General.Get;
using SFC.Player.Contracts.Messages.Player.General.Find;

namespace SFC.Player.Api.Services;

[Authorize(Policy.General)]
public class PlayerService(IMapper mapper, ISender mediator) : PlayerServiceBase
{
    public override async Task<GetPlayerResponse> GetPlayer(GetPlayerRequest request, ServerCallContext context)
    {
        GetPlayerQuery query = mapper.Map<GetPlayerQuery>(request);

        GetPlayerViewModel model = await mediator.Send(query).ConfigureAwait(true);

        context.AddAuditableHeaderIfRequested(mapper.Map<AuditableHeader>(model.Player));

        return mapper.Map<GetPlayerResponse>(model);
    }

    public override async Task<GetPlayersResponse> GetPlayers(GetPlayersRequest request, ServerCallContext context)
    {
        GetPlayersQuery query = mapper.Map<GetPlayersQuery>(request);

        GetPlayersViewModel result = await mediator.Send(query).ConfigureAwait(true);

        context.AddPaginationHeader(mapper.Map<PaginationHeader>(result.Metadata));

        return mapper.Map<GetPlayersResponse>(result);
    }
}