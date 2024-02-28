using AutoMapper;

using MediatR;

using SFC.Players.Application.Common.Constants;
using SFC.Players.Application.Common.Exceptions;
using SFC.Players.Application.Common.Extensions;
using SFC.Players.Application.Interfaces.Persistence;
using SFC.Players.Domain.Entities;
using SFC.Players.Domain.Entities.Data;

namespace SFC.Players.Application.Features.Players.Commands.Update;

public record UpdatePlayerCommandHandler(IMapper Mapper, IPlayerRepository PlayerRepository)
    : IRequestHandler<UpdatePlayerCommand>
{
    public async Task Handle(UpdatePlayerCommand command, CancellationToken cancellationToken)
    {
        Player player = await PlayerRepository.GetByIdAsync(command.PlayerId)
            ?? throw new NotFoundException(Messages.PlayerNotFound);

        List<StatType> statTypes = new();

        Player updatedPlayer = Mapper.Map(command.Player, player, opt => opt.BeforeMap((updated, existing) => statTypes.AddRange(existing.Stats.Select(s => s.Type))))
                                     .SetStatTypes(statTypes);

        await PlayerRepository.UpdateAsync(updatedPlayer);
    }
}
