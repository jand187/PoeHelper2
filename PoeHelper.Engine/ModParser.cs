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
				IModParser parser = null; 
				try
				{
					parser = modParsers.Single(m => m.CanParse(line));
				}
				catch (InvalidOperationException e)
				{
					Console.WriteLine("Could not find parser for line '{0}'", line);
				}

				yield return parser.Parse(line);
			}
		}
	}
}
