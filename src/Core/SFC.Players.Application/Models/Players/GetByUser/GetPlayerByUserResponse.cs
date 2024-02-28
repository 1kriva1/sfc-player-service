using AutoMapper;

using SFC.Players.Application.Common.Extensions;
using SFC.Players.Application.Common.Mappings;
using SFC.Players.Application.Features.Players.Queries.Get;
using SFC.Players.Application.Models.Base;
using SFC.Players.Application.Models.Players.GetByUser.Result;

namespace SFC.Players.Application.Models.Players.Get;

public class GetPlayerByUserResponse :
    BaseErrorResponse, IMapFrom<GetPlayerByUserViewModel>
{
    public static GetPlayerByUserResponse SuccessResult => new();

    public PlayerByUserModel? Player { get; set; } = null!;

    public void Mapping(Profile profile) => profile.CreateMap<GetPlayerByUserViewModel, GetPlayerByUserResponse>()
                                                   .IgnoreAllNonExisting();
}
