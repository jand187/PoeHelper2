using System;
using System.Linq;
using System.Text.RegularExpressions;
using PoeHelper.Engine.Model;

namespace PoeHelper.Engine.ModParsers
{
	public class AddsRangeToPropertyParser : IModParser
	{
		private readonly string[] patterns;

		public AddsRangeToPropertyParser()
		{
			patterns = new[]
			{
				@"Adds (?<value>\d+)-(?<value2>\d+) (?<name>Physical Damage)",
				@"Adds (?<value>\d+)-(?<value2>\d+) (?<name>Cold Damage)",
				@"Adds (?<value>\d+)-(?<value2>\d+) (?<name>Fire Damage)",
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

			return new RangeValueMod
			{
				Name = match.Groups["name"].Value,
				Value = Convert.ToInt32(match.Groups["value"].Value),
				Value2 = Convert.ToInt32(match.Groups["value2"].Value),
			};
		}

		public bool CanParse(string line)
		{
			return patterns.Select(pattern => Regex.Match(line, pattern)).Any(match => match.Success);
		}
	}
}
