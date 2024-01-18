using System.Text.Json.Serialization;

namespace WarcraftdleAPI.Domain.WowCharacter;

public class Expansion
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Abbreviation { get; set; }

    [JsonIgnore]
    public IEnumerable<WowCharacter> WowCharacters { get; set; } = null!;
}