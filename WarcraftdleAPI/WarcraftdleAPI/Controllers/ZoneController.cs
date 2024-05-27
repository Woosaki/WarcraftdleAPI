using MediatR;
using Microsoft.AspNetCore.Mvc;
using WarcraftdleAPI.Application.Zones.Dtos;
using WarcraftdleAPI.Application.Zones.Queries.GetZones;
using WarcraftdleAPI.Application.Zones.Queries.GetZoneById;
using WarcraftdleAPI.Application.Zones.Commands.CreateZone;
using WarcraftdleAPI.Application.Zones.Commands.DeleteZone;

namespace WarcraftdleAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class ZoneController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ZoneDto>>> GetAll()
    {
        var zones = await mediator.Send(new GetZonesQuery());

        return Ok(zones);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ZoneDto>> GetById(int id)
    {
        var zone = await mediator.Send(new GetZoneByIdQuery(id));

        return Ok(zone);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateZoneCommand command)
    {
        int id = await mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await mediator.Send(new DeleteZoneCommand(id));

        return NoContent();
    }
}