using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using WarcraftdleAPI.Application.Services;
using WarcraftdleAPI.Domain.WowCharacter;

namespace WarcraftdleAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class AffiliationController(AffiliationService affiliationService) : ControllerBase
{
	[HttpGet]
	public async Task<ActionResult<IEnumerable<Affiliation>>> Get()
	{
		var affiliations = await affiliationService.GetAsync();

		return Ok(affiliations);
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<Affiliation>> GetById(int id)
	{
		var affiliation = await affiliationService.GetByIdAsync(id);

		return Ok(affiliation);
	}

	[HttpPost]
	public async Task<IActionResult> Add([FromBody] string name)
	{
		var affiliationId = await affiliationService.AddAsync(name);

		var controllerName = ControllerContext.ActionDescriptor.ControllerName;
		var url = $"/{controllerName}/{affiliationId}";

		return Created(url, null);
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> Delete(int id)
	{
		await affiliationService.DeleteAsync(id);

		return Ok();
	}

}
