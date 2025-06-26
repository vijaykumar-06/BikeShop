using MediatR;
using Microsoft.AspNetCore.Mvc;
using BikeShop.Application.DTOs;
using BikeShop.Application.Queries;
using BikeShop.Application.Commands;

namespace BikeShop.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BikesController : ControllerBase
{
    private readonly IMediator _mediator;
    public BikesController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<ActionResult<List<BikeDto>>> GetAll()
        => Ok(await _mediator.Send(new GetAllBikesQuery()));

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] BikeDto dto)
    {
        var id = await _mediator.Send(
            new CreateBikeCommand(dto.Manufacturer, dto.Model, dto.Price)
        );
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BikeDto>> GetById(int id)
    {
        var all = await _mediator.Send(new GetAllBikesQuery());
        var bike = all.Find(b => b.Id == id);
        return bike is null ? NotFound() : Ok(bike);
    }
}
