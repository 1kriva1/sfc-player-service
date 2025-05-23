using FluentValidation;

using SFC.Player.Application.Features.Player.Commands.Common.Validators;
using SFC.Player.Application.Interfaces.Common;
using SFC.Player.Application.Interfaces.Persistence.Repository.Data;

namespace SFC.Player.Application.Features.Player.Commands.Update;
public class UpdatePlayerCommandValidator : AbstractValidator<UpdatePlayerCommand>
{
    public UpdatePlayerCommandValidator(
        IDateTimeService dateTimeService,
        IStatTypeRepository statTypeRepository,
        IFootballPositionRepository footballPositionRepository,
        IWorkingFootRepository workingFootRepository,
        IGameStyleRepository gameStyleRepository)
    {
        RuleFor(command => command.Player).SetValidator(new PlayerValidator<UpdatePlayerDto>(
            dateTimeService,
            statTypeRepository,
            footballPositionRepository,
            workingFootRepository,
            gameStyleRepository)
       );
    }
}
