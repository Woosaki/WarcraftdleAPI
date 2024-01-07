using Microsoft.AspNetCore.Mvc;
using WarcraftdleAPI.Application.Services;
using WarcraftdleAPI.Domain.WowCharacter;

namespace WarcraftdleAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class ZoneController(ZoneService zoneService) : ControllerBase
{
	[HttpGet]
	public async Task<ActionResult<IEnumerable<Zone>>> Get()
	{
		var zones = await zoneService.GetAsync();

		return Ok(zones);
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<Zone>> GetById(int id)
	{
		var zone = await zoneService.GetByIdAsync(id);

		return Ok(zone);
	}

	[HttpPost]
	public async Task<IActionResult> Add([FromBody] string name)
	{
		var zoneId = await zoneService.AddAsync(name);

		var controllerName = ControllerContext.ActionDescriptor.ControllerName;
		var url = $"/{controllerName}/{zoneId}";

		return Created(url, null);
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> Delete(int id)
	{
		await zoneService.DeleteAsync(id);

		return Ok();
	}
}
