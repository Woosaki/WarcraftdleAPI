using MediatR;
using Microsoft.AspNetCore.Mvc;
using WarcraftdleAPI.Application.Affiliations.Dtos;
using WarcraftdleAPI.Application.Affiliations.Queries.GetAffiliations;
using WarcraftdleAPI.Application.Affiliations.Queries.GetAffiliationById;
using WarcraftdleAPI.Application.Affiliations.Commands.CreateAffiliation;
using WarcraftdleAPI.Application.Affiliations.Commands.DeleteAffiliation;

namespace WarcraftdleAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class AffiliationController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AffiliationDto>>> GetAll()
    {
        var affiliations = await mediator.Send(new GetAffiliationsQuery());

        return Ok(affiliations);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AffiliationDto>> GetById(int id)
    {
        var affiliation = await mediator.Send(new GetAffiliationByIdQuery(id));

        return Ok(affiliation);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateAffiliationCommand command)
    {
        int id = await mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await mediator.Send(new DeleteAffiliationCommand(id));

        return NoContent();
    }
}