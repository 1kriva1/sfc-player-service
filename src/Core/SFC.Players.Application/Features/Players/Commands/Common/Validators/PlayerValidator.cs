using FluentValidation;

using SFC.Players.Application.Common.Constants;
using SFC.Players.Application.Common.Extensions;
using SFC.Players.Application.Interfaces.Common;
using SFC.Players.Application.Models.Players.Common;
using SFC.Players.Domain.Enums;

namespace SFC.Players.Application.Features.Players.Commands.Common.Validators;

public class PlayerValidator<T> : AbstractValidator<T> where T : BasePlayerDto
{
    private readonly IDateTimeService _dateTimeService;

    public PlayerValidator(IDateTimeService dateTimeService)
    {
        _dateTimeService = dateTimeService;

        SetRulesForGeneralProfile();

        SetRulesForFootballProfile();

        SetRulesForStats();
    }

    private void SetRulesForGeneralProfile()
    {
        RuleFor(p => p.Profile.General.FirstName)
           .RequiredProperty(ValidationConstants.NAME_VALUE_MAX_LENGTH, nameof(PlayerGeneralProfileDto.FirstName));

        RuleFor(p => p.Profile.General.LastName)
           .RequiredProperty(ValidationConstants.NAME_VALUE_MAX_LENGTH, nameof(PlayerGeneralProfileDto.LastName));

        RuleFor(p => p.Profile.General.Biography)
           .MaximumLength(ValidationConstants.DESCRIPTION_VALUE_MAX_LENGTH)
           .WithName(nameof(PlayerGeneralProfileDto.Biography));

        RuleFor(p => p.Profile.General.City)
           .RequiredProperty(ValidationConstants.CITY_VALUE_MAX_LENGTH, nameof(PlayerGeneralProfileDto.City));

        When(p => p.Profile.General.Birthday.HasValue, () =>
#pragma warning disable CS8629 // Nullable value type may be null.
            RuleFor(p => p.Profile.General.Birthday)
               .Must(value => value.Value.Date >= _dateTimeService.Now.Date.AddYears(-ValidationConstants.MAX_YEARS_OLD))
               .WithName(nameof(PlayerGeneralProfileDto.Birthday))
               .WithMessage(string.Format(Messages.InvalidBirthdayMaxValue, ValidationConstants.MAX_YEARS_OLD))
               .Must(value => value.Value.Date < _dateTimeService.Now.Date)
               .WithName(nameof(PlayerGeneralProfileDto.Birthday))
               .WithMessage(Messages.InvalidBirthdayMinValue));

        When(p => p.Profile.General.Photo != null, () =>
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            RuleFor(p => p.Profile.General.Photo.Size)
                .InclusiveBetween(1, ValidationConstants.PHOTO_MAX_SIZE_IN_BYTES)
                .WithName(nameof(PlayerGeneralProfileDto.Photo));

            RuleFor(p => p.Profile.General.Photo.Extension)
                .IsInEnum()
                .WithName(nameof(PlayerGeneralProfileDto.Photo));
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        });

        When(p => p.Profile.General.Tags?.Any() ?? false, () =>
        {
            RuleFor(p => p.Profile.General.Tags)
            .Must(tags => tags.Distinct().Count() == tags.Count())
            .WithMessage(Messages.TagsUniqueness)
            .Must(tags => tags.Count() <= ValidationConstants.TAGS_MAX_LENGTH)
            .WithName(nameof(PlayerGeneralProfileDto.Tags))
            .WithMessage(string.Format(Messages.InvalidTagsSize, nameof(PlayerGeneralProfileDto.Tags), ValidationConstants.TAGS_MAX_LENGTH));

            RuleForEach(p => p.Profile.General.Tags)
                .NotEmpty()
                .WithName(nameof(PlayerGeneralProfileDto.Tags))
                .WithMessage(Messages.TagEmpty)
                .MaximumLength(ValidationConstants.TAG_VALUE_MAX_LENGTH)
                .WithMessage(Messages.TagMaxLength);
        });

