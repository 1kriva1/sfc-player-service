using AutoMapper;

using MediatR;

using SFC.Player.Application.Common.Constants;
using SFC.Player.Application.Common.Exceptions;
using SFC.Player.Application.Interfaces.Persistence.Repository.Player;
using SFC.Player.Domain.Events.Player;

namespace SFC.Player.Application.Features.Player.Commands.Update;
public class UpdatePlayerCommandHandler(IMapper mapper, IPlayerRepository playerRepository)
    : IRequestHandler<UpdatePlayerCommand>
{
    private readonly IMapper _mapper = mapper;
    private readonly IPlayerRepository _playerRepository = playerRepository;

    public async Task Handle(UpdatePlayerCommand request, CancellationToken cancellationToken)
    {
        PlayerEntity player = await _playerRepository.GetByIdAsync(request.PlayerId).ConfigureAwait(true)
            ?? throw new NotFoundException(Localization.PlayerNotFound);

        PlayerEntity updatedPlayer = _mapper.Map(request.Player, player);

        updatedPlayer.AddDomainEvent(new PlayerUpdatedEvent(updatedPlayer));

        await _playerRepository.UpdateAsync(updatedPlayer).ConfigureAwait(true);
    }
}
