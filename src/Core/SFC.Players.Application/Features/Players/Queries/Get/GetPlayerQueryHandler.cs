using AutoMapper;

using MediatR;

using SFC.Players.Application.Common.Constants;
using SFC.Players.Application.Common.Exceptions;
using SFC.Players.Application.Interfaces.Persistence;
using SFC.Players.Domain.Entities;

namespace SFC.Players.Application.Features.Players.Queries.Get;

public record GetPlayerQueryHandler(IMapper Mapper, IPlayerRepository PlayerRepository)
    : IRequestHandler<GetPlayerQuery, GetPlayerViewModel>
{
    public async Task<GetPlayerViewModel> Handle(GetPlayerQuery request, CancellationToken cancellationToken)
    {
        Player player = await PlayerRepository.GetByIdAsync(request.PlayerId) 
            ?? throw new NotFoundException(Messages.PlayerNotFound);

        return Mapper.Map<GetPlayerViewModel>(player);
    }
}
