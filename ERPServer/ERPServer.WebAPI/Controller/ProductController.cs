﻿using ERPServer.Application.Features.Products.CreateProduct;
using ERPServer.Application.Features.Products.GetAllProducts;
using ERPServer.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ERPServer.WebAPI.Controller;

public class ProductController : ApiController
{
    public ProductController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> GetAll(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request);
        return StatusCode(response.StatusCode, response);
    }

    //[HttpPost]
    //public async Task<IActionResult> Delete(DeleteDepotCommand request, CancellationToken cancellationToken)
    //{
    //    var response = await _mediator.Send(request);
    //    return StatusCode(response.StatusCode, response);
    //}

    //[HttpPost]
    //public async Task<IActionResult> Update(UpdateDepotCommand request, CancellationToken cancellationToken)
    //{
    //    var response = await _mediator.Send(request);
    //    return StatusCode(response.StatusCode, response);
    //}
}
