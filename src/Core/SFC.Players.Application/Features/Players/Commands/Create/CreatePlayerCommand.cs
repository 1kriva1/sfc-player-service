using AutoMapper;

using SFC.Players.Application.Common.Enums;
using SFC.Players.Application.Common.Extensions;
using SFC.Players.Application.Common.Mappings;
using SFC.Players.Application.Features.Common.Base;
using SFC.Players.Application.Models.Players.Create;

namespace SFC.Players.Application.Features.Players.Commands.Create;
public class CreatePlayerCommand :
    Request<CreatePlayerViewModel>, IMapFrom<CreatePlayerRequest>
{
    public override RequestId RequestId { get => RequestId.CreatePlayer; }

    public CreatePlayerDto Player { get; set; } = null!;

    public void Mapping(Profile profile) => profile.CreateMap<CreatePlayerRequest, CreatePlayerCommand>()
                                                   .IgnoreAllNonExisting();
}
