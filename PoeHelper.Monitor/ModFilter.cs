using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using PoeHelper.Monitor.MatchCondition;

namespace PoeHelper.Monitor
{
	public interface IModFilter
	{
		//string Name { get; set; }
		//string Pattern { get; set; }
		string Matches { get; set; }
		//IEnumerable<IMatchCondition> MatchConditions { get; set; }
		bool FindMatches(string text);
		PreferredMod CreateOutput();
	}

	public class ModFilter : IModFilter
	{
		public IEnumerable<IMatchCondition> MatchConditions { get; set; }
		public string Name { get; set; }
		public string Pattern { get; set; }
		public string Matches { get; set; }

		public bool FindMatches(string text)
		{
			var regex = new Regex(Pattern);
			var matches = regex.Matches(text).Cast<Match>().ToList();
			var builder = new StringBuilder();

			foreach (var match in matches)
			{
				if (MatchConditions.All(m => m.IsSatisfied(match)))
				{
					builder.AppendLine(match.Groups[0].Value);
				}
			}

			Matches = builder.ToString();

			return !string.IsNullOrWhiteSpace(Matches);
		}

		public PreferredMod CreateOutput()
		{
			return new PreferredMod
			{
				Header = this.Name,
				Description = this.Matches
			};
		}
	}

	//public class CompositeModFilter : IModFilter
	//{
	//	public CompositeModFilter()
	//	{
	//		MatchConditions = new List<IMatchCondition>();
	//	}

	//	public IEnumerable<IMatchCondition> MatchConditions { get; set; }
	//	public IEnumerable<IModFilter> ChildFilters { get; set; }
	//	public string Name { get; set; }
	//	public string Pattern { get; set; }
	//	public string Matches { get; set; }

	//	public bool FindMatches(string text)
	//	{
	//		var regex = new Regex(Pattern);
	//		var matches = regex.Matches(text).Cast<Match>().ToList();
	//		var builder = new StringBuilder();

	//		foreach (var match in matches)
	//		{
	//			if (MatchConditions.All(m => m.IsSatisfied(match)))
	//			{
	//				builder.AppendLine(match.Groups[0].Value);
	//			}
	//		}

	//		foreach (var filter in ChildFilters)
	//		{
	//			if (filter.FindMatches(text))
	//				builder.Append(filter.Matches);
	//		}

	//		Matches = builder.ToString();

	//		return !string.IsNullOrWhiteSpace(Matches);
	//	}

	//	public PreferredMod CreateOutput()
	//	{
	//		return new PreferredMod
	//		{
	//			Header = this.Name,
	//			Description = this.Matches
	//		};
	//	}
	//}

	public class AndModFilter : IModFilter
	{
		public IEnumerable<IModFilter> ChildFilters { get; set; }
		public string Name { get; set; }
		public string Pattern { get; set; }
		public string Matches { get; set; }

		public bool FindMatches(string text)
		{
			if (ChildFilters.All(f => f.FindMatches(text)))
			{
				Matches = ChildFilters.Aggregate(string.Empty, (s, filter) => s + filter.Matches);
				return true;
			}

			return false;
		}

		public PreferredMod CreateOutput()
		{
			return new PreferredMod
			{
				Header = this.Name,
				Description = this.Matches
			};
		}
	}
}
