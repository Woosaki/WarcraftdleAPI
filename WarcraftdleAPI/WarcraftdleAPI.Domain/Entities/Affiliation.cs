using System.Text.Json.Serialization;

namespace WarcraftdleAPI.Domain.Entities;

public class Affiliation
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    [JsonIgnore]
    public IEnumerable<Character> Characters { get; set; } = null!;
}