using Microsoft.AspNetCore.Mvc;
using WarcraftdleAPI.Application.Dto;
using WarcraftdleAPI.Application.Interfaces;
using WarcraftdleAPI.Domain.Character;

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

	[HttpPost]
	public async Task<IActionResult> AddAsync(CharacterAddRequest request)
	{
		var characterId = await service.AddAsync(request);

		return Created(characterId.ToString(), null);
	}
}
