using MediatR;

using Microsoft.AspNetCore.Mvc;

using OSBM.Admin.Application.DTOs.Products;
using OSBM.Admin.Application.Features.Products.Commands;
using OSBM.Admin.Application.Features.Products.Queries;

using System.Net;

namespace OSBM.Admin.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ProductDto), 200)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult> Create(CreateProductCommand request)
    {
        var response = await _mediator.Send(request);
        if (response == null)
        {
            return BadRequest("create.product.failed");
        }
        return Ok(response);
    }

    [HttpGet("{id:long}")]
    [ProducesResponseType(typeof(ProductDto), 200)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetById([FromRoute] long id)
    {
        var request = new GetProductByIdQuery(id);
        var response = await _mediator.Send(request);
        if (response == null)
        {
            return NotFound("product.not.found");
        }
        return Ok(response);
    }
}