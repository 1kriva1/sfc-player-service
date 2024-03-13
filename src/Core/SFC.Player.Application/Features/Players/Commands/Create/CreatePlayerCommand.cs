using AutoMapper;

using SFC.Player.Application.Common.Enums;
using SFC.Player.Application.Common.Extensions;
using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Common.Base;
using SFC.Player.Application.Features.Player.Create;

namespace SFC.Player.Application.Features.Player.Commands.Create;
public class CreatePlayerCommand :
    Request<CreatePlayerViewModel>, IMapFrom<CreatePlayerRequest>
{
    public override RequestId RequestId { get => RequestId.CreatePlayer; }

    public CreatePlayerDto Player { get; set; } = null!;

    public void Mapping(Profile profile) => profile.CreateMap<CreatePlayerRequest, CreatePlayerCommand>()
                                                   .IgnoreAllNonExisting();
}
