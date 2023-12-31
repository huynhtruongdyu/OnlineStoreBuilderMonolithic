﻿using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using OSBM.Admin.API.Controllers.Base;
using OSBM.Admin.Application.Features.Products.Commands;
using OSBM.Admin.Application.Features.Products.Queries;
using OSBM.Admin.Shared.Constants;

namespace OSBM.Admin.API.Controllers.V1;

[ApiVersion("1.0")]
public class ProductsController : BaseApiV1Controller
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    //[POST] api/products
    [HttpPost]
    [MapToApiVersion("1.0")]
    public async Task<IActionResult> Create(CreateProductCommand request)
    {
        var response = await _mediator.Send(request);
        if (response == null)
        {
            return BadResult("create.product.failed");
        }
        return SuccessResult("");
    }

    //[GET] api/products/1
    [HttpGet("{id:long}")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.ADMIN)]
    public async Task<IActionResult> GetById([FromRoute] long id)
    {
        var request = new GetProductByIdQuery(id);
        var response = await _mediator.Send(request);
        if (response == null)
        {
            return BadResult("product.not.found");
        }
        return SuccessResult(response);
    }

    //[GET] api/products
    [HttpGet]
    [MapToApiVersion("1.0")]
    [ResponseCache(Duration = 10, Location = ResponseCacheLocation.Any, NoStore = false)]
    public async Task<IActionResult> GetAll([FromQuery] GetProductsWithPaginationQuery request)
    {
        var response = await _mediator.Send(request);
        return SuccessResult(response);
    }
}