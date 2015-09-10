using System.Text;

namespace PoeHelper.Monitor.Filters
{
	public class RegexModFilterResult : IModFilterResult
	{
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