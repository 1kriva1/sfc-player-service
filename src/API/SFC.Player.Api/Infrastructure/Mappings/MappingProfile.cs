using Google.Protobuf.WellKnownTypes;

using SFC.Player.Api.Infrastructure.Models.Common;
using SFC.Player.Application.Common.Extensions;
using SFC.Player.Application.Common.Mappings.Base;
using SFC.Player.Application.Features.Common.Dto.Common;
using SFC.Player.Application.Features.Common.Dto.Pagination;
using SFC.Player.Application.Features.Player.Common.Dto;
using SFC.Player.Application.Features.Player.Queries.Find;
using SFC.Player.Application.Features.Player.Queries.Find.Dto.Filters;
using SFC.Player.Application.Features.Player.Queries.Get;

using System.Reflection;

namespace SFC.Player.Api.Infrastructure.Mappings;

public class MappingProfile : BaseMappingProfile
{
    protected override Assembly Assembly => Assembly.GetExecutingAssembly();

    public MappingProfile() : base()
    {
        ApplyCustomMappings();
    }

    private void ApplyCustomMappings()
    {
        #region Simple types

        CreateMap<DateTime, Timestamp>()
            .ConvertUsing(value => DateTime.SpecifyKind(value, DateTimeKind.Utc).ToTimestamp());

        CreateMap<TimeSpan, Duration>()
            .ConvertUsing(value => Duration.FromTimeSpan(value));

        CreateMap<Duration, TimeSpan>()
            .ConvertUsing(value => value.ToTimeSpan());

        #endregion Simple types

        #region Generic types

        CreateMap(typeof(RangeLimitModel<>), typeof(RangeLimitDto<>));

        #endregion Generic types

        #region Complex types

        // contracts
        CreateMapPlayerContracts();

        #endregion Complex types
    }

    private void CreateMapPlayerContracts()
    {
        // get player
        CreateMap<PlayerDto, SFC.Player.Contracts.Models.Player.General.Player>();
        CreateMap<PlayerProfileDto, SFC.Player.Contracts.Models.Player.General.PlayerProfile>();
        CreateMap<PlayerGeneralProfileDto, SFC.Player.Contracts.Models.Player.General.PlayerGeneralProfile>();
        CreateMap<PlayerAvailabilityDto, SFC.Player.Contracts.Models.Player.General.PlayerAvailability>();
        CreateMap<PlayerFootballProfileDto, SFC.Player.Contracts.Models.Player.General.PlayerFootballProfile>();
        CreateMap<PlayerStatsDto, SFC.Player.Contracts.Models.Player.General.PlayerStats>();
        CreateMap<PlayerStatPointsDto, SFC.Player.Contracts.Models.Player.General.PlayerStatPoints>();
        CreateMap<PlayerStatValueDto, SFC.Player.Contracts.Models.Player.General.PlayerStatValue>();
        CreateMap<GetPlayerViewModel, SFC.Player.Contracts.Messages.Player.General.Get.GetPlayerResponse>();
        CreateMap<SFC.Player.Contracts.Messages.Player.General.Get.GetPlayerRequest, GetPlayerQuery>()
             .ForMember(p => p.PlayerId, d => d.MapFrom(z => z.Id));
        CreateMap<PlayerDto, SFC.Player.Contracts.Headers.AuditableHeader>()
            .IgnoreAllNonExisting();

        // get players
        // (filters)
        CreateMap<SFC.Player.Contracts.Messages.Player.General.Find.GetPlayersRequest, GetPlayersQuery>();
        CreateMap<SFC.Player.Contracts.Models.Common.Pagination, PaginationDto>();
        CreateMap<SFC.Player.Contracts.Models.Common.Sorting, SortingDto>();
        CreateMap<SFC.Player.Contracts.Models.Player.General.Find.Filters.GetPlayersFilter, GetPlayersFilterDto>();
        CreateMap<SFC.Player.Contracts.Models.Player.General.Find.Filters.GetPlayersProfileFilter, GetPlayersProfileFilterDto>();
        CreateMap<SFC.Player.Contracts.Models.Player.General.Find.Filters.GetPlayersGeneralProfileFilter, GetPlayersGeneralProfileFilterDto>();
        CreateMap<SFC.Player.Contracts.Models.Player.General.Find.Filters.GetPlayersAvailabilityLimit, GetPlayersAvailabilityLimitDto>();       
        CreateMap<SFC.Player.Contracts.Models.Player.General.Find.Filters.GetPlayersFootballProfileFilter, GetPlayersFootballProfileFilterDto>();
        CreateMap<SFC.Player.Contracts.Models.Player.General.Find.Filters.GetPlayersStatsFilter, GetPlayersStatsFilterDto>();
        CreateMap<SFC.Player.Contracts.Models.Player.General.Find.Filters.GetPlayersStatsBySkillRangeLimit, GetPlayersStatsBySkillRangeLimitDto>();
        CreateMap(typeof(SFC.Player.Contracts.Models.Common.RangeLimit), typeof(RangeLimitDto<>));
        // (result)
        CreateMap<GetPlayersViewModel, SFC.Player.Contracts.Messages.Player.General.Find.GetPlayersResponse>();
        // (headers)
        CreateMap<PageMetadataDto, SFC.Player.Contracts.Headers.PaginationHeader>()
            .IgnoreAllNonExisting();        
    }
}
