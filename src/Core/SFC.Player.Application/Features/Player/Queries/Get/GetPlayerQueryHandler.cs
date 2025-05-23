using AutoMapper;

using MediatR;

using SFC.Player.Application.Common.Constants;
using SFC.Player.Application.Common.Exceptions;
using SFC.Player.Application.Interfaces.Persistence.Repository.Player;

namespace SFC.Player.Application.Features.Player.Queries.Get;

public record GetPlayerQueryHandler(IMapper Mapper, IPlayerRepository PlayerRepository)
    : IRequestHandler<GetPlayerQuery, GetPlayerViewModel>
{
    public async Task<GetPlayerViewModel> Handle(GetPlayerQuery request, CancellationToken cancellationToken)
    {
        PlayerEntity player = await PlayerRepository.GetByIdAsync(request.PlayerId).ConfigureAwait(true)
            ?? throw new NotFoundException(Localization.PlayerNotFound);

        return Mapper.Map<GetPlayerViewModel>(player);
    }
}
