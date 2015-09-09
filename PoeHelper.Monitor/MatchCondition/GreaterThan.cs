using System;
using System.Text.RegularExpressions;

namespace PoeHelper.Monitor.MatchCondition
{
	public interface IMatchCondition
	{
		int Group { get; set; }
		object Value { get; set; }
		bool IsSatisfied(Match match);
	}

	public class GreaterThan : IMatchCondition
	{
		public int Group { get; set; }
		public object Value { get; set; }

		public bool IsSatisfied(Match match)
		{
			return match.Groups[Group].IntegerValue() > (int)Value;
		}
	}

	public class HasText : IMatchCondition
	{
		public int Group { get; set; }
		public object Value { get; set; }
		public bool IsSatisfied(Match match)
		{
			return match.Success;
		}
	}

	public static class RegexGroupExtensions
	{
		public static int IntegerValue(this Group @this)
		{
			return Convert.ToInt32(@this.Value);
		}
	}

}
