using MediatR;
using Microsoft.AspNetCore.Mvc;
using WarcraftdleAPI.Application.Characters.Dtos;
using WarcraftdleAPI.Application.Characters.Queries.GetCharacters;
using WarcraftdleAPI.Application.Characters.Queries.GetCharacterById;
using WarcraftdleAPI.Application.Characters.Commands.CreateCharacter;
using WarcraftdleAPI.Application.Characters.Commands.DeleteCharacter;

namespace WarcraftdleAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class CharacterController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CharacterDto>>> Get([FromQuery] string? startsWith)
    {
        var characters = await mediator.Send(new GetCharactersQuery(startsWith));


        return Ok(characters);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CharacterDto>> GetById(int id)
    {
        var character = await mediator.Send(new GetCharacterByIdQuery(id));

        return Ok(character);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCharacterCommand command)
    {
        int id = await mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await mediator.Send(new DeleteCharacterCommand(id));

        return NoContent();
    }
}