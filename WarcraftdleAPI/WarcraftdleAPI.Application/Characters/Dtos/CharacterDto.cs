using WarcraftdleAPI.Domain.Enums;

namespace WarcraftdleAPI.Application.Characters.Dtos;

public class CharacterDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public Gender Gender { get; set; }
    public Race Race { get; set; }
    public Class? Class { get; set; }
    public IEnumerable<Expansion> Expansions { get; set; } = null!;
    public IEnumerable<string> Affiliations { get; set; } = null!;
    public IEnumerable<string> Zones { get; set; } = null!;
}
