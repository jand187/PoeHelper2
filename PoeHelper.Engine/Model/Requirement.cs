using System.Diagnostics;

namespace PoeHelper.Engine
{
	[DebuggerDisplay("{Name} {Value}")]
	public class Requirement
	{
		public string Name { get; set; }
		public int Value { get; set; }
	}
}