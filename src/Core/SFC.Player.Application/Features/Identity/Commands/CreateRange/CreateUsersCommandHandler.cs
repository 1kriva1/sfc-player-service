using AutoMapper;

using MediatR;

using SFC.Player.Application.Interfaces.Persistence.Repository.Identity;
using SFC.Player.Domain.Entities.Identity;
using SFC.Player.Domain.Events.Identity;

namespace SFC.Player.Application.Features.Identity.Commands.CreateRange;
public class CreateUsersCommandHandler(IMapper mapper, IMediator mediator, IUserRepository userRepository)
    : IRequestHandler<CreateUsersCommand>
{
    private readonly IMapper _mapper = mapper;
    private readonly IMediator _mediator = mediator;
    private readonly IUserRepository _userRepository = userRepository;

    public async Task Handle(CreateUsersCommand request, CancellationToken cancellationToken)
    {
        IEnumerable<User> users = _mapper.Map<IEnumerable<User>>(request.Users);

        User[] newUsers = await _userRepository.AddRangeIfNotExistsAsync([.. users])
                                               .ConfigureAwait(false);

        UsersCreatedEvent @event = new(newUsers);

        await _mediator.Publish(@event, cancellationToken)
                       .ConfigureAwait(false);
    }
}
