using FluentValidation;

using SFC.Player.Application.Common.Constants;
using SFC.Player.Application.Features.Player.Common.Models;
using SFC.Player.Application.Interfaces.Persistence;

namespace SFC.Player.Application.Features.Player.Common.Validators;
public class RelatedPlayerValidator : AbstractValidator<IPlayerRelatedRequest>
{
    public RelatedPlayerValidator(IUserRepository userRepository)
    {
        RuleFor(p => p)
           .MustAsync((command, cancellation) => userRepository.AnyAsync(command.PlayerId, command.UserId))
           .WithName(nameof(IPlayerRelatedRequest.PlayerId))
           .WithMessage(Messages.PlayerNotRelatedToThisUser);
    }
}
