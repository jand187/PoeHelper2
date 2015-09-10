using System.Collections.Generic;
using System.Linq;

namespace PoeHelper.Monitor.Filters
{
	public class AndModFilter : IModFilter
	{
		public IEnumerable<IModFilter> ChildFilters { get; set; }
		public string Name { get; set; }

		public IEnumerable<IModFilterResult> FindMatches(string text)
		{
			if (ChildFilters == null)
				return new IModFilterResult[0];

			if (ChildFilters.Any(c => !c.FindMatches(text).Any()))
				return new IModFilterResult[0];

			var childFilterResults = ChildFilters.SelectMany(c => c.FindMatches(text));

			return new IModFilterResult[]
			{
				new AndModFilterResult(childFilterResults)
				{
					Name = this.Name
				}
			};
		}
	}
}