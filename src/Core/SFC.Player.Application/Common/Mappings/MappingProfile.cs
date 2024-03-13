using System.Reflection;

using AutoMapper;

using SFC.Player.Application.Common.Extensions;
using SFC.Player.Application.Features.Common.Dto;
using SFC.Player.Application.Features.Common.Dto.Pagination;
using SFC.Player.Application.Features.Common.Models.Paging;
using SFC.Player.Application.Features.Player.Commands.Create;
using SFC.Player.Application.Features.Player.Commands.Update;
using SFC.Player.Application.Features.Player.Common.Dto;
using SFC.Player.Application.Models.Common;
using SFC.Player.Domain.Entities;
using SFC.Player.Domain.Entities.Data;

using PlayerEntity = SFC.Player.Domain.Entities.Player;

namespace SFC.Player.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        Configure();

        ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());

        ApplyCustomMappings();
    }

    private void Configure()
    {
        AllowNullCollections = true;
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
            .ConvertUsing(statType => statType.Id);

        CreateMap<int, StatType>()
            .ConvertUsing(statType => new StatType { Id = statType });

        #endregion Simple types

        #region Complex types

        CreateMap<PlayerGeneralProfileDto, PlayerGeneralProfile>()
            .IgnoreAllNonExisting();

        CreateMap<PlayerAvailabilityDto, PlayerAvailability>()
            .IgnoreAllNonExisting();

        CreateMap<PlayerFootballProfileDto, PlayerFootballProfile>()
            .ForMember(p => p.PositionId, d => d.MapFrom(z => z.Position))
            .ForMember(p => p.AdditionalPositionId, d => d.MapFrom(z => z.AdditionalPosition))
            .ForMember(p => p.WorkingFootId, d => d.MapFrom(z => z.WorkingFoot))
            .ForMember(p => p.GameStyleId, d => d.MapFrom(z => z.GameStyle))
            .ForMember(p => p.DomainEvents, d => d.Ignore())
            .ForMember(p => p.Id, d => d.Ignore())
            .ForMember(p => p.Player, d => d.Ignore());

        CreateMap<PlayerPhotoDto, PlayerPhoto>()
            .IgnoreAllNonExisting();

        CreateMap<BasePlayerDto, PlayerEntity>()
            .ForMember(p => p.GeneralProfile, d => d.MapFrom(z => z.Profile.General))
            .ForMember(p => p.FootballProfile, d => d.MapFrom(z => z.Profile.Football))
            .ForMember(p => p.Availability, d => d.MapFrom(z => z.Profile.General.Availability))
            .ForMember(p => p.Points, d => d.MapFrom(z => z.Stats.Points))
            .ForMember(p => p.Photo, d => d.MapFrom(z => z.Profile.General.Photo))
            .ForMember(p => p.Tags, d => d.MapFrom(z => z.Profile.General.Tags))
            .ForMember(p => p.Stats, d => d.MapFrom(z => z.Stats.Values))
            .ForMember(p => p.User, d => d.Ignore())
            .ForMember(p => p.CreatedDate, d => d.Ignore())
            .ForMember(p => p.CreatedBy, d => d.Ignore())
            .ForMember(p => p.LastModifiedDate, d => d.Ignore())
            .ForMember(p => p.LastModifiedBy, d => d.Ignore())
            .ForMember(p => p.Id, d => d.Ignore())
            .ForMember(p => p.DomainEvents, d => d.Ignore());

        CreateMap<PlayerStatPointsDto, PlayerStatPoints>()
            .IgnoreAllNonExisting();

        CreateMap<CreatePlayerDto, PlayerEntity>()
            .IncludeBase<BasePlayerDto, PlayerEntity>();

        CreateMap<UpdatePlayerDto, PlayerEntity>()
            .IncludeBase<BasePlayerDto, PlayerEntity>();

        CreateMap<PlayerStatValueDto, PlayerStat>()
            .ForMember(p => p.DomainEvents, d => d.Ignore())
            .ForMember(p => p.Id, d => d.Ignore())
            .ForMember(p => p.Player, d => d.Ignore());

        #endregion Complex types

        #region Generic types

        CreateMap(typeof(RangeLimitModel<>), typeof(RangeLimitDto<>));

        CreateMap(typeof(PagedList<>), typeof(PageDto<>))
            .ForMember(nameof(PageDto<object>.Items), d => d.Ignore())
            .ForMember(nameof(PageDto<object>.Metadata), d => d.Ignore());

        CreateMap(typeof(PagedList<>), typeof(PageMetadataDto))
            .ForMember(nameof(PageMetadataDto.Links), d => d.Ignore());

        #endregion Generic types
    }

    private void ApplyMappingsFromAssembly(Assembly assembly)
    {
        string mappingMethodName = nameof(IMapFrom<object>.Mapping);

        static bool HasInterface(Type t) => t.IsGenericType &&
            (t.GetGenericTypeDefinition() == typeof(IMapFrom<>) || t.GetGenericTypeDefinition() == typeof(IMapFromReverse<>));

        List<Type> types = assembly.GetExportedTypes()
                                   .Where(t => t.GetInterfaces().Any(HasInterface) && !t.IsInterface)
                                   .ToList();

        Type[] argumentTypes = new Type[] { typeof(Profile) };

        foreach (Type type in types)
        {
            object? instance = Activator.CreateInstance(type);

            MethodInfo? methodInfo = type.GetMethod(mappingMethodName);

            if (methodInfo != null)
            {
                methodInfo.Invoke(instance, new object[] { this });
            }
            else
            {
                List<Type> interfaces = type.GetInterfaces().Where(HasInterface).ToList();

                if (interfaces.Count > 0)
                {
                    foreach (Type @interface in interfaces)
                    {
                        MethodInfo? interfaceMethodInfo = @interface.GetMethod(mappingMethodName, argumentTypes);

                        interfaceMethodInfo?.Invoke(instance, new object[] { this });
                    }
                }
            }
        }
    }
}
