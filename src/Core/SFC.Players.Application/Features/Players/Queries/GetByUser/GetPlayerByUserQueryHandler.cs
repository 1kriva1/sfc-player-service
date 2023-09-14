using AutoMapper;

using MediatR;

using SFC.Players.Application.Interfaces.Persistence;
using SFC.Players.Domain.Entities;

namespace SFC.Players.Application.Features.Players.Queries.Get;

public record GetPlayerByUserQueryHandler(IMapper Mapper, IPlayerRepository PlayerRepository)
    : IRequestHandler<GetPlayerByUserQuery, GetPlayerByUserViewModel?>
{
    public async Task<GetPlayerByUserViewModel?> Handle(GetPlayerByUserQuery request, CancellationToken cancellationToken)
    {
        Player? player = await PlayerRepository.GetByUserIdAsync(request.UserId);

        return Mapper.Map<GetPlayerByUserViewModel?>(player);
    }
}
