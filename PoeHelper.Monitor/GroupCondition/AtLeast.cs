using System;
using System.Text.RegularExpressions;

namespace PoeHelper.Monitor.GroupCondition
{
	public class AtLeast : IGroupCondition
	{
		public int Group { get; set; }
		public int Value { get; set; }

		public bool IsSatisfiedBy(Match match)
		{
			return Convert.ToInt32(match.Groups[Group].Value) >= Value;
		}
	}

	public class NoCondition : IGroupCondition
	{
		public int Group { get; set; }
		public int Value { get; set; }
		public bool IsSatisfiedBy(Match match)
		{
			return match.Success;
		}
	}
}