using ERPServer.Application.Features.Depots.CreateDepot;
using ERPServer.Application.Features.Depots.DeleteDepot;
using ERPServer.Application.Features.Depots.GetAllDepot;
using ERPServer.Application.Features.Depots.UpdateDepot;
using ERPServer.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ERPServer.WebAPI.Controller;

public sealed class DepotController : ApiController
{
    public DepotController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateDepotCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> GetAll(GetAllDepotQuery request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(DeleteDepotCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateDepotCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request);
        return StatusCode(response.StatusCode, response);
    }
}
