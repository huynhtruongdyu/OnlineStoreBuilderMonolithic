﻿using MediatR;

using Microsoft.AspNetCore.Mvc;

using OSBM.Admin.API.Controllers.Base;
using OSBM.Admin.Application.DTOs.Products;
using OSBM.Admin.Application.Features.Products.Commands;
using OSBM.Admin.Application.Features.Products.Queries;

using System.Net;

namespace OSBM.Admin.API.Controllers;

public class ProductsController : BaseApiResponse
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductCommand request)
    {
        var response = await _mediator.Send(request);
        if (response == null)
        {
            return BadResult("create.product.failed");
        }
        return SuccessResult("");
    }

    [HttpGet("{id:long}")]
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
}