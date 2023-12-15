namespace WarcraftdleAPI.Domain.Character;

public class Affiliation
{
	public int Id { get; set; }
	public string Name { get; set; } = null!;
	public ICollection<WowCharacter> WowCharacters { get; set; } = null!;
}