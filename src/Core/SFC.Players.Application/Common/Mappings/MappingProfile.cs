using System.Reflection;

using AutoMapper;

using SFC.Players.Application.Common.Extensions;
using SFC.Players.Application.Common.Mappings.Converters;
using SFC.Players.Application.Models.Players.Common;
using SFC.Players.Application.Models.Players.Create;
using SFC.Players.Application.Models.Players.Update;
using SFC.Players.Domain.Entities;


namespace SFC.Players.Application.Common.Mappings;
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

        CreateMap<string?, PlayerPhotoDto?>()
            .ConvertUsing<PlayerFileTypeConverter>();

        CreateMap<PlayerPhoto?, string?>()
            .ConvertUsing<Base64StringTypeConverter>();

        #endregion Simple types

        #region Complex types

        CreateMap<PlayerGeneralProfileDto, PlayerGeneralProfile>()
            .IgnoreAllNonExisting();

        CreateMap<PlayerPhotoDto, PlayerPhoto>()
            .IgnoreAllNonExisting();

        CreateMap<BasePlayerDto, Player>()
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

        CreateMap<CreatePlayerDto, Player>()
            .IncludeBase<BasePlayerDto, Player>();

        CreateMap<UpdatePlayerDto, Player>()
            .IncludeBase<BasePlayerDto, Player>();

        #endregion Complex types
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
