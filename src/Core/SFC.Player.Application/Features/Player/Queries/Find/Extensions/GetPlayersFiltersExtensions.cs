﻿using System.Linq.Expressions;

using SFC.Player.Application.Common.Constants;
using SFC.Player.Application.Features.Common.Constants;
using SFC.Player.Application.Features.Common.Dto.Common;
using SFC.Player.Application.Features.Common.Models.Find.Filters;
using SFC.Player.Application.Features.Player.Queries.Find.Dto.Filters;

namespace SFC.Player.Application.Features.Player.Queries.Find.Extensions;
public static class GetPlayersFiltersExtensions
{
    public static IEnumerable<Filter<PlayerEntity>> BuildSearchFilters(this GetPlayersFilterDto filter, DateTime now)
    {
        return [
            new() {
                Condition = !string.IsNullOrEmpty(filter?.Profile?.General?.Name),
                Expression = player=>player.GeneralProfile.FirstName.Contains(filter!.Profile.General!.Name!) || player.GeneralProfile.LastName.Contains(filter.Profile.General.Name!)
            },
            new() {
                Condition = !string.IsNullOrEmpty(filter?.Profile?.General?.City),
                Expression = player=>player.GeneralProfile.City.Contains(filter!.Profile.General!.City!)
            },
            new() {
                Condition = filter?.Profile?.General?.Tags?.Any() ?? false,
                Expression = player=>player.Tags.Any(tag=> filter!.Profile.General!.Tags.Contains(tag.Value))
            },
            new() {
                Condition = BuildLimitFromCondition(filter?.Profile?.General?.Years, ValidationConstants.RangeLimit),
                Expression = player => !player.GeneralProfile.Birthday.HasValue || player.GeneralProfile.Birthday <= now.AddYears(-filter!.Profile.General!.Years!.From!.Value)
            },
            new() {
                Condition = BuildLimitToCondition(filter?.Profile?.General?.Years, ValidationConstants.RangeLimit),
                Expression = player => !player.GeneralProfile.Birthday.HasValue || player.GeneralProfile.Birthday >= now.AddYears(-filter!.Profile.General!.Years!.To!.Value)
            },
            new() {
                Condition = (filter?.Profile?.General?.Availability?.From.HasValue ?? false)
                    && (filter.Profile.General?.Availability!.To == null || filter.Profile.General.Availability.From <= filter.Profile.General.Availability.To),
                Expression = player => !player.Availability.From.HasValue || player.Availability.From <= filter!.Profile.General!.Availability.From
            },
            new() {
                Condition = (filter?.Profile?.General?.Availability?.To.HasValue ?? false)
                    && (filter.Profile.General.Availability.From == null || filter.Profile.General.Availability.To >= filter.Profile.General.Availability.From),
                Expression = player => !player.Availability.To.HasValue || player.Availability.To >= filter!.Profile.General!.Availability.To
            },
            new() {
                Condition = filter?.Profile?.General?.Availability?.Days?.Any() ?? false,
                Expression = player=>player.Availability.Days.Count == 0 || player.Availability.Days.Any(day=> filter!.Profile.General!.Availability.Days.Contains(day.Day))
            },
            new() {
                Condition = filter?.Profile?.General?.FreePlay.HasValue ?? false,
                Expression = player=>player.GeneralProfile.FreePlay == filter!.Profile.General!.FreePlay
            },
            new() {
                Condition = filter?.Profile?.General?.HasPhoto.HasValue ?? false,
                Expression = player=>(filter!.Profile.General!.HasPhoto!.Value && player.Photo != null && player.Photo.Size > 0)
                    || (!filter.Profile.General!.HasPhoto!.Value && (player.Photo == null || player.Photo.Size <= 0))
            },
            new() {
                Condition = BuildLimitFromCondition(filter?.Profile?.Football?.Height, ValidationConstants.PlayerSizeRange),
                Expression = player=> !player.FootballProfile.Height.HasValue || player.FootballProfile.Height >= filter!.Profile.Football!.Height.From
            },
            new() {
                Condition = BuildLimitToCondition(filter?.Profile?.Football?.Height, ValidationConstants.PlayerSizeRange),
                Expression = player=>!player.FootballProfile.Height.HasValue || player.FootballProfile.Height <= filter!.Profile.Football!.Height.To
            },
            new() {
                Condition = BuildLimitFromCondition(filter?.Profile?.Football?.Weight, ValidationConstants.PlayerSizeRange),
                Expression = player=> !player.FootballProfile.Weight.HasValue || player.FootballProfile.Weight >= filter!.Profile.Football!.Weight.From
            },
            new() {
                Condition = BuildLimitToCondition(filter?.Profile?.Football?.Weight, ValidationConstants.PlayerSizeRange),
                Expression = player=>!player.FootballProfile.Weight.HasValue || player.FootballProfile.Weight <= filter!.Profile.Football!.Weight.To
            },
            new() {
                Condition = filter?.Profile?.Football?.Positions?.Any() ?? false,
                Expression = player=>!player.FootballProfile.PositionId.HasValue || filter!.Profile.Football!.Positions.Contains((int)player.FootballProfile.PositionId.Value)
            },
            new() {
                Condition = filter?.Profile?.Football?.WorkingFoot.HasValue ?? false,
                Expression = player=>!player.FootballProfile.WorkingFootId.HasValue || (int?)player.FootballProfile.WorkingFootId == filter!.Profile.Football!.WorkingFoot
            },
            new() {
                Condition = filter?.Profile?.Football?.GameStyles?.Any() ?? false,
                Expression = player=>!player.FootballProfile.GameStyleId.HasValue || filter!.Profile.Football!.GameStyles.Contains((int)player.FootballProfile.GameStyleId.Value)
            },
            new() {
                Condition = filter?.Profile?.Football?.Skill.HasValue ?? false,
                Expression = player=>!player.FootballProfile.Skill.HasValue || player.FootballProfile.Skill >= filter!.Profile.Football!.Skill
            },
            new() {
                Condition = filter?.Profile?.Football?.PhysicalCondition.HasValue ?? false,
                Expression = player=>!player.FootballProfile.PhysicalCondition.HasValue || player.FootballProfile.PhysicalCondition >= filter!.Profile.Football!.PhysicalCondition
            },
            new() {
                Condition = BuildLimitFromCondition(filter?.Stats?.Total, ValidationConstants.StatValueRange),
                Expression = FilterByStat(null, filter?.Stats?.Total?.From, null)
            },
            new() {
                Condition = BuildLimitToCondition(filter?.Stats?.Total, ValidationConstants.StatValueRange),
                Expression = FilterByStat(null, null, filter?.Stats?.Total?.To)
            },
            new() {
                Condition = BuildLimitFromCondition(filter?.Stats?.Physical, ValidationConstants.StatValueRange),
                Expression = FilterByStat(filter?.Stats?.Physical?.Skill, filter?.Stats?.Physical?.From, null)
            },
            new() {
                Condition = BuildLimitToCondition(filter?.Stats?.Physical, ValidationConstants.StatValueRange),
                Expression = FilterByStat(filter?.Stats?.Physical?.Skill, null, filter?.Stats?.Physical?.To)
            },
            new() {
                Condition = BuildLimitFromCondition(filter?.Stats?.Mental, ValidationConstants.StatValueRange),
                Expression = FilterByStat(filter?.Stats?.Mental?.Skill, filter?.Stats?.Mental?.From, null)
            },
            new() {
                Condition = BuildLimitToCondition(filter?.Stats?.Mental, ValidationConstants.StatValueRange),
                Expression = FilterByStat(filter?.Stats?.Mental?.Skill, null, filter?.Stats?.Mental?.To)
            },
            new() {
                Condition = BuildLimitFromCondition(filter?.Stats?.Skill, ValidationConstants.StatValueRange),
                Expression = FilterByStat(filter?.Stats?.Skill?.Skill, filter?.Stats?.Skill?.From, null)
            },
            new() {
                Condition = BuildLimitToCondition(filter?.Stats?.Skill, ValidationConstants.StatValueRange),
                Expression = FilterByStat(filter?.Stats?.Skill?.Skill, null, filter?.Stats?.Skill?.To)
            },
            new() {
                Condition = filter?.Stats?.Raiting.HasValue ?? false,
                Expression = FilterByRaiting(filter?.Stats?.Raiting)
            }
        ];
    }

