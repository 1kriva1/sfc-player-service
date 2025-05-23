using AutoMapper;

using MediatR;

using SFC.Player.Application.Common.Constants;
using SFC.Player.Application.Common.Exceptions;
using SFC.Player.Application.Interfaces.Identity;
using SFC.Player.Application.Interfaces.Persistence.Repository.Player;

namespace SFC.Player.Application.Features.Player.Queries.GetByUser;
public class GetPlayerByUserQueryHandler(IMapper mapper, IPlayerRepository playerRepository, IUserService userService)
    : IRequestHandler<GetPlayerByUserQuery, GetPlayerByUserViewModel?>
{
    private readonly IMapper _mapper = mapper;
    private readonly IPlayerRepository _playerRepository = playerRepository;
    private readonly IUserService _userService = userService;

    public async Task<GetPlayerByUserViewModel?> Handle(GetPlayerByUserQuery request, CancellationToken cancellationToken)
    {
        Guid userId = _userService.GetUserId() ?? throw new AuthorizationException(Localization.AuthorizationError);

        PlayerEntity? player = await _playerRepository.GetByUserIdAsync(userId).ConfigureAwait(true);

        return _mapper.Map<GetPlayerByUserViewModel?>(player);
    }
}
