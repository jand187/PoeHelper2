using System.Collections.Generic;

namespace PoeHelper.Monitor.Filters
{
	public interface IModFilter
	{
		string Name { get; set; }
		IEnumerable<IModFilterResult> FindMatches(string text);
	}
}