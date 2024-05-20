using WarcraftdleAPI.Domain.Enums;

namespace WarcraftdleAPI.Domain.Entities;

public class Character
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public Gender Gender { get; set; }
    public Race Race { get; set; }
    public Class? Class { get; set; }
    public IEnumerable<Expansion> Expansions { get; set; } = null!;
    public IEnumerable<Affiliation> Affiliations { get; set; } = null!;
    public IEnumerable<Zone> Zones { get; set; } = null!;
}