using Microsoft.EntityFrameworkCore;

namespace WarcraftdleAPI.Application.Validators;

public static class ValidationHelpers
{
	public static bool BeValidName(string name)
	{
		if (string.IsNullOrWhiteSpace(name))
		{
			return false;
		}

		var words = name.Split(' ');

		foreach (var word in words)
		{
			if (!char.IsUpper(word[0]) || !word[1..].All(char.IsLower))
			{
				return false;
			}
		}

		return true;
	}
}
