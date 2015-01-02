using System;
using System.Linq;
using System.Text.RegularExpressions;
using PoeHelper.Engine.Model;

namespace PoeHelper.Engine.ModParsers
{
	public class PlusPercentToPropertyParser : IModParser
	{
		private readonly string[] patterns;

		public PlusPercentToPropertyParser()
		{
			patterns = new[]
			{
				@"\+(?<value>\d+)% (?<name>to Lightning Resistance)",
				@"\+(?<value>\d+)% (?<name>to Fire Resistance)",
				@"\+(?<value>\d+)% (?<name>to Cold Resistance)",
				@"\+(?<value>\d+)% (?<name>to Chaos Resistance)",
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
			return patterns.Select(pattern => Regex.Match(line, (string) pattern)).Any(match => match.Success);
		}
	}
}