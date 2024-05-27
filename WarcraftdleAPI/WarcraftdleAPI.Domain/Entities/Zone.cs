namespace WarcraftdleAPI.Domain.Entities;

public class Zone
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public IEnumerable<Character> Characters { get; set; } = null!;
}