using MediatR;

namespace WarcraftdleAPI.Application.Characters.Commands.CreateCharacter;

public class CreateCharacterCommand : IRequest<int>
{
    public string Name { get; set; } = null!;
    public string Gender { get; set; } = null!;
    public string Race { get; set; } = null!;
    public string? Class { get; set; }
    public IEnumerable<string> Expansions { get; set; } = null!;
    public IEnumerable<string> Affiliations { get; set; } = null!;
    public IEnumerable<string> Zones { get; set; } = null!;
}
