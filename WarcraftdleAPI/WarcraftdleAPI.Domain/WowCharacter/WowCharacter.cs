namespace WarcraftdleAPI.Domain.WowCharacter;

public class WowCharacter
{
	public int Id { get; set; }
	public string Name { get; set; } = null!;
	public string Photo { get; set; } = null!;
	public Gender Gender { get; set; } = null!;
	public Race Race { get; set; } = null!;
	public Class? Class { get; set; }
	public IEnumerable<Expansion> Expansions { get; set; } = null!;
	public IEnumerable<Affiliation> Affiliations { get; set; } = null!;
	public IEnumerable<Zone> Zones { get; set; } = null!;
}
