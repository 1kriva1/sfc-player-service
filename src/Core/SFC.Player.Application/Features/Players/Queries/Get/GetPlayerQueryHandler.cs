using AutoMapper;

using MediatR;

using SFC.Player.Application.Common.Constants;
using SFC.Player.Application.Common.Exceptions;
using SFC.Player.Application.Interfaces.Persistence;
using SFC.Player.Domain.Entities;

using PlayerEntity = SFC.Player.Domain.Entities.Player;

namespace SFC.Player.Application.Features.Players.Queries.Get;

public record GetPlayerQueryHandler(IMapper Mapper, IPlayerRepository PlayerRepository)
    : IRequestHandler<GetPlayerQuery, GetPlayerViewModel>
{
    public async Task<GetPlayerViewModel> Handle(GetPlayerQuery request, CancellationToken cancellationToken)
    {
        PlayerEntity player = await PlayerRepository.GetByIdAsync(request.PlayerId) 
            ?? throw new NotFoundException(Messages.PlayerNotFound);

        return Mapper.Map<GetPlayerViewModel>(player);
    }
}
