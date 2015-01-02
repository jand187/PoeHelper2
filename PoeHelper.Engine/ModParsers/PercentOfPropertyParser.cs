using System;
using System.Linq;
using System.Text.RegularExpressions;
using PoeHelper.Engine.Model;

namespace PoeHelper.Engine.ModParsers
{
	public class PercentOfPropertyParser : IModParser
	{
		private readonly string[] patterns;

		public PercentOfPropertyParser()
		{
			patterns = new[]
			{
				@"(?<value>\d+)% (?<name>of Physical Attack Damage Leeched as Mana)",
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