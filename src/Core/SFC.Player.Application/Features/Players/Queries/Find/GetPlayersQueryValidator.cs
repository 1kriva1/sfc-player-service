using FluentValidation;

using SFC.Player.Application.Common.Constants;
using SFC.Player.Application.Features.Common.Dto;
using SFC.Player.Application.Features.Players.Common.Dto;
using SFC.Player.Application.Features.Players.Queries.Find.Dto.Filters;

namespace SFC.Player.Application.Features.Players.Queries.Find;
public class GetPlayersQueryValidator : AbstractValidator<GetPlayersQuery>
{
    public GetPlayersQueryValidator()
    {

        When(p => p.Filter?.Profile?.General != null, () =>
        {
            RuleFor(p => p.Filter.Profile.General.Name)
                .MaximumLength(ValidationConstants.NAME_VALUE_MAX_LENGTH)
                .WithName(nameof(GetPlayersGeneralProfileFilterDto.Name));

            RuleFor(p => p.Filter.Profile.General.City)
                 .MaximumLength(ValidationConstants.CITY_VALUE_MAX_LENGTH)
                 .WithName(nameof(GetPlayersGeneralProfileFilterDto.City));

            When(p => p.Filter.Profile.General.Tags?.Any() ?? false, () =>
            {
                RuleFor(p => p.Filter.Profile.General.Tags)
                    .Must(tags => tags.Distinct().Count() == tags.Count())
                    .WithMessage(Messages.TagsUniqueness)
                    .Must(tags => tags.Count() <= ValidationConstants.TAGS_MAX_LENGTH)
                    .WithName(nameof(GetPlayersGeneralProfileFilterDto.Tags))
                    .WithMessage(string.Format(Messages.InvalidTagsSize, nameof(GetPlayersGeneralProfileFilterDto.Tags), ValidationConstants.TAGS_MAX_LENGTH));

                RuleForEach(p => p.Filter.Profile.General.Tags)
                    .NotEmpty()
                    .WithName(nameof(GetPlayersGeneralProfileFilterDto.Tags))
                    .WithMessage(Messages.TagEmpty)
                    .MaximumLength(ValidationConstants.TAG_VALUE_MAX_LENGTH)
                    .WithMessage(Messages.TagMaxLength);
            });

            When(p => p.Filter.Profile.General.Availability?.Days?.Any() ?? false, () =>
            {
                RuleFor(p => p.Filter.Profile.General.Availability.Days)
                    .Must(days => days.Count() <= Enum.GetNames(typeof(DayOfWeek)).Length)
                    .WithMessage(string.Format(Messages.AvailableDaysSize, nameof(GetPlayersAvailabilityLimitDto.Days), Enum.GetNames(typeof(DayOfWeek)).Length));

                RuleForEach(p => p.Filter.Profile.General.Availability.Days)
                    .IsInEnum()
                    .WithName(nameof(GetPlayersAvailabilityLimitDto.Days))
                    .WithMessage(Messages.InvalidAvailableDay);

            });

            When(p => (p.Filter.Profile.General.Availability?.From.HasValue ?? false)
                && (p.Filter.Profile.General.Availability?.To.HasValue ?? false), () =>
                {
#pragma warning disable CS8629 // Nullable value type may be null.
                    RuleFor(p => p.Filter.Profile.General.Availability.To)
                        .GreaterThanOrEqualTo(p => p.Filter.Profile.General.Availability.From.Value)
                        .WithMessage(string.Format(Messages.MustBeGreaterThan, nameof(GetPlayersAvailabilityLimitDto.To), nameof(GetPlayersAvailabilityLimitDto.From)));

                    RuleFor(p => p.Filter.Profile.General.Availability.From)
                        .LessThanOrEqualTo(p => p.Filter.Profile.General.Availability.To.Value)
                        .WithMessage(string.Format(Messages.MustBeLessThan, nameof(GetPlayersAvailabilityLimitDto.From), nameof(GetPlayersAvailabilityLimitDto.To)));
#pragma warning restore CS8629 // Nullable value type may be null.
                });

            When(p => (p.Filter.Profile.General.Years?.From.HasValue ?? false)
                && (p.Filter.Profile.General.Years?.To.HasValue ?? false), () =>
                {
#pragma warning disable CS8629 // Nullable value type may be null.
                    RuleFor(p => p.Filter.Profile.General.Years.To)
                        .GreaterThanOrEqualTo(p => p.Filter.Profile.General.Years.From.Value)
                        .WithMessage(string.Format(Messages.MustBeGreaterThan, nameof(RangeLimitDto<short?>.To), nameof(RangeLimitDto<short?>.From)));

                    RuleFor(p => p.Filter.Profile.General.Years.From)
                        .LessThanOrEqualTo(p => p.Filter.Profile.General.Years.To.Value)
                        .WithMessage(string.Format(Messages.MustBeLessThan, nameof(RangeLimitDto<short?>.From), nameof(RangeLimitDto<short?>.To)));
#pragma warning restore CS8629 // Nullable value type may be null.
                });
        });

        When(p => p.Filter?.Profile?.Football != null, () =>
        {
            When(p => (p.Filter.Profile.Football.Height?.From.HasValue ?? false)
            && (p.Filter.Profile.Football.Height?.To.HasValue ?? false), () =>
            {
#pragma warning disable CS8629 // Nullable value type may be null.
                RuleFor(p => p.Filter.Profile.Football.Height.To)
                    .GreaterThanOrEqualTo(p => p.Filter.Profile.Football.Height.From.Value)
                    .WithMessage(string.Format(Messages.MustBeGreaterThan, nameof(RangeLimitDto<short?>.To), nameof(RangeLimitDto<short?>.From)));

                RuleFor(p => p.Filter.Profile.Football.Height.From)
                    .LessThanOrEqualTo(p => p.Filter.Profile.Football.Height.To.Value)
                    .WithMessage(string.Format(Messages.MustBeLessThan, nameof(RangeLimitDto<short?>.From), nameof(RangeLimitDto<short?>.To)));
#pragma warning restore CS8629 // Nullable value type may be null.
            });

            When(p => (p.Filter.Profile.Football.Weight?.From.HasValue ?? false)
                && (p.Filter.Profile.Football.Weight?.To.HasValue ?? false), () =>
                {
#pragma warning disable CS8629 // Nullable value type may be null.
                    RuleFor(p => p.Filter.Profile.Football.Weight.To)
                        .GreaterThanOrEqualTo(p => p.Filter.Profile.Football.Weight.From.Value)
                        .WithMessage(string.Format(Messages.MustBeGreaterThan, nameof(RangeLimitDto<short?>.To), nameof(RangeLimitDto<short?>.From)));

                    RuleFor(p => p.Filter.Profile.Football.Weight.From)
                        .LessThanOrEqualTo(p => p.Filter.Profile.Football.Weight.To.Value)
                        .WithMessage(string.Format(Messages.MustBeLessThan, nameof(RangeLimitDto<short?>.From), nameof(RangeLimitDto<short?>.To)));
#pragma warning restore CS8629 // Nullable value type may be null.
                });
        });

        When(p => p.Filter?.Stats != null, () =>
        {
            When(p => (p.Filter.Stats.Total?.From.HasValue ?? false)
            && (p.Filter.Stats.Total?.To.HasValue ?? false), () =>
            {
#pragma warning disable CS8629 // Nullable value type may be null.
                RuleFor(p => p.Filter.Stats.Total.To)
                    .GreaterThanOrEqualTo(p => p.Filter.Stats.Total.From.Value)
                    .WithMessage(string.Format(Messages.MustBeGreaterThan, nameof(RangeLimitDto<short?>.To), nameof(RangeLimitDto<short?>.From)));

                RuleFor(p => p.Filter.Stats.Total.From)
                    .LessThanOrEqualTo(p => p.Filter.Stats.Total.To.Value)
                    .WithMessage(string.Format(Messages.MustBeLessThan, nameof(RangeLimitDto<short?>.From), nameof(RangeLimitDto<short?>.To)));
#pragma warning restore CS8629 // Nullable value type may be null.
            });

            When(p => (p.Filter.Stats.Physical?.From.HasValue ?? false)
                && (p.Filter.Stats.Physical?.To.HasValue ?? false), () =>
                {
#pragma warning disable CS8629 // Nullable value type may be null.
                    RuleFor(p => p.Filter.Stats.Physical.To)
                        .GreaterThanOrEqualTo(p => p.Filter.Stats.Physical.From.Value)
                        .WithMessage(string.Format(Messages.MustBeGreaterThan, nameof(GetPlayersStatsBySkillRangeLimitDto.To), nameof(GetPlayersStatsBySkillRangeLimitDto.From)));

                    RuleFor(p => p.Filter.Stats.Physical.From)
                        .LessThanOrEqualTo(p => p.Filter.Stats.Physical.To.Value)
                        .WithMessage(string.Format(Messages.MustBeLessThan, nameof(GetPlayersStatsBySkillRangeLimitDto.From), nameof(GetPlayersStatsBySkillRangeLimitDto.To)));
#pragma warning restore CS8629 // Nullable value type may be null.
                });

            When(p => (p.Filter.Stats.Mental?.From.HasValue ?? false)
                && (p.Filter.Stats.Mental?.To.HasValue ?? false), () =>
                {
#pragma warning disable CS8629 // Nullable value type may be null.
                    RuleFor(p => p.Filter.Stats.Mental.To)
                        .GreaterThanOrEqualTo(p => p.Filter.Stats.Mental.From.Value)
                        .WithMessage(string.Format(Messages.MustBeGreaterThan, nameof(GetPlayersStatsBySkillRangeLimitDto.To), nameof(GetPlayersStatsBySkillRangeLimitDto.From)));

                    RuleFor(p => p.Filter.Stats.Mental.From)
                        .LessThanOrEqualTo(p => p.Filter.Stats.Mental.To.Value)
                        .WithMessage(string.Format(Messages.MustBeLessThan, nameof(GetPlayersStatsBySkillRangeLimitDto.From), nameof(GetPlayersStatsBySkillRangeLimitDto.To)));
#pragma warning restore CS8629 // Nullable value type may be null.
                });

            When(p => (p.Filter.Stats.Skill?.From.HasValue ?? false)
                && (p.Filter.Stats.Skill?.To.HasValue ?? false), () =>
                {
#pragma warning disable CS8629 // Nullable value type may be null.
                    RuleFor(p => p.Filter.Stats.Skill.To)
                        .GreaterThanOrEqualTo(p => p.Filter.Stats.Skill.From.Value)
                        .WithMessage(string.Format(Messages.MustBeGreaterThan, nameof(GetPlayersStatsBySkillRangeLimitDto.To), nameof(GetPlayersStatsBySkillRangeLimitDto.From)));

                    RuleFor(p => p.Filter.Stats.Skill.From)
                        .LessThanOrEqualTo(p => p.Filter.Stats.Skill.To.Value)
                        .WithMessage(string.Format(Messages.MustBeLessThan, nameof(GetPlayersStatsBySkillRangeLimitDto.From), nameof(GetPlayersStatsBySkillRangeLimitDto.To)));
#pragma warning restore CS8629 // Nullable value type may be null.
                });
        });
    }
}
