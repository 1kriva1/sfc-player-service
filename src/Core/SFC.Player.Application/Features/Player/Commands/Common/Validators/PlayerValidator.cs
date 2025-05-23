using FluentValidation;

using SFC.Player.Application.Common.Constants;
using SFC.Player.Application.Common.Extensions;
using SFC.Player.Application.Features.Player.Common.Dto;
using SFC.Player.Application.Interfaces.Common;
using SFC.Player.Application.Interfaces.Persistence.Repository.Data;
using SFC.Player.Domain.Entities.Data;

namespace SFC.Player.Application.Features.Player.Commands.Common.Validators;

public class PlayerValidator<T> : AbstractValidator<T> where T : BasePlayerDto
{
    private readonly IDateTimeService _dateTimeService;
    private readonly IStatTypeRepository _statTypeRepository;
    private readonly IFootballPositionRepository _footballPositionRepository;
    private readonly IWorkingFootRepository _workingFootRepository;
    private readonly IGameStyleRepository _gameStyleRepository;

    public PlayerValidator(
        IDateTimeService dateTimeService,
        IStatTypeRepository statTypeRepository,
        IFootballPositionRepository footballPositionRepository,
        IWorkingFootRepository workingFootRepository,
        IGameStyleRepository gameStyleRepository)
    {
        _dateTimeService = dateTimeService;
        _statTypeRepository = statTypeRepository;
        _footballPositionRepository = footballPositionRepository;
        _workingFootRepository = workingFootRepository;
        _gameStyleRepository = gameStyleRepository;
        SetRulesForGeneralProfile();

        SetRulesForFootballProfile();

        SetRulesForStats();        
    }

    private void SetRulesForGeneralProfile()
    {
        RuleFor(p => p.Profile.General.FirstName)
           .RequiredProperty(ValidationConstants.NameValueMaxLength, nameof(PlayerGeneralProfileDto.FirstName));

        RuleFor(p => p.Profile.General.LastName)
           .RequiredProperty(ValidationConstants.NameValueMaxLength, nameof(PlayerGeneralProfileDto.LastName));

        RuleFor(p => p.Profile.General.Biography)
           .MaximumLength(ValidationConstants.DescriptionValueMaxLength)
           .WithName(nameof(PlayerGeneralProfileDto.Biography));

        RuleFor(p => p.Profile.General.City)
           .RequiredProperty(ValidationConstants.CityValueMaxLength, nameof(PlayerGeneralProfileDto.City));

        When(p => p.Profile.General.Birthday.HasValue, () =>
#pragma warning disable CS8629 // Nullable value type may be null.
            RuleFor(p => p.Profile.General.Birthday)
               .Must(value => value.Value.Date >= _dateTimeService.Now.Date.AddYears(-ValidationConstants.MaxYearsOld))
               .WithName(nameof(PlayerGeneralProfileDto.Birthday))
               .WithMessage(Localization.InvalidBirthdayMaxValue.BuildValidationMessage(ValidationConstants.MaxYearsOld))
               .Must(value => value.Value.Date < _dateTimeService.Now.Date)
               .WithName(nameof(PlayerGeneralProfileDto.Birthday))
               .WithMessage(Localization.InvalidBirthdayMinValue));

        When(p => p.Profile.General.Photo != null, () =>
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            RuleFor(p => p.Profile.General.Photo.Size)
                .InclusiveBetween(1, ValidationConstants.PhotoMaxSizeInBytes)
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
                .WithMessage(Localization.TagsUniqueness)
                .Must(tags => tags.Count() <= ValidationConstants.TagsMaxLength)
                .WithName(nameof(PlayerGeneralProfileDto.Tags))
                .WithMessage(Localization.InvalidTagsSize.BuildValidationMessage(nameof(PlayerGeneralProfileDto.Tags), ValidationConstants.TagsMaxLength));

            RuleForEach(p => p.Profile.General.Tags)
                .NotEmpty()
                .WithName(nameof(PlayerGeneralProfileDto.Tags))
                .WithMessage(Localization.TagEmpty)
                .MaximumLength(ValidationConstants.TagValueMaxLength)
                .WithMessage(Localization.TagMaxLength);
        });

        When(p => p.Profile.General.Availability?.Days?.Any() ?? false, () =>
        {
            RuleFor(p => p.Profile.General.Availability.Days)
                .Must(days => days.Count() <= Enum.GetNames(typeof(DayOfWeek)).Length)
                .WithMessage(Localization.AvailableDaysSize.BuildValidationMessage(nameof(PlayerAvailabilityDto.Days), Enum.GetNames(typeof(DayOfWeek)).Length));

            RuleForEach(p => p.Profile.General.Availability.Days)
                .IsInEnum()
                .WithName(nameof(PlayerAvailabilityDto.Days))
                .WithMessage(Localization.InvalidAvailableDay);

        });

