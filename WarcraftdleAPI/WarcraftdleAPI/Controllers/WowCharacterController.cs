using Microsoft.AspNetCore.Mvc;
using WarcraftdleAPI.Domain.Character;
using WarcraftdleAPI.Domain.Interfaces;

namespace WarcraftdleAPI.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class WowCharacterController(IWowCharacterService service) : ControllerBase
{
	[HttpGet]
	public async Task<ActionResult<IEnumerable<WowCharacter>>> GetAsync()
	{
		var characters = await service.GetAsync();

		return Ok(characters);
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<WowCharacter>> GetByIdAsync(int id)
	{
		var character = await service.GetByIdAsync(id);

		return Ok(character);
	}
}
