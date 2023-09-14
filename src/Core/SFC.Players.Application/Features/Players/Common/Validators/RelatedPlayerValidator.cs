using FluentValidation;

using SFC.Players.Application.Common.Constants;
using SFC.Players.Application.Features.Players.Common.Models;
using SFC.Players.Application.Interfaces.Persistence;

namespace SFC.Players.Application.Features.Players.Common.Validators;
public class RelatedPlayerValidator : AbstractValidator<IPlayerRelatedRequest>
{
    public RelatedPlayerValidator(IUserRepository userRepository)
    {
        RuleFor(p => p)
           .MustAsync(async (command, cancellation) => await userRepository.AnyAsync(command.PlayerId, command.UserId))
           .WithName(nameof(IPlayerRelatedRequest.PlayerId))
           .WithMessage(Messages.PlayerNotRelatedToThisUser);
    }
}
