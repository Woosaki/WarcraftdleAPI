namespace WarcraftdleAPI.Domain.User;

public class User
{
	public int Id { get; set; }
	public string Username { get; set; } = null!;
	public string Email { get; set; } = null!;
	public string Password { get; set; } = null!;
	public DateTime CreatedAt { get; set; }
	public Role Role { get; set; } = null!;
}
