namespace WarcraftdleAPI.Domain.WowCharacter;

public class Expansion
{
	public int Id { get; set; }
	public string Name { get; set; } = null!;
	public string? Abbreviation { get; set; }
	public IEnumerable<WowCharacter> WowCharacters { get; set; } = null!;
}