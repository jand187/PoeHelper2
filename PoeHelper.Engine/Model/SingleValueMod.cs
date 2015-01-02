using System.Diagnostics;

namespace PoeHelper.Engine.Model
{
	[DebuggerDisplay("{ModType} {Value} {Name}")]
	public class SingleValueMod : IItemMod
	{
		public int Value { get; set; }
		public string Name { get; set; }
		public ModTypes ModType { get; set; }
	}
}
