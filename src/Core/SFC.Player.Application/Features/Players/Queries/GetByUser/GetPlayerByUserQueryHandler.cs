using AutoMapper;

using MediatR;

using SFC.Player.Application.Interfaces.Persistence;
using SFC.Player.Domain.Entities;

using PlayerEntity = SFC.Player.Domain.Entities.Player;

namespace SFC.Player.Application.Features.Player.Queries.Get;
public record GetPlayerByUserQueryHandler(IMapper Mapper, IPlayerRepository PlayerRepository)
    : IRequestHandler<GetPlayerByUserQuery, GetPlayerByUserViewModel?>
{
    public async Task<GetPlayerByUserViewModel?> Handle(GetPlayerByUserQuery request, CancellationToken cancellationToken)
    {
        PlayerEntity? player = await PlayerRepository.GetByUserIdAsync(request.UserId);

        return Mapper.Map<GetPlayerByUserViewModel?>(player);
    }
}
