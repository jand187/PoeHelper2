using System.Collections.Generic;
using System.Diagnostics;

namespace PoeHelper.Engine.Model
{
	[DebuggerDisplay("{Rarity} {ItemType} {Name}")]
	public class Item
	{
		public string Rarity { get; set; }
		public string Name { get; set; }
		public string ItemType { get; set; }
		public IEnumerable<IItemMod> Mods { get; set; }
	}
}
