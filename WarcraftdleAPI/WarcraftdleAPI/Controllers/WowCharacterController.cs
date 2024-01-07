using Microsoft.AspNetCore.Mvc;
using WarcraftdleAPI.Application.Services;
using WarcraftdleAPI.Domain.WowCharacter;

namespace WarcraftdleAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class WowCharacterController(WowCharacterService wowCharacterService) : ControllerBase
{
	[HttpGet]
	public async Task<ActionResult<IEnumerable<Zone>>> Get()
	{
		var wowCharacters = await wowCharacterService.GetAsync();

		return Ok(wowCharacters);
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<Zone>> GetById(int id)
	{
		var wowCharacter = await wowCharacterService.GetByIdAsync(id);

		return Ok(wowCharacter);
	}

	[HttpPost]
	public async Task<IActionResult> Add([FromBody] string name)
	{
		var wowCharacterId = await wowCharacterService.AddAsync(name);

		var controllerName = ControllerContext.ActionDescriptor.ControllerName;
		var url = $"/{controllerName}/{wowCharacterId}";

		return Created(url, null);
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> Delete(int id)
	{
		await wowCharacterService.DeleteAsync(id);

		return Ok();
	}
}
