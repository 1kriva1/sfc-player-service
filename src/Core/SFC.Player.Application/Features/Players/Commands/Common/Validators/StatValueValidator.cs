using FluentValidation;

using SFC.Player.Application.Common.Constants;
using SFC.Player.Application.Features.Players.Common.Dto;
using SFC.Player.Application.Interfaces.Persistence;
using SFC.Player.Domain.Entities.Data;

namespace SFC.Player.Application.Features.Players.Commands.Common.Validators;
public class StatValueValidator : AbstractValidator<IEnumerable<PlayerStatValueDto>>
{
    public StatValueValidator(IStatTypeRepository statTypesRepository)
    {
        RuleFor(stats => stats)
           .MustAsync(async (stats, cancellation) => await statTypesRepository.CountAsync() == stats.Count())
           .WithName(nameof(BasePlayerDto.Stats))
           .WithMessage(Messages.StatLength)
           // stat exist validation
           .MustAsync(async (stats, cancellation) =>
           {
               IReadOnlyList<StatType> types = await statTypesRepository.ListAllAsync();
               IEnumerable<int> typesUnderValidation = stats.Select(m => m.Type).Order();
               return types.Select(t => t.Id).SequenceEqual(typesUnderValidation.Order());
           })
           .WithName(nameof(PlayerStatValueDto.Type))
           .WithMessage(string.Format(Messages.MustBeInStatTypeRange, nameof(BasePlayerDto.Stats), nameof(PlayerStatValueDto.Type)));
    }
}
