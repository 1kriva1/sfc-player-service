using AutoMapper;

using MediatR;

using SFC.Player.Application.Interfaces.Persistence.Repository.Identity;
using SFC.Player.Domain.Entities.Identity;

namespace SFC.Player.Application.Features.Identity.Commands.Create;
public class CreateUserCommandHandler(IMapper mapper, IUserRepository identityUserRepository)
    : IRequestHandler<CreateUserCommand>
{
    private readonly IMapper _mapper = mapper;
    private readonly IUserRepository _identityUserRepository = identityUserRepository;

    public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        User user = _mapper.Map<User>(request.User);

        await _identityUserRepository.AddAsync(user)
                                     .ConfigureAwait(false);
    }
}
