using FluentValidation;

using SFC.Players.Application.Features.Players.Common.Validators;
using SFC.Players.Application.Features.Players.Queries.Get;
using SFC.Players.Application.Interfaces.Persistence;

namespace SFC.Players.Application.Features.Players.Commands.Update;
public class GetPlayerQueryValidator : AbstractValidator<GetPlayerQuery>
{
    public GetPlayerQueryValidator(IUserRepository userRepository)
    {
        RuleFor(command => command).SetValidator(new RelatedPlayerValidator(userRepository));
    }
}
