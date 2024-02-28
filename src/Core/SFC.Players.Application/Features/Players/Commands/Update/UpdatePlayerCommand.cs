using AutoMapper;

using SFC.Players.Application.Common.Enums;
using SFC.Players.Application.Common.Extensions;
using SFC.Players.Application.Common.Mappings;
using SFC.Players.Application.Features.Common.Base;
using SFC.Players.Application.Features.Players.Common.Models;
using SFC.Players.Application.Models.Players.Update;

namespace SFC.Players.Application.Features.Players.Commands.Update;

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
