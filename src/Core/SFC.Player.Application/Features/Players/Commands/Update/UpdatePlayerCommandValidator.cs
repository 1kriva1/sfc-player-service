using FluentValidation;

using SFC.Player.Application.Features.Players.Commands.Common.Validators;
using SFC.Player.Application.Features.Players.Common.Validators;
using SFC.Player.Application.Interfaces.Common;
using SFC.Player.Application.Interfaces.Persistence;
using SFC.Player.Domain.Entities.Data;

namespace SFC.Player.Application.Features.Players.Commands.Update;
public class UpdatePlayerCommandValidator : AbstractValidator<UpdatePlayerCommand>
{
    public UpdatePlayerCommandValidator(
        IDateTimeService dateTimeService,
        IUserRepository userRepository,
        IStatTypeRepository statTypeRepository,
        IDataRepository<FootballPosition> footballPositionRepository,
        IDataRepository<WorkingFoot> workingFootRepository,
        IDataRepository<GameStyle> gameStyleRepository)
    {
        RuleFor(command => command).SetValidator(new RelatedPlayerValidator(userRepository));
        RuleFor(command => command.Player).SetValidator(new PlayerValidator<UpdatePlayerDto>(
            dateTimeService,
            statTypeRepository,
            footballPositionRepository,
            workingFootRepository,
            gameStyleRepository)
       );
    }
}
