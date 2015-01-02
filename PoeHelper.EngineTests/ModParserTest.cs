using System;
using System.IO;
using System.Linq;
using FluentAssertions;
using PoeHelper.Engine;
using PoeHelper.Engine.Model;
using Xunit;
using Xunit.Extensions;

namespace PoeHelper.EngineTests
{
	public class ModParserTest
	{
		private ModParser target;

		public ModParserTest()
		{
			target = new ModParser();
		}

		[Theory]
		[InlineData("+31 to Strength", 31, "to Strength")]
		[InlineData("+3 to Dexterity", 3, "to Dexterity")]
		[InlineData("+31 to Intelligence", 31, "to Intelligence")]
		[InlineData("+14 to maximum Energy Shield", 14, "to maximum Energy Shield")]
		[InlineData("+194 to Armour", 194, "to Armour")]
		[InlineData("+125 to Accuracy Rating", 125, "to Accuracy Rating")]
		[InlineData("+1 to Level of Socketed Gems", 1, "to Level of Socketed Gems")]
		[InlineData("+28 to maximum Life", 28, "to maximum Life")]
		[InlineData("+38 to maximum Mana", 38, "to maximum Mana")]
		public void Parse_should_return_SingleValueMod(string modText, int value, string name)
		{
			var mod = (SingleValueMod) target.Parse(modText).First();

			mod.Name.Should().Be(name);
			mod.Value.Should().Be(value);
		}

		[Fact]
		public void Parse_should_parse_multiline_text()
		{
			var modText = @"
88% increased Physical Damage
Adds 9-16 Physical Damage
Adds 29-48 Cold Damage
8% increased Attack Speed
+305 to Accuracy Rating
24% increased Critical Strike Chance
";

			var mods = target.Parse(modText);
			mods.Should().HaveCount(6);
		}

		[Fact]
		public void Parse_should_parse_not_throw()
		{
			var files = Directory.GetFiles(Path.Combine(Environment.CurrentDirectory, @"TestData\Items"));

			foreach (var file in files)
			{
				var modText = File.ReadAllText(file);
				var startIndex = modText.LastIndexOf("--------", System.StringComparison.Ordinal) + "--------".Length;
				modText = modText.Substring(startIndex);
				target.Parse(modText).ToList();
			}
		}
	}
}
