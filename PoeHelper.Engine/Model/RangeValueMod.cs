using System.Diagnostics;

namespace PoeHelper.Engine.Model
{
	[DebuggerDisplay("{ModType} {Value}-{Value2} {Name}")]
	public class RangeValueMod : IItemMod
	{
		public int Value { get; set; }
		public int Value2 { get; set; }
		public string Name { get; set; }
		public ModTypes ModType { get; set; }
	}
}
