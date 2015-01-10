using System;
using System.IO;
using FluentAssertions;
using PoeHelper.Engine;
using Xunit;

namespace PoeHelper.EngineTests
{
	public class ItemParserTest
	{
		private readonly ItemParser target;

		public ItemParserTest()
		{
			target = new ItemParser();
		}

		[Fact]
		public void Parser_should_parse_rarity()
		{
			var itemText = @"Rarity: Rare
Cataclysm Cloak
Crypt Armour
--------";
Itemlevel: 56";
			var item = target.Parse(itemText);
			item.Rarity.Should().Be("Rare");
		}

		[Fact]
		public void Parse_should_parse_not_throw()
		{
			var files = Directory.GetFiles(Path.Combine(Environment.CurrentDirectory, @"TestData\Items"));

			foreach (var file in files)
			{
				var modText = File.ReadAllText(file);

				var item = target.Parse(modText);

				item.Name.Should().NotBeNullOrEmpty();
				item.Rarity.Should().NotBeNullOrEmpty();
				item.ItemType.Should().NotBeNullOrEmpty();
				item.ItemLevel.Should().NotBe(0);
				item.Mods.Should().NotBeNull();
			}
		}
	}
}
