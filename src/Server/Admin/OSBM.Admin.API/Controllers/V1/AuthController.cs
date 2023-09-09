using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using OSBM.Admin.API.Controllers.Base;
using OSBM.Admin.Application.Features.Auth.Commands;

namespace OSBM.Admin.API.Controllers.V1;

[AllowAnonymous]
public class AuthController : BaseApiV1Controller
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterCommand request)
    {
        var response = await _mediator.Send(request);
        if (response == null)
        {
            return BadResult("register.failed");
        }
        return SuccessResult(response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginCommand request)
    {
        var response = await _mediator.Send(request);
        return SuccessResult(response);
    }
}