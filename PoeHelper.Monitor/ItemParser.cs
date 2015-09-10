using System.Collections.Generic;
using System.Linq;
using PoeHelper.Monitor.Filters;

namespace PoeHelper.Monitor
{
	public interface IItemParser
	{
		IEnumerable<IModFilterResult> Parse(string message);
	}

	public class ItemParser : IItemParser
	{
		private readonly IEnumerable<IModFilter> filters;

		public ItemParser(IEnumerable<IModFilter> filters)
		{
			this.filters = filters;
		}

		public IEnumerable<IModFilterResult> Parse(string message)
		{
			return filters.SelectMany(f => f.FindMatches(message));
		}
	}
}
