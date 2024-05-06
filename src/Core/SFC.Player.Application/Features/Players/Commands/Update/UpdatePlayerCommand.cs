using AutoMapper;

using SFC.Player.Application.Common.Enums;
using SFC.Player.Application.Common.Extensions;
using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Common.Base;
using SFC.Player.Application.Features.Players.Common.Models;
using SFC.Player.Application.Models.Players.Update;

namespace SFC.Player.Application.Features.Players.Commands.Update;

public class UpdatePlayerCommand : Request,
    IPlayerRelatedRequest,
    IMapFrom<UpdatePlayerRequest>
{
    public override RequestId RequestId { get => RequestId.UpdatePlayer; }

    public long PlayerId { get; set; }

    public UpdatePlayerDto Player { get; set; } = null!;

    public UpdatePlayerCommand SetPlayerId(long id)
    {
        PlayerId = id;
        return this;
    }

    public void Mapping(Profile profile) => profile.CreateMap<UpdatePlayerRequest, UpdatePlayerCommand>()
                                                   .IgnoreAllNonExisting();
}
