using System.Linq.Expressions;
using SFC.Player.Application.Features.Common.Models.Find.Sorting;
using SFC.Player.Application.Features.Common.Dto.Common;
using SFC.Player.Application.Features.Common.Extensions;
using SFC.Player.Application.Features.Player.Queries.Find.Dto.Filters;
using SFC.Player.Application.Features.Player.Common.Dto;
using SFC.Player.Application.Common.Enums;

namespace SFC.Player.Application.Features.Player.Queries.Find.Extensions;
public static class GetPlayersSortingExtensions
{
    public static IEnumerable<Sorting<PlayerEntity, dynamic>> BuildPlayerSearchSorting(this IEnumerable<SortingDto> sorting)
    {
        IEnumerable<Sorting<PlayerEntity, dynamic>> result = sorting
            .BuildSearchSorting<PlayerEntity>(BuildExpression)
            .AddDefaultSorting();

        return result;
    }

    private static Expression<Func<PlayerEntity, dynamic>>? BuildExpression(string name)
    {
        return name switch
        {
            nameof(PlayerGeneralProfileDto.FirstName) => p => p.GeneralProfile.FirstName,
            nameof(PlayerGeneralProfileDto.LastName) => p => p.GeneralProfile.LastName,
            nameof(PlayerFootballProfileDto.PhysicalCondition) => p => p.FootballProfile.PhysicalCondition!,
            nameof(PlayerFootballProfileDto.Height) => p => p.FootballProfile.Height!,
            nameof(PlayerFootballProfileDto.Weight) => p => p.FootballProfile.Weight!,
            nameof(GetPlayersStatsFilterDto.Raiting) => p => p.Stats.Sum(m => m.Value),
            _ => null
        };
    }

    private static IEnumerable<Sorting<PlayerEntity, dynamic>> AddDefaultSorting(this IEnumerable<Sorting<PlayerEntity, dynamic>> sortings)
    {
        IEnumerable<Sorting<PlayerEntity, dynamic>> defaultSorting = [
            new Sorting<PlayerEntity, dynamic>{
                Condition = true,
                Direction = SortingDirection.Ascending,
                Expression = player => player.Id
            }
        ];

        return sortings.Concat(defaultSorting);
    }
}
