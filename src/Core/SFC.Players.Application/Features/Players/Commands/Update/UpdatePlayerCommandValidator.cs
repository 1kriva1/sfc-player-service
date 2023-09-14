using FluentValidation;

using SFC.Players.Application.Features.Players.Commands.Common.Validators;
using SFC.Players.Application.Features.Players.Common.Validators;
using SFC.Players.Application.Interfaces.Common;
using SFC.Players.Application.Interfaces.Persistence;
using SFC.Players.Application.Models.Players.Update;

namespace SFC.Players.Application.Features.Players.Commands.Update;
public class UpdatePlayerCommandValidator : AbstractValidator<UpdatePlayerCommand>
{
    public UpdatePlayerCommandValidator(IDateTimeService dateTimeService, IUserRepository userRepository)
    {
        RuleFor(command => command).SetValidator(new RelatedPlayerValidator(userRepository));
        RuleFor(command => command.Player).SetValidator(new PlayerValidator<UpdatePlayerDto>(dateTimeService));
    }
}
