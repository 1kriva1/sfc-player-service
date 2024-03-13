using FluentValidation;

using SFC.Player.Application.Features.Player.Common.Validators;
using SFC.Player.Application.Features.Player.Queries.Get;
using SFC.Player.Application.Interfaces.Persistence;

namespace SFC.Player.Application.Features.Player.Commands.Update;
public class GetPlayerQueryValidator : AbstractValidator<GetPlayerQuery>
{
    public GetPlayerQueryValidator(IUserRepository userRepository)
    {
        RuleFor(command => command).SetValidator(new RelatedPlayerValidator(userRepository));
    }
}
