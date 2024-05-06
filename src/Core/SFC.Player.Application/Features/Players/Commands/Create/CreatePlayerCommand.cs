using AutoMapper;

using SFC.Player.Application.Common.Enums;
using SFC.Player.Application.Common.Extensions;
using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Common.Base;
using SFC.Player.Application.Models.Players.Create;

namespace SFC.Player.Application.Features.Players.Commands.Create;
public class CreatePlayerCommand :
    Request<CreatePlayerViewModel>, IMapFrom<CreatePlayerRequest>
{
    public override RequestId RequestId { get => RequestId.CreatePlayer; }

    public CreatePlayerDto Player { get; set; } = null!;

    public void Mapping(Profile profile) => profile.CreateMap<CreatePlayerRequest, CreatePlayerCommand>()
                                                   .IgnoreAllNonExisting();
}
