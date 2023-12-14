﻿namespace WarcraftdleAPI.Domain.WowCharacter;

public class Zone
{
	public int Id { get; set; }
	public string Name { get; set; } = null!;
	public ICollection<WowCharacter> WowCharacters { get; set; } = null!;
}