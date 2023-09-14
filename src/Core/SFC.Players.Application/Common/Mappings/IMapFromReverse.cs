using AutoMapper;

namespace SFC.Players.Application.Common.Mappings;
public interface IMapFromReverse<T>: IMapFrom<T>
{
    new void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType()).ReverseMap();
}
