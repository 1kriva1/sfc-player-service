using AutoMapper;

using SFC.Player.Application.Common.Mappings.Interfaces;
using SFC.Player.Application.Features.Player.Common.Dto;

namespace SFC.Player.Application.Features.Player.Commands.Create;

public class CreatePlayerDto : BasePlayerDto, IMapTo<PlayerEntity>
{
    public new void Mapping(Profile profile)
    {
        profile.CreateMap<CreatePlayerDto, PlayerEntity>()
               .IncludeBase<BasePlayerDto, PlayerEntity>();
    }
}
