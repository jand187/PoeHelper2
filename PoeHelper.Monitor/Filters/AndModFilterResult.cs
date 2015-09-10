using System.Collections.Generic;
using System.Text;

namespace PoeHelper.Monitor.Filters
{
	public class AndModFilterResult : IModFilterResult
	{
		private readonly IEnumerable<IModFilterResult> childFilterResults;

		public AndModFilterResult(IEnumerable<IModFilterResult> childFilterResults)
		{
			this.childFilterResults = childFilterResults;
		}

		public string Name { get; set; }
		public string Matches { get; set; }

		public override string ToString()
		{
			return new StringBuilder()
				.AppendLine(Name)
				.AppendLine()
				.ToString();
		}
	}
}