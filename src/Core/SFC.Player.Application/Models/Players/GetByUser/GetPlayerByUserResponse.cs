using AutoMapper;

using SFC.Player.Application.Common.Extensions;
using SFC.Player.Application.Common.Mappings;
using SFC.Player.Application.Features.Player.Queries.Get;
using SFC.Player.Application.Models.Base;
using SFC.Player.Application.Features.Player.GetByUser.Result;

namespace SFC.Player.Application.Features.Player.Get;

public class GetPlayerByUserResponse :
    BaseErrorResponse, IMapFrom<GetPlayerByUserViewModel>
{
    public static GetPlayerByUserResponse SuccessResult => new();

    public PlayerByUserModel? Player { get; set; } = null!;

    public void Mapping(Profile profile) => profile.CreateMap<GetPlayerByUserViewModel, GetPlayerByUserResponse>()
                                                   .IgnoreAllNonExisting();
}
