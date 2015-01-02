using System;
using System.Linq;
using System.Text.RegularExpressions;
using PoeHelper.Engine.Model;

namespace PoeHelper.Engine.ModParsers
{
	public class PlusToPropertyParser : IModParser
	{
		private readonly string[] patterns;

		public PlusToPropertyParser()
		{
			patterns = new[]
			{
				@"\+(?<value>\d+) (?<name>to Strength)",
				@"\+(?<value>\d+) (?<name>to Dexterity)",
				@"\+(?<value>\d+) (?<name>to Intelligence)",
				@"\+(?<value>\d+) (?<name>to maximum Energy Shield)",
				@"\+(?<value>\d+) (?<name>to Armour)",
				@"\+(?<value>\d+) (?<name>to Accuracy Rating)",
				@"\+(?<value>\d+) (?<name>to Level of Socketed Gems)",
				@"\+(?<value>\d+) (?<name>to maximum Life)",
				@"\+(?<value>\d+) (?<name>to maximum Mana)"
			};
		}

		public IItemMod Parse(string modText)
		{
			Match match = null;

			foreach (var pattern in patterns)
			{
				match = Regex.Match(modText, pattern);
				if (match.Success)
					break;
			}

			return new SingleValueMod
			{
				Name = match.Groups["name"].Value,
				Value = Convert.ToInt32(match.Groups["value"].Value),
			};
		}

		public bool CanParse(string line)
		{
			return patterns.Select(pattern => Regex.Match(line, pattern)).Any(match => match.Success);
		}
	}
}
