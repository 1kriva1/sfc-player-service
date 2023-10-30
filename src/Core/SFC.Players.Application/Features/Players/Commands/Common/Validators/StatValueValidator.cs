using FluentValidation;

using SFC.Players.Application.Common.Constants;
using SFC.Players.Application.Interfaces.Persistence;
using SFC.Players.Application.Models.Players.Common;
using SFC.Players.Domain.Entities.Data;

namespace SFC.Players.Application.Features.Players.Commands.Common.Validators;
public class StatValueValidator : AbstractValidator<IEnumerable<PlayerStatValueDto>>
{
    public StatValueValidator(IStatTypeRepository statTypesRepository, IStatCategoryRepository statCategoryRepository)
    {
        RuleFor(stats => stats)
           .MustAsync(async (stats, cancellation) => await statTypesRepository.CountAsync() == stats.Count())
           .WithName(nameof(BasePlayerDto.Stats))
           .WithMessage(Messages.StatLength)
           // category exist validation
           .MustAsync(async (stats, cancellation) =>
           {
               IEnumerable<int> categories = stats.Select(m => m.Category).Distinct();
               return await statCategoryRepository.CountAsync(categories) == categories.Count();
           })
           .WithName(nameof(PlayerStatValueDto.Category))
           .WithMessage(string.Format(Messages.MustBeInCategoryRange, nameof(BasePlayerDto.Stats), nameof(PlayerStatValueDto.Category)))
           // stat exist validation
           .MustAsync(async (stats, cancellation) =>
           {
               IEnumerable<int> types = stats.Select(m => m.Type);
               return await statTypesRepository.CountAsync(types) == types.Count();
           })
           .WithName(nameof(PlayerStatValueDto.Type))
           .WithMessage(string.Format(Messages.MustBeInStatTypeRange, nameof(BasePlayerDto.Stats), nameof(PlayerStatValueDto.Type)))
           // stat has valid category validation
           .MustAsync(async (stats, cancellation) =>
           {
               IReadOnlyList<StatType> types = await statTypesRepository.ListAllAsync();
               return stats.All(stat => types.Any(t => t.CategoryId == stat.Category && t.Id == stat.Type));
           })
           .WithMessage(string.Format(Messages.EachStatTypeMustBeInSpecificCategory, nameof(BasePlayerDto.Stats), nameof(PlayerStatValueDto.Type), nameof(PlayerStatValueDto.Category)));
    }
}
