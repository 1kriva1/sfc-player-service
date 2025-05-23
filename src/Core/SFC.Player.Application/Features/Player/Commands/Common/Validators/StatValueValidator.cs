using FluentValidation;

using SFC.Player.Application.Common.Constants;
using SFC.Player.Application.Common.Extensions;
using SFC.Player.Application.Features.Player.Common.Dto;
using SFC.Player.Application.Interfaces.Persistence.Repository.Data;
using SFC.Player.Domain.Entities.Data;

namespace SFC.Player.Application.Features.Player.Commands.Common.Validators;
public class StatValueValidator : AbstractValidator<IEnumerable<PlayerStatValueDto>>
{
    public StatValueValidator(IStatTypeRepository statTypesRepository)
    {
        RuleFor(stats => stats)
           .MustAsync(async (stats, cancellation) => await statTypesRepository.CountAsync().ConfigureAwait(true) == stats.Count())
           .WithName(nameof(BasePlayerDto.Stats))
           .WithMessage(Localization.StatLength)
           // stat exist validation
           .MustAsync(async (stats, cancellation) =>
           {
               IReadOnlyList<StatType> types = await statTypesRepository.ListAllAsync().ConfigureAwait(true);
               IEnumerable<int> typesUnderValidation = stats.Select(m => m.Type).Order();
               return types.Select(t => (int)t.Id).SequenceEqual(typesUnderValidation.Order());
           })
           .WithName(nameof(PlayerStatValueDto.Type))
           .WithMessage(Localization.MustBeInStatTypeRange.BuildValidationMessage(nameof(BasePlayerDto.Stats), nameof(PlayerStatValueDto.Type)));
    }
}
