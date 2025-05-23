using System.Reflection;

using SFC.Player.Application.Common.Mappings.Base;
using SFC.Player.Application.Features.Common.Dto.Pagination;
using SFC.Player.Application.Features.Common.Models.Find.Paging;
using SFC.Player.Domain.Entities.Data;
using SFC.Player.Domain.Entities.Player;

namespace SFC.Player.Application.Common.Mappings;

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

        CreateMap<DayOfWeek, PlayerAvailableDay>()
            .ConvertUsing(day => new PlayerAvailableDay { Day = day });

        CreateMap<PlayerAvailableDay, DayOfWeek>()
            .ConvertUsing(day => day.Day);

        CreateMap<string, PlayerTag>()
            .ConvertUsing(tag => new PlayerTag { Value = tag });

        CreateMap<PlayerTag, string>()
            .ConvertUsing(tag => tag.Value);

        CreateMap<StatType, int>()
            .ConvertUsing(statType => (int)statType.Id);

        CreateMap<int, StatType>()
            .ConvertUsing(statType => new StatType { Id = (StatTypeEnum)statType });

        #endregion Simple types

        #region Generic types        

        CreateMap(typeof(PagedList<>), typeof(PageDto<>))
            .ForMember(nameof(PageDto<object>.Items), d => d.Ignore())
            .ForMember(nameof(PageDto<object>.Metadata), d => d.Ignore());

        CreateMap(typeof(PagedList<>), typeof(PageMetadataDto));

        #endregion Generic types
    }
}