namespace WarcraftdleAPI.Domain.Character;

public class Expansion
{
	public int Id { get; set; }
	public string Name { get; set; } = null!;
	public string? Abbreviation { get; set; }
	public ICollection<WowCharacter> WowCharacters { get; set; } = null!;
}