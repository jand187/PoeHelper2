using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using PoeHelper.Monitor.GroupCondition;

namespace PoeHelper.Monitor.Filters
{
	public class RegexModFilter : IModFilter
	{
		private Regex regex;
		public string Pattern { get; set; }
		public IEnumerable<IGroupCondition> GroupConditions { get; set; }

		public IEnumerable<IModFilterResult> FindMatches(string text)
		{
			regex = new Regex(Pattern);
			var matches = regex.Matches(text).Cast<Match>().ToList();

			foreach (var match in matches)
			{
				if (GroupConditions.All(g => g.IsSatisfiedBy(match)))
					yield return new RegexModFilterResult
					{
						Name = ">39",
						Matches = match.Groups[0].Value
					};
			}
		}

		public string Name { get; set; }
	}
}