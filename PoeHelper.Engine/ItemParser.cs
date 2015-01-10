using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using PoeHelper.Engine.Model;

namespace PoeHelper.Engine
{
	public class ItemParser
	{
		private readonly ModParser modParser;
		private readonly RequirementParser requirementParser;

		public ItemParser()
		{
			modParser = new ModParser();
			requirementParser = new RequirementParser();
		}

		public Item Parse(string itemText)
		{
			var lines = itemText.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);
			var rarity = Regex.Match(lines.First(), @"Rarity: (?<rarity>\w+)").Groups["rarity"].Value;

			var item = new Item
			{
				Rarity = rarity,
				Name = lines[1],
				ItemType = GetItemType(lines, rarity),
				Requirements = requirementParser.Parse(lines).ToList(),
				ItemLevel = GetProperty(lines, "Itemlevel: "),
				Mods = GetMods(lines, rarity),
			};

			return item;
		}

		private int GetProperty(IEnumerable<string> lines, string key)
		{
			return Convert.ToInt32(lines.Single(l => l.StartsWith(key)).Replace(key, string.Empty));
		}

		private IEnumerable<IItemMod> GetMods(string[] lines, string rarity)
		{
			switch (rarity)
			{
				case "Normal":
					return new List<IItemMod>();
				case "Magic":
				case "Rare":
				case "Unique":
					var modStartLine = GetModStart(lines);
					return modParser.Parse(string.Join(Environment.NewLine, lines.Skip(modStartLine))).ToList();

				default:
					Console.WriteLine("Unknown rarity '{0}'", rarity);
					break;
			}

			return new List<IItemMod>();
		}

		private int GetModStart(string[] lines)
		{
			for (var index = 0; index < lines.Length; index++)
			{
				if (lines[index].StartsWith("Itemlevel: "))
					return index + 2;
			}

			return 0;
		}

		private string GetItemType(string[] lines, string rarity)
		{
			switch (rarity)
			{
				case "Normal":
				case "Magic":
					return lines[1];

				case "Rare":
				case "Unique":
					return lines[2];

				default:
					Console.WriteLine("Unknown rarity '{0}'", rarity);
					return "Unknown";
			}
		}
	}
}
