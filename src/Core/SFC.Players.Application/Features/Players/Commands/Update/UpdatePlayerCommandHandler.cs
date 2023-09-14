using AutoMapper;

using MediatR;

using SFC.Players.Application.Common.Constants;
using SFC.Players.Application.Common.Exceptions;
using SFC.Players.Application.Interfaces.Persistence;
using SFC.Players.Domain.Entities;

namespace SFC.Players.Application.Features.Players.Commands.Update;

public record UpdatePlayerCommandHandler(IMapper Mapper, IPlayerRepository PlayerRepository)
    : IRequestHandler<UpdatePlayerCommand>
{
    public async Task Handle(UpdatePlayerCommand command, CancellationToken cancellationToken)
    {
        Player player = await PlayerRepository.GetByIdAsync(command.PlayerId)
            ?? throw new NotFoundException(Messages.PlayerNotFound);

        Player updatedPlayer = Mapper.Map(command.Player, player);

        await PlayerRepository.UpdateAsync(updatedPlayer);
    }
}
