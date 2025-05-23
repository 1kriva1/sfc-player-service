using FluentValidation;

using SFC.Player.Application.Common.Constants;
using SFC.Player.Application.Features.Player.Commands.Common.Validators;
using SFC.Player.Application.Interfaces.Common;
using SFC.Player.Application.Interfaces.Identity;
using SFC.Player.Application.Interfaces.Persistence.Repository.Data;
using SFC.Player.Application.Interfaces.Persistence.Repository.Player;

namespace SFC.Player.Application.Features.Player.Commands.Create;
public class CreatePlayerCommandValidator : AbstractValidator<CreatePlayerCommand>
{
    public CreatePlayerCommandValidator(
        IDateTimeService dateTimeService,
        IPlayerRepository playerRepository,
        IStatTypeRepository statTypeRepository,
        IFootballPositionRepository footballPositionRepository,
        IWorkingFootRepository workingFootRepository,
        IGameStyleRepository gameStyleRepository,
        IUserService userService)
    {
        Guid? userId = userService.GetUserId();

        When(p => userId.HasValue, () => RuleFor(command => command).MustAsync(async (command, cancellation) => !await playerRepository.AnyAsync(userId!.Value).ConfigureAwait(true))
                                                                    .WithName(nameof(CreatePlayerCommand.Player))
                                                                    .WithMessage(Localization.PlayerAlreadyCreatedForThisUser));

        RuleFor(command => command.Player).SetValidator(new PlayerValidator<CreatePlayerDto>(
            dateTimeService,
            statTypeRepository,
            footballPositionRepository,
            workingFootRepository,
            gameStyleRepository));
    }
}
