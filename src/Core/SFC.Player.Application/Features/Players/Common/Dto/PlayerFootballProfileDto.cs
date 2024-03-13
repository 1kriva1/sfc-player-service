using AutoMapper;

using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Player.Common;
using SFC.Player.Domain.Entities;

namespace SFC.Player.Application.Features.Player.Common.Dto;

public class PlayerFootballProfileDto : IMapFrom<PlayerFootballProfile>
{
    public int? Height { get; set; }

    public int? Weight { get; set; }

    public int? Position { get; set; }

    public int? AdditionalPosition { get; set; }

    public int? WorkingFoot { get; set; }

    public int? Number { get; set; }

    public int? GameStyle { get; set; }

    public int? Skill { get; set; }

    public int? WeakFoot { get; set; }

    public int? PhysicalCondition { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<PlayerFootballProfile, PlayerFootballProfileDto>()
                                                       .ForMember(p => p.Position, d => d.MapFrom(z => z.PositionId))
                                                       .ForMember(p => p.AdditionalPosition, d => d.MapFrom(z => z.AdditionalPositionId))
                                                       .ForMember(p => p.WorkingFoot, d => d.MapFrom(z => z.WorkingFootId))
                                                       .ForMember(p => p.GameStyle, d => d.MapFrom(z => z.GameStyleId));

        profile.CreateMap<PlayerFootballProfileModel, PlayerFootballProfileDto>();
    }
}
