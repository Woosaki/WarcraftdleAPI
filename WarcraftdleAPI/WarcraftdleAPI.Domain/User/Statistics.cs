namespace WarcraftdleAPI.Domain.User;

public class Statistics
{
	public int Id { get; set; }
	public int GamesPlayed { get; set; }
	public int GamesWon { get; set; }
	public int CurrentStreak { get; set; }
	public int MaxStreak { get; set; }
}
