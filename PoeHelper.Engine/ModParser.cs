using System;
using System.Collections.Generic;
using System.Linq;
using PoeHelper.Engine.Model;
using PoeHelper.Engine.ModParsers;

namespace PoeHelper.Engine
{
	public class ModParser
	{
		private readonly IEnumerable<IModParser> modParsers;
		private IItemMod lastMod;
		private bool implicitFound;

		public ModParser()
		{
			modParsers = new List<IModParser>
			{
				new PlusToPropertyParser(),
				new PercentIncreasedPropertyParser(),
				new AddsRangeToPropertyParser(),
				new PlusPercentToPropertyParser(),
				new PercentOfPropertyParser(),
				new ReflectPropertyParser(),
			};
		}

		public IEnumerable<IItemMod> Parse(string modText)
		{
			var lines = modText.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);
			foreach (var line in lines)
			{
				if (line == "--------" && lastMod != null && !implicitFound)
				{
					lastMod.ModType = ModTypes.Implicit;
					implicitFound = true;
					continue;
				}

				IModParser parser = null;
				try
				{
					parser = modParsers.Single(m => m.CanParse(line));
				}
				catch (InvalidOperationException e)
				{
					Console.WriteLine("ModParser could not find parser for line '{0}'", line);
					continue;
				}

				lastMod = parser.Parse(line);
				lastMod.ModType = ModTypes.Explicit;
				yield return lastMod;
			}
		}
	}

	public enum ModTypes
	{
		Unknown = 0,
		Implicit = 1,
		Explicit = 2,
	}
}