        When(p => p.Profile.General.Availability?.Days?.Any() ?? false, () =>
        {
            RuleFor(p => p.Profile.General.Availability.Days)
            .Must(days => days.Count() <= Enum.GetNames(typeof(DayOfWeek)).Length)
            .WithMessage(string.Format(Messages.AvailableDaysSize, nameof(PlayerAvailabilityDto.Days), Enum.GetNames(typeof(DayOfWeek)).Length));

            RuleForEach(p => p.Profile.General.Availability.Days)
                .IsInEnum()
                .WithName(nameof(PlayerAvailabilityDto.Days))
                .WithMessage(Messages.InvalidAvailableDay);

        });

        When(p => (p.Profile.General.Availability?.From.HasValue ?? false)
            && (p.Profile.General.Availability?.To.HasValue ?? false), () =>
            {
#pragma warning disable CS8629 // Nullable value type may be null.
                RuleFor(p => p.Profile.General.Availability.To)
                    .GreaterThan(p => p.Profile.General.Availability.From.Value)
                    .WithMessage(string.Format(Messages.MustBeGreaterThan, nameof(PlayerAvailabilityDto.To), nameof(PlayerAvailabilityDto.From)));

                RuleFor(p => p.Profile.General.Availability.From)
                    .LessThan(p => p.Profile.General.Availability.To.Value)
                    .WithMessage(string.Format(Messages.MustBeLessThan, nameof(PlayerAvailabilityDto.From), nameof(PlayerAvailabilityDto.To)));
#pragma warning restore CS8629 // Nullable value type may be null.
            });
    }

    private void SetRulesForFootballProfile()
    {
        When(p => p.Profile.Football.Height.HasValue, () =>
        {
            RuleFor(p => p.Profile.Football.Height)
                .InclusiveBetween(ValidationConstants.PLAYER_SIZE_RANGE.Item1, ValidationConstants.PLAYER_SIZE_RANGE.Item2)
                .WithName(nameof(PlayerFootballProfileDto.Height));
        });

        When(p => p.Profile.Football.Weight.HasValue, () =>
        {
            RuleFor(p => p.Profile.Football.Weight)
                .InclusiveBetween(ValidationConstants.PLAYER_SIZE_RANGE.Item1, ValidationConstants.PLAYER_SIZE_RANGE.Item2)
                .WithName(nameof(PlayerFootballProfileDto.Weight));
        });

        When(p => p.Profile.Football.Position.HasValue, () =>
        {
            RuleFor(p => p.Profile.Football.Position)
                .IsInEnum()
                .NotEqual(p => p.Profile.Football.AdditionalPosition)
                .WithName(nameof(PlayerFootballProfileDto.Position))
                .WithMessage(string.Format(Messages.MustBeNotEqual, nameof(PlayerFootballProfileDto.AdditionalPosition)));
        });

        When(p => p.Profile.Football.AdditionalPosition.HasValue, () =>
        {
            RuleFor(p => p.Profile.Football.AdditionalPosition)
                .IsInEnum()
                .NotEqual(p => p.Profile.Football.Position)
                .WithName(nameof(PlayerFootballProfileDto.AdditionalPosition))
                .WithMessage(string.Format(Messages.MustBeNotEqual, nameof(PlayerFootballProfileDto.Position)));
        });

        When(p => p.Profile.Football.WorkingFoot.HasValue, () =>
        {
            RuleFor(p => p.Profile.Football.WorkingFoot)
                .IsInEnum()
                .WithName(nameof(PlayerFootballProfileDto.WorkingFoot));
        });

        When(p => p.Profile.Football.Number.HasValue, () =>
        {
            RuleFor(p => p.Profile.Football.Number)
                .InclusiveBetween(ValidationConstants.PLAYER_NUMBER_RANGE.Item1, ValidationConstants.PLAYER_NUMBER_RANGE.Item2)
                .WithName(nameof(PlayerFootballProfileDto.Number));
        });

        When(p => p.Profile.Football.GameStyle.HasValue, () =>
        {
            RuleFor(p => p.Profile.Football.GameStyle)
                .IsInEnum()
                .WithName(nameof(PlayerFootballProfileDto.GameStyle));
        });

        When(p => p.Profile.Football.Skill.HasValue, () =>
        {
            RuleFor(p => p.Profile.Football.Skill)
                .InclusiveBetween(ValidationConstants.RAITING_VALUE_RANGE.Item1, ValidationConstants.RAITING_VALUE_RANGE.Item2)
                .WithName(nameof(PlayerFootballProfileDto.Skill));
        });

        When(p => p.Profile.Football.WeakFoot.HasValue, () =>
        {
            RuleFor(p => p.Profile.Football.WeakFoot)
                .InclusiveBetween(ValidationConstants.RAITING_VALUE_RANGE.Item1, ValidationConstants.RAITING_VALUE_RANGE.Item2)
                .WithName(nameof(PlayerFootballProfileDto.WeakFoot));
        });

        When(p => p.Profile.Football.PhysicalCondition.HasValue, () =>
        {
            RuleFor(p => p.Profile.Football.PhysicalCondition)
                .InclusiveBetween(ValidationConstants.RAITING_VALUE_RANGE.Item1, ValidationConstants.RAITING_VALUE_RANGE.Item2)
                .WithName(nameof(PlayerFootballProfileDto.PhysicalCondition));
        });
    }

    private void SetRulesForStats()
    {
        RuleFor(p => p.Stats.Points.Available)
            .GreaterThanOrEqualTo(0)
            .WithName(nameof(PlayerStatPointsDto.Available));

        RuleFor(p => p.Stats.Points.Used)
            .GreaterThanOrEqualTo(0)
            .WithName(nameof(PlayerStatPointsDto.Used));

        RuleFor(p => p.Stats.Values)
            .Must(stats => stats.Count() == ValidationConstants.STATS_COUNT)
            .WithName(nameof(BasePlayerDto.Stats))
            .WithMessage(string.Format(Messages.LengthMustBeEqual, nameof(BasePlayerDto.Stats), ValidationConstants.STATS_COUNT));

        RuleForEach(p => p.Stats.Values)
            .Must(stat => Enum.IsDefined(typeof(StatCategory), stat.Category))
            .WithName(nameof(PlayerStatValueDto.Category))
            .WithMessage(string.Format(Messages.MustBeInCategoryRange, nameof(BasePlayerDto.Stats), nameof(PlayerStatValueDto.Category)))
            .Must(stat => Enum.IsDefined(typeof(StatType), stat.Type))
            .WithName(nameof(PlayerStatValueDto.Type))
            .WithMessage(string.Format(Messages.MustBeInStatTypeRange, nameof(BasePlayerDto.Stats), nameof(PlayerStatValueDto.Type)))
            .Must(stat =>
            {
                ValidationConstants.CATEGORY_TYPE_STAT_RELATIONS.TryGetValue(stat.Category, out IEnumerable<StatType>? types);
                return types?.Contains(stat.Type) ?? false;
            })
            .WithMessage(string.Format(Messages.EachStatTypeMustBeInSpecificCategory, nameof(BasePlayerDto.Stats), nameof(PlayerStatValueDto.Type), nameof(PlayerStatValueDto.Category)))
            .Must(stat => ValidationConstants.STAT_VALUE_RANGE.Item1 <= stat.Value && stat.Value <= ValidationConstants.STAT_VALUE_RANGE.Item2)
            .WithName(nameof(PlayerStatValueDto.Value))
            .WithMessage(string.Format(Messages.StatValueMustBeInRange, nameof(BasePlayerDto.Stats), nameof(PlayerStatValueDto.Value),
                ValidationConstants.STAT_VALUE_RANGE.Item1, ValidationConstants.STAT_VALUE_RANGE.Item2));
    }
}