    #region Private

    public static Expression<Func<PlayerEntity, bool>> FilterByStat(int? skill, short? from, short? to)
    {
        if (from.HasValue)
        {
            return player => ((int)Math.Ceiling((double)player.Stats.Where(s => !skill.HasValue || (int?)s.Type.SkillId == skill).Sum(m => m.Value)
                / (player.Stats.Where(s => !skill.HasValue || (int?)s.Type.SkillId == skill).Count() * PlayerConstants.StatMaxValue)
                * ValidationConstants.PercentageMaxValue)) >= from;
        }
        else if (to.HasValue)
        {
            return player => ((int)Math.Ceiling((double)player.Stats.Where(s => !skill.HasValue || (int?)s.Type.SkillId == skill).Sum(m => m.Value)
                / (player.Stats.Where(s => !skill.HasValue || (int?)s.Type.SkillId == skill).Count() * PlayerConstants.StatMaxValue)
                * ValidationConstants.PercentageMaxValue)) <= to;
        }

        return player => true;
    }

    public static Expression<Func<PlayerEntity, bool>> FilterByRaiting(int? raiting)
    {
        return raiting.HasValue
            ? player => ((PlayerConstants.StarsMaxValue * (int)Math.Ceiling((double)player.Stats.Sum(m => m.Value)
                / (player.Stats.Count() * PlayerConstants.StatMaxValue) * ValidationConstants.PercentageMaxValue))
                / ValidationConstants.PercentageMaxValue) >= raiting
            : player => true;
    }

    private static bool BuildLimitFromCondition(RangeLimitDto<short?>? limit, Tuple<int, int> rangeLimit)
    {
        return (limit?.From.HasValue ?? false)
            && !(limit.From == rangeLimit.Item1 && limit.To == rangeLimit.Item2);
    }

    private static bool BuildLimitToCondition(RangeLimitDto<short?>? limit, Tuple<int, int> rangeLimit)
    {
        return (limit?.To.HasValue ?? false)
            && !(limit.From == rangeLimit.Item1 && limit.To == rangeLimit.Item2);
    }

    #endregion Private
}
