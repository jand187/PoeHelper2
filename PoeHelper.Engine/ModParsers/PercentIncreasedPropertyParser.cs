using System;
using System.Linq;
using System.Text.RegularExpressions;
using PoeHelper.Engine.Model;

namespace PoeHelper.Engine.ModParsers
{
	public class PercentIncreasedPropertyParser : IModParser
	{
		private readonly string[] patterns;

		public PercentIncreasedPropertyParser()
		{
			patterns = new[]
			{
				// damage
				@"(?<value>\d+)% (?<name>increased Physical Damage)",
				@"(?<value>\d+)% (?<name>increased Spell Damage)",
				@"(?<value>\d+)% (?<name>increased Elemental Damage with Weapons)",

				// attack
				@"(?<value>\d+)% (?<name>increased Attack Speed)",
				@"(?<value>\d+)% (?<name>increased Accuracy Rating)",
				@"(?<value>\d+)% (?<name>increased Stun Recovery)",
				@"(?<value>\d+)% (?<name>increased Critical Strike Chance)",

				//defense
				@"(?<value>\d+)% (?<name>increased Evasion Rating)",
				@"(?<value>\d+)% (?<name>increased Energy Shield)",
				@"(?<value>\d+)% (?<name>increased Armour)",
				@"(?<value>\d+)% (?<name>increased Armour and Energy Shield)",
				@"(?<value>\d+)% (?<name>increased Armour and Evasion Rating)",
				@"(?<value>\d+)% (?<name>increased Evasion and Energy Shield)",

				// effects
				@"(?<value>\d+)% (?<name>increased Stun Duration on Enemies)",

				// leech and regen
				@"(?<value>\d+)% (?<name>increased Mana Regeneration Rate)",

				// magic find
				@"(?<value>\d+)% (?<name>increased Rarity of Items found)",

				//misc
				@"(?<value>\d+)% (?<name>increased Light Radius)",

				// flasks
				@"(?<value>\d+)% (?<name>reduced Flask Charges used)",
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
