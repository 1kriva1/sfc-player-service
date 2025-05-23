using AutoMapper;

using SFC.Player.Application.Common.Mappings.Interfaces;
using SFC.Player.Application.Features.Player.Common.Dto;

namespace SFC.Player.Application.Features.Player.Commands.Update;
public class UpdatePlayerDto : BasePlayerDto, IMapTo<PlayerEntity>
{
    public new void Mapping(Profile profile)
    {
        profile.CreateMap<UpdatePlayerDto, PlayerEntity>()
               .IncludeBase<BasePlayerDto, PlayerEntity>();
    }
}
