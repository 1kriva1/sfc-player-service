using AutoMapper;

using MediatR;

using SFC.Player.Application.Common.Constants;
using SFC.Player.Application.Common.Exceptions;
using SFC.Player.Application.Common.Extensions;
using SFC.Player.Application.Interfaces.Persistence;
using SFC.Player.Domain.Entities;
using SFC.Player.Domain.Entities.Data;

using PlayerEntity = SFC.Player.Domain.Entities.Player;

namespace SFC.Player.Application.Features.Players.Commands.Update;
public record UpdatePlayerCommandHandler(IMapper Mapper, IPlayerRepository PlayerRepository)
    : IRequestHandler<UpdatePlayerCommand>
{
    public async Task Handle(UpdatePlayerCommand command, CancellationToken cancellationToken)
    {
        PlayerEntity player = await PlayerRepository.GetByIdAsync(command.PlayerId)
            ?? throw new NotFoundException(Messages.PlayerNotFound);

        List<StatType> statTypes = new();

        PlayerEntity updatedPlayer = Mapper.Map(command.Player, player,
                                                opt => opt.BeforeMap((updated, existing) => statTypes.AddRange(existing.Stats.Select(s => s.Type))))
                                           .SetStatTypes(statTypes);

        await PlayerRepository.UpdateAsync(updatedPlayer);
    }
}
