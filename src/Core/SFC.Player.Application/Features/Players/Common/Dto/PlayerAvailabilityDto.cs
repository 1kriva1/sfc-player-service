using AutoMapper;

using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Player.Common;
using SFC.Player.Domain.Entities;

namespace SFC.Player.Application.Features.Player.Common.Dto;

public record PlayerAvailabilityDto : IMapFromReverse<PlayerAvailability>
{
    public TimeSpan? From { get; set; }

    public TimeSpan? To { get; set; }

    public IEnumerable<DayOfWeek> Days { get; set; } = new List<DayOfWeek>();

    public void Mapping(Profile profile)
    {
        profile.CreateMap<PlayerAvailability, PlayerAvailabilityDto>();
        profile.CreateMap<PlayerAvailabilityModel, PlayerAvailabilityDto>();
    }
}
