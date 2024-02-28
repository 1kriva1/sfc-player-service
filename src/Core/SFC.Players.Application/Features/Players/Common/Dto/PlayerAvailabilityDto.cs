using AutoMapper;

using SFC.Players.Application.Common.Mappings;
using SFC.Players.Application.Models.Players.Common;
using SFC.Players.Domain.Entities;

namespace SFC.Players.Application.Features.Players.Common.Dto;

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
