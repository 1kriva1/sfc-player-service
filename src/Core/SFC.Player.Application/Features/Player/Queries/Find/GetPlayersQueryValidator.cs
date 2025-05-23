using FluentValidation;

using SFC.Player.Application.Common.Constants;
using SFC.Player.Application.Common.Extensions;
using SFC.Player.Application.Features.Common.Dto.Common;
using SFC.Player.Application.Features.Common.Validators.Common;
using SFC.Player.Application.Features.Player.Queries.Find.Dto.Filters;

namespace SFC.Player.Application.Features.Player.Queries.Find;
public class GetPlayersQueryValidator : AbstractValidator<GetPlayersQuery>
{
    public GetPlayersQueryValidator()
    {
        // pagination request validation
        RuleFor(command => command)
            .SetValidator(new PaginationRequestValidator<GetPlayersViewModel, GetPlayersFilterDto>());

        When(p => p.Filter?.Profile?.General != null, () =>
        {
            RuleFor(p => p.Filter.Profile.General.Name)
                .MaximumLength(ValidationConstants.NameValueMaxLength)
                .WithName(nameof(GetPlayersGeneralProfileFilterDto.Name));

            RuleFor(p => p.Filter.Profile.General.City)
                 .MaximumLength(ValidationConstants.CityValueMaxLength)
                 .WithName(nameof(GetPlayersGeneralProfileFilterDto.City));

            When(p => p.Filter.Profile.General.Tags?.Any() ?? false, () =>
            {
                RuleFor(p => p.Filter.Profile.General.Tags)
                    .Must(tags => tags.Distinct().Count() == tags.Count())
                    .WithMessage(Localization.TagsUniqueness)
                    .Must(tags => tags.Count() <= ValidationConstants.TagsMaxLength)
                    .WithName(nameof(GetPlayersGeneralProfileFilterDto.Tags))
                    .WithMessage(Localization.InvalidTagsSize.BuildValidationMessage(nameof(GetPlayersGeneralProfileFilterDto.Tags), ValidationConstants.TagsMaxLength));

                RuleForEach(p => p.Filter.Profile.General.Tags)
                    .NotEmpty()
                    .WithName(nameof(GetPlayersGeneralProfileFilterDto.Tags))
                    .WithMessage(Localization.TagEmpty)
                    .MaximumLength(ValidationConstants.TagValueMaxLength)
                    .WithMessage(Localization.TagMaxLength);
            });

            When(p => p.Filter.Profile.General.Availability?.Days?.Any() ?? false, () =>
            {
                RuleFor(p => p.Filter.Profile.General.Availability.Days)
                    .Must(days => days.Count() <= Enum.GetNames(typeof(DayOfWeek)).Length)
                    .WithMessage(Localization.AvailableDaysSize.BuildValidationMessage(nameof(GetPlayersAvailabilityLimitDto.Days), Enum.GetNames(typeof(DayOfWeek)).Length));

                RuleForEach(p => p.Filter.Profile.General.Availability.Days)
                    .IsInEnum()
                    .WithName(nameof(GetPlayersAvailabilityLimitDto.Days))
                    .WithMessage(Localization.InvalidAvailableDay);

            });

            When(p => (p.Filter.Profile.General.Availability?.From.HasValue ?? false)
                && (p.Filter.Profile.General.Availability?.To.HasValue ?? false), () =>
                {
#pragma warning disable CS8629 // Nullable value type may be null.
                    RuleFor(p => p.Filter.Profile.General.Availability.To)
                        .GreaterThanOrEqualTo(p => p.Filter.Profile.General.Availability.From.Value)
                        .WithMessage(Localization.MustBeGreaterThan.BuildValidationMessage(nameof(GetPlayersAvailabilityLimitDto.To), nameof(GetPlayersAvailabilityLimitDto.From)));

                    RuleFor(p => p.Filter.Profile.General.Availability.From)
                        .LessThanOrEqualTo(p => p.Filter.Profile.General.Availability.To.Value)
                        .WithMessage(Localization.MustBeLessThan.BuildValidationMessage(nameof(GetPlayersAvailabilityLimitDto.From), nameof(GetPlayersAvailabilityLimitDto.To)));
#pragma warning restore CS8629 // Nullable value type may be null.
                });

            When(p => (p.Filter.Profile.General.Years?.From.HasValue ?? false)
                && (p.Filter.Profile.General.Years?.To.HasValue ?? false), () =>
                {
#pragma warning disable CS8629 // Nullable value type may be null.
                    RuleFor(p => p.Filter.Profile.General.Years.To)
                        .GreaterThanOrEqualTo(p => p.Filter.Profile.General.Years.From.Value)
                        .WithMessage(Localization.MustBeGreaterThan.BuildValidationMessage(nameof(RangeLimitDto<short?>.To), nameof(RangeLimitDto<short?>.From)));

                    RuleFor(p => p.Filter.Profile.General.Years.From)
                        .LessThanOrEqualTo(p => p.Filter.Profile.General.Years.To.Value)
                        .WithMessage(Localization.MustBeLessThan.BuildValidationMessage(nameof(RangeLimitDto<short?>.From), nameof(RangeLimitDto<short?>.To)));
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
                    .WithMessage(Localization.MustBeGreaterThan.BuildValidationMessage(nameof(RangeLimitDto<short?>.To), nameof(RangeLimitDto<short?>.From)));

                RuleFor(p => p.Filter.Profile.Football.Height.From)
                    .LessThanOrEqualTo(p => p.Filter.Profile.Football.Height.To.Value)
                    .WithMessage(Localization.MustBeLessThan.BuildValidationMessage(nameof(RangeLimitDto<short?>.From), nameof(RangeLimitDto<short?>.To)));
#pragma warning restore CS8629 // Nullable value type may be null.
            });

            When(p => (p.Filter.Profile.Football.Weight?.From.HasValue ?? false)
                && (p.Filter.Profile.Football.Weight?.To.HasValue ?? false), () =>
                {
#pragma warning disable CS8629 // Nullable value type may be null.
                    RuleFor(p => p.Filter.Profile.Football.Weight.To)
                        .GreaterThanOrEqualTo(p => p.Filter.Profile.Football.Weight.From.Value)
                        .WithMessage(Localization.MustBeGreaterThan.BuildValidationMessage(nameof(RangeLimitDto<short?>.To), nameof(RangeLimitDto<short?>.From)));

                    RuleFor(p => p.Filter.Profile.Football.Weight.From)
                        .LessThanOrEqualTo(p => p.Filter.Profile.Football.Weight.To.Value)
                        .WithMessage(Localization.MustBeLessThan.BuildValidationMessage(nameof(RangeLimitDto<short?>.From), nameof(RangeLimitDto<short?>.To)));
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
                    .WithMessage(Localization.MustBeGreaterThan.BuildValidationMessage(nameof(RangeLimitDto<short?>.To), nameof(RangeLimitDto<short?>.From)));

                RuleFor(p => p.Filter.Stats.Total.From)
                    .LessThanOrEqualTo(p => p.Filter.Stats.Total.To.Value)
                    .WithMessage(Localization.MustBeLessThan.BuildValidationMessage(nameof(RangeLimitDto<short?>.From), nameof(RangeLimitDto<short?>.To)));
#pragma warning restore CS8629 // Nullable value type may be null.
            });

            When(p => (p.Filter.Stats.Physical?.From.HasValue ?? false)
                && (p.Filter.Stats.Physical?.To.HasValue ?? false), () =>
                {
#pragma warning disable CS8629 // Nullable value type may be null.
                    RuleFor(p => p.Filter.Stats.Physical.To)
                        .GreaterThanOrEqualTo(p => p.Filter.Stats.Physical.From.Value)
                        .WithMessage(Localization.MustBeGreaterThan.BuildValidationMessage(nameof(GetPlayersStatsBySkillRangeLimitDto.To), nameof(GetPlayersStatsBySkillRangeLimitDto.From)));

                    RuleFor(p => p.Filter.Stats.Physical.From)
                        .LessThanOrEqualTo(p => p.Filter.Stats.Physical.To.Value)
                        .WithMessage(Localization.MustBeLessThan.BuildValidationMessage(nameof(GetPlayersStatsBySkillRangeLimitDto.From), nameof(GetPlayersStatsBySkillRangeLimitDto.To)));
#pragma warning restore CS8629 // Nullable value type may be null.
                });

            When(p => (p.Filter.Stats.Mental?.From.HasValue ?? false)
                && (p.Filter.Stats.Mental?.To.HasValue ?? false), () =>
                {
#pragma warning disable CS8629 // Nullable value type may be null.
                    RuleFor(p => p.Filter.Stats.Mental.To)
                        .GreaterThanOrEqualTo(p => p.Filter.Stats.Mental.From.Value)
                        .WithMessage(Localization.MustBeGreaterThan.BuildValidationMessage(nameof(GetPlayersStatsBySkillRangeLimitDto.To), nameof(GetPlayersStatsBySkillRangeLimitDto.From)));

                    RuleFor(p => p.Filter.Stats.Mental.From)
                        .LessThanOrEqualTo(p => p.Filter.Stats.Mental.To.Value)
                        .WithMessage(Localization.MustBeLessThan.BuildValidationMessage(nameof(GetPlayersStatsBySkillRangeLimitDto.From), nameof(GetPlayersStatsBySkillRangeLimitDto.To)));
#pragma warning restore CS8629 // Nullable value type may be null.
                });

            When(p => (p.Filter.Stats.Skill?.From.HasValue ?? false)
                && (p.Filter.Stats.Skill?.To.HasValue ?? false), () =>
                {
#pragma warning disable CS8629 // Nullable value type may be null.
                    RuleFor(p => p.Filter.Stats.Skill.To)
                        .GreaterThanOrEqualTo(p => p.Filter.Stats.Skill.From.Value)
                        .WithMessage(Localization.MustBeGreaterThan.BuildValidationMessage(nameof(GetPlayersStatsBySkillRangeLimitDto.To), nameof(GetPlayersStatsBySkillRangeLimitDto.From)));

                    RuleFor(p => p.Filter.Stats.Skill.From)
                        .LessThanOrEqualTo(p => p.Filter.Stats.Skill.To.Value)
                        .WithMessage(Localization.MustBeLessThan.BuildValidationMessage(nameof(GetPlayersStatsBySkillRangeLimitDto.From), nameof(GetPlayersStatsBySkillRangeLimitDto.To)));
#pragma warning restore CS8629 // Nullable value type may be null.
                });
        });
    }
}
