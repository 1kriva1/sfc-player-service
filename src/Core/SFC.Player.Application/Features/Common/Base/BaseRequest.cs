using MediatR;

using Microsoft.Extensions.Logging;

using SFC.Player.Application.Common.Enums;

namespace SFC.Player.Application.Features.Common.Base;

public abstract class BaseRequest
{
    public abstract RequestId RequestId { get; }

    public EventId EventId => new((int)RequestId, Enum.GetName(RequestId));
}

public abstract class Request : BaseRequest, IRequest { }

public abstract class Request<TResponse> : BaseRequest, IRequest<TResponse> { }

