using System.Reflection;

using SFC.Data.Messages.Events;
using SFC.Player.Application.Common.Mappings.Base;
using SFC.Player.Application.Features.Data.Commands.Reset;
using SFC.Player.Application.Common.Extensions;
using SFC.Player.Application.Features.Data.Common.Dto;
using SFC.Player.Messages.Commands.Data;
using SFC.Identity.Messages.Events;
using SFC.Player.Application.Features.Identity.Commands.Create;
using SFC.Player.Application.Features.Identity.Commands.CreateRange;
using SFC.Player.Domain.Entities.Player;
using SFC.Player.Application.Common.Dto.Identity;
using SFC.Player.Messages.Models.Data;
using SFC.Player.Messages.Events.Player.General;

namespace SFC.Player.Infrastructure.Mappings;
public class MappingProfile : BaseMappingProfile
{
    protected override Assembly Assembly => Assembly.GetExecutingAssembly();

    public MappingProfile() : base()
    {
        ApplyCustomMappings();
    }

    private void ApplyCustomMappings()
    {
        #region Data

        // data messages        
        CreateMapDataMessages();

        #endregion Data

        #region Identity

        // identity messages        
        CreateMapIdentityMessages();

        // identity contracts        
        CreateMapIdentityContracts();

        #endregion Identity

        #region Player

        // player messages        
        CreateMapPlayerMessages();

        #endregion Player
    }

    #region Data

    private void CreateMapDataMessages()
    {
        CreateMap<SFC.Data.Messages.Events.Data.DataInitialized, ResetDataCommand>().IgnoreAllNonExisting();
        CreateMap<SFC.Data.Messages.Models.Data.DataValue, FootballPositionDto>();
        CreateMap<SFC.Data.Messages.Models.Data.DataValue, GameStyleDto>();
        CreateMap<SFC.Data.Messages.Models.Data.DataValue, StatCategoryDto>();
        CreateMap<SFC.Data.Messages.Models.Data.DataValue, StatSkillDto>();
        CreateMap<SFC.Data.Messages.Models.Data.StatTypeDataValue, StatTypeDto>();
        CreateMap<SFC.Data.Messages.Models.Data.DataValue, WorkingFootDto>();
    }

    #endregion Data

    #region Identity

    private void CreateMapIdentityMessages()
    {
        CreateMap<IEnumerable<SFC.Identity.Messages.Models.User.User>, CreateUsersCommand>()
            .ForMember(p => p.Users, d => d.MapFrom(z => z));

        CreateMap<SFC.Identity.Messages.Events.User.UserCreated, CreateUserCommand>().IgnoreAllNonExisting();

        CreateMap<SFC.Identity.Messages.Models.User.User, UserDto>();
    }

    private void CreateMapIdentityContracts()
    {
        CreateMap<Guid, SFC.Identity.Contracts.Messages.User.Get.GetUserRequest>()
            .ConvertUsing(id => new SFC.Identity.Contracts.Messages.User.Get.GetUserRequest { Id = id.ToString() });
        CreateMap<SFC.Identity.Contracts.Models.User.User, UserDto>();
    }

    #endregion Identity

    #region Player

    private void CreateMapPlayerMessages()
    {
        CreateMap<IEnumerable<PlayerEntity>, PlayersSeeded>()
            .ForMember(p => p.Players, d => d.MapFrom(z => z));

        CreateMap<IEnumerable<PlayerEntity>, SFC.Player.Messages.Commands.Player.SeedPlayers>()
            .ForMember(p => p.Players, d => d.MapFrom(z => z));

        // data
        CreateMap<InitializeData, ResetDataCommand>().IgnoreAllNonExisting();
        CreateMap<DataValue, FootballPositionDto>();
        CreateMap<DataValue, GameStyleDto>();
        CreateMap<DataValue, StatCategoryDto>();
        CreateMap<DataValue, StatSkillDto>();
        CreateMap<StatTypeDataValue, StatTypeDto>();
        CreateMap<DataValue, WorkingFootDto>();

        // player
        CreateMap<PlayerEntity, PlayerCreated>()
            .ForMember(p => p.Player, d => d.MapFrom(z => z));
        CreateMap<PlayerEntity, PlayerUpdated>()
            .ForMember(p => p.Player, d => d.MapFrom(z => z));
        CreateMap<PlayerEntity, SFC.Player.Messages.Models.Player.Player>();
        CreateMap<PlayerGeneralProfile, SFC.Player.Messages.Models.Player.PlayerGeneralProfile>();
        CreateMap<PlayerFootballProfile, SFC.Player.Messages.Models.Player.PlayerFootballProfile>();
        CreateMap<PlayerAvailability, SFC.Player.Messages.Models.Player.PlayerAvailability>();
        CreateMap<PlayerAvailableDay, SFC.Player.Messages.Models.Player.PlayerAvailableDay>();
        CreateMap<PlayerStatPoints, SFC.Player.Messages.Models.Player.PlayerStatPoints>();
        CreateMap<PlayerPhoto, SFC.Player.Messages.Models.Player.PlayerPhoto>();
        CreateMap<PlayerTag, SFC.Player.Messages.Models.Player.PlayerTag>();
        CreateMap<PlayerStat, SFC.Player.Messages.Models.Player.PlayerStat>();
    }

    #endregion Player    
}
