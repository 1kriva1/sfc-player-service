using FluentValidation;

using SFC.Player.Application.Common.Constants;
using SFC.Player.Application.Features.Player.Commands.Common.Validators;
using SFC.Player.Application.Interfaces.Common;
using SFC.Player.Application.Interfaces.Persistence;
using SFC.Player.Domain.Entities.Data;

namespace SFC.Player.Application.Features.Player.Commands.Create;
public class CreatePlayerCommandValidator : AbstractValidator<CreatePlayerCommand>
{
    public CreatePlayerCommandValidator(
        IDateTimeService dateTimeService,
        IUserRepository userRepository,
        IStatTypeRepository statTypeRepository,
        IDataRepository<FootballPosition> footballPositionRepository,
        IDataRepository<WorkingFoot> workingFootRepository,
        IDataRepository<GameStyle> gameStyleRepository)
    {
        RuleFor(command => command).MustAsync(async (command, cancellation) => !await userRepository.AnyAsync(command.UserId))
                                   .WithName(nameof(CreatePlayerCommand.Player))
                                   .WithMessage(Messages.PlayerAlreadyCreatedForThisUser);

        RuleFor(command => command.Player).SetValidator(new PlayerValidator<CreatePlayerDto>(
            dateTimeService,
            statTypeRepository,
            footballPositionRepository,
            workingFootRepository,
            gameStyleRepository));
    }
}
