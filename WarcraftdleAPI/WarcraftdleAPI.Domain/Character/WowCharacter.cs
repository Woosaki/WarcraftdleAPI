namespace WarcraftdleAPI.Domain.Character;

public class WowCharacter
{
	public int Id { get; set; }
	public string Name { get; set; } = null!;
	public string Photo { get; set; } = null!;
	public Gender Gender { get; set; } = null!;
	public Race Race { get; set; } = null!;
	public Class? Class { get; set; }
	public ICollection<Expansion> Expansions { get; set; } = null!;
	public ICollection<Affiliation> Affiliations { get; set; } = null!;
	public ICollection<Zone> Zones { get; set; } = null!;
}