        When(p => (p.Profile.General.Availability?.From.HasValue ?? false)
            && (p.Profile.General.Availability?.To.HasValue ?? false), () =>
            {
#pragma warning disable CS8629 // Nullable value type may be null.
                RuleFor(p => p.Profile.General.Availability.To)
                    .GreaterThan(p => p.Profile.General.Availability.From.Value)
                    .WithMessage(Localization.MustBeGreaterThan.BuildValidationMessage(nameof(PlayerAvailabilityDto.To), nameof(PlayerAvailabilityDto.From)));

                RuleFor(p => p.Profile.General.Availability.From)
                    .LessThan(p => p.Profile.General.Availability.To.Value)
                    .WithMessage(Localization.MustBeLessThan.BuildValidationMessage(nameof(PlayerAvailabilityDto.From), nameof(PlayerAvailabilityDto.To)));
#pragma warning restore CS8629 // Nullable value type may be null.
            });
    }

    private void SetRulesForFootballProfile()
    {
        When(p => p.Profile.Football.Height.HasValue, () =>
        {
            RuleFor(p => p.Profile.Football.Height)
                .InclusiveBetween(ValidationConstants.PlayerSizeRange.Item1, ValidationConstants.PlayerSizeRange.Item2)
                .WithName(nameof(PlayerFootballProfileDto.Height));
        });

        When(p => p.Profile.Football.Weight.HasValue, () =>
        {
            RuleFor(p => p.Profile.Football.Weight)
                .InclusiveBetween(ValidationConstants.PlayerSizeRange.Item1, ValidationConstants.PlayerSizeRange.Item2)
                .WithName(nameof(PlayerFootballProfileDto.Weight));
        });

        When(p => p.Profile.Football.Position.HasValue, () =>
        {
            RuleFor(p => p.Profile.Football.Position)
                .MustAsync((position, cancellation) => _footballPositionRepository.AnyAsync((FootballPositionEnum)position!.Value))
                .WithName(nameof(PlayerFootballProfileDto.Position))
                .WithMessage(Localization.DataValidator)
                .NotEqual(p => p.Profile.Football.AdditionalPosition)
                .WithName(nameof(PlayerFootballProfileDto.Position))
                .WithMessage(Localization.MustBeNotEqual.BuildValidationMessage(nameof(PlayerFootballProfileDto.AdditionalPosition)));
        });

        When(p => p.Profile.Football.AdditionalPosition.HasValue, () =>
        {
            RuleFor(p => p.Profile.Football.AdditionalPosition)
                .MustAsync((additionalPosition, cancellation) => _footballPositionRepository.AnyAsync((FootballPositionEnum)additionalPosition!.Value))
                .WithName(nameof(PlayerFootballProfileDto.AdditionalPosition))
                .WithMessage(Localization.DataValidator)
                .NotEqual(p => p.Profile.Football.Position)
                .WithName(nameof(PlayerFootballProfileDto.AdditionalPosition))
                .WithMessage(Localization.MustBeNotEqual.BuildValidationMessage(nameof(PlayerFootballProfileDto.Position)));
        });

        When(p => p.Profile.Football.WorkingFoot.HasValue, () =>
        {
            RuleFor(p => p.Profile.Football.WorkingFoot)
                .MustAsync((foot, cancellation) => _workingFootRepository.AnyAsync((WorkingFootEnum)foot!.Value))
                .WithName(nameof(PlayerFootballProfileDto.WorkingFoot))
                .WithMessage(Localization.DataValidator);
        });

        When(p => p.Profile.Football.Number.HasValue, () =>
        {
            RuleFor(p => p.Profile.Football.Number)
                .InclusiveBetween(ValidationConstants.PlayerNumberRange.Item1, ValidationConstants.PlayerNumberRange.Item2)
                .WithName(nameof(PlayerFootballProfileDto.Number));
        });

        When(p => p.Profile.Football.GameStyle.HasValue, () =>
        {
            RuleFor(p => p.Profile.Football.GameStyle)
                .MustAsync((style, cancellation) => _gameStyleRepository.AnyAsync((GameStyleEnum)style!.Value))
                .WithName(nameof(PlayerFootballProfileDto.GameStyle))
                .WithMessage(Localization.DataValidator);
        });

        When(p => p.Profile.Football.Skill.HasValue, () =>
        {
            RuleFor(p => p.Profile.Football.Skill)
                .InclusiveBetween(ValidationConstants.RaitingValueRange.Item1, ValidationConstants.RaitingValueRange.Item2)
                .WithName(nameof(PlayerFootballProfileDto.Skill));
        });

        When(p => p.Profile.Football.WeakFoot.HasValue, () =>
        {
            RuleFor(p => p.Profile.Football.WeakFoot)
                .InclusiveBetween(ValidationConstants.RaitingValueRange.Item1, ValidationConstants.RaitingValueRange.Item2)
                .WithName(nameof(PlayerFootballProfileDto.WeakFoot));
        });

        When(p => p.Profile.Football.PhysicalCondition.HasValue, () =>
        {
            RuleFor(p => p.Profile.Football.PhysicalCondition)
                .InclusiveBetween(ValidationConstants.RaitingValueRange.Item1, ValidationConstants.RaitingValueRange.Item2)
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
            .SetValidator(new StatValueValidator(_statTypeRepository));

        RuleForEach(p => p.Stats.Values)
            .Must(stat => ValidationConstants.StatValueRange.Item1 <= stat.Value && stat.Value <= ValidationConstants.StatValueRange.Item2)
            .WithName(nameof(PlayerStatValueDto.Value))
            .WithMessage(Localization.StatValueMustBeInRange.BuildValidationMessage(nameof(BasePlayerDto.Stats), nameof(PlayerStatValueDto.Value),
                ValidationConstants.StatValueRange.Item1, ValidationConstants.StatValueRange.Item2));
    }
}