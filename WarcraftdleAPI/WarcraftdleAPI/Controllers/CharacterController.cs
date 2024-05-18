using Microsoft.AspNetCore.Mvc;
using WarcraftdleAPI.Application.Dtos.WowCharacters;
using WarcraftdleAPI.Application.Services;

namespace WarcraftdleAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class CharacterController(WowCharacterService wowCharacterService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<WowCharacterDto>>> Get([FromQuery] string? startsWith)
    {
        var wowCharacters = await wowCharacterService.GetAsync(startsWith);

        return Ok(wowCharacters);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<WowCharacterDto>> GetById(int id)
    {
        var wowCharacter = await wowCharacterService.GetByIdAsync(id);

        return Ok(wowCharacter);
    }

    [HttpGet("random")]
    public async Task<ActionResult<WowCharacterDto>> GetRandom()
    {
        var wowCharacter = await wowCharacterService.GetRandomAsync();

        return Ok(wowCharacter);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddWowCharacterRequest request)
    {
        var wowCharacterId = await wowCharacterService.AddAsync(request);

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