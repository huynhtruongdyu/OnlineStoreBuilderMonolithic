using MediatR;

using Microsoft.AspNetCore.Mvc;

using OSBM.Admin.API.Controllers.Base;
using OSBM.Admin.Application.Features.Products.Commands;
using OSBM.Admin.Application.Features.Products.Queries;

namespace OSBM.Admin.API.Controllers.V2;

[ApiVersion("2.0")]
public class TestsController : BaseApiV1Controller
{
    private readonly IMediator _mediator;

    public TestsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    //[POST] api/products
    [HttpGet("ping")]
    [MapToApiVersion("2.0")]
    public IActionResult Ping()
    {
        return SuccessResult("pong");
    }
}