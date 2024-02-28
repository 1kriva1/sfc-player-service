using FluentValidation;

using SFC.Players.Application.Common.Constants;
using SFC.Players.Application.Features.Players.Commands.Common.Validators;
using SFC.Players.Application.Interfaces.Common;
using SFC.Players.Application.Interfaces.Persistence;
using SFC.Players.Domain.Entities.Data;

namespace SFC.Players.Application.Features.Players.Commands.Create;
public class CreatePlayerCommandValidator : AbstractValidator<CreatePlayerCommand>
{
    public CreatePlayerCommandValidator(
        IDateTimeService dateTimeService,
        IUserRepository userRepository,
        IStatCategoryRepository statCategoryRepository,
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
            statCategoryRepository,
            statTypeRepository,
            footballPositionRepository,
            workingFootRepository,
            gameStyleRepository));
    }
}
