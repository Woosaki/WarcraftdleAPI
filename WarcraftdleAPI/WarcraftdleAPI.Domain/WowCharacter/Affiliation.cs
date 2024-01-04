namespace WarcraftdleAPI.Domain.WowCharacter;

public class Affiliation
{
	public int Id { get; set; }
	public string Name { get; set; } = null!;
	public IEnumerable<WowCharacter> WowCharacters { get; set; } = null!;
}