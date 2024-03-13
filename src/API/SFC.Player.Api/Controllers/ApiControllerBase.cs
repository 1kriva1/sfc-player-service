using AutoMapper;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using SFC.Player.Application.Interfaces.Identity;

namespace SFC.Player.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
    private ISender? _mediator;

    private IMapper? _mapper;

    private IUserService? _userService;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

    protected IMapper Mapper => _mapper ??= HttpContext.RequestServices.GetRequiredService<IMapper>();

    protected IUserService UserService => _userService ??= HttpContext.RequestServices.GetRequiredService<IUserService>();
}
