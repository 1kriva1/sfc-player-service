using AutoMapper;

using SFC.Player.Application.Common.Extensions;
using SFC.Player.Application.Common.Mappings.Interfaces;
using SFC.Player.Domain.Entities.Player;

namespace SFC.Player.Application.Features.Player.Common.Dto;

public record PlayerAvailabilityDto : IMapFromReverse<PlayerAvailability>
{
    public TimeSpan? From { get; set; }

    public TimeSpan? To { get; set; }

    public IEnumerable<DayOfWeek> Days { get; set; } = [];

    public void Mapping(Profile profile)
    {
        profile.CreateMap<PlayerAvailabilityDto, PlayerAvailability>()
            .ReverseMap()
            .IgnoreAllNonExisting();
    }
}
