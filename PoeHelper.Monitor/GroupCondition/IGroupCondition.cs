using System.Text.RegularExpressions;

namespace PoeHelper.Monitor.GroupCondition
{
	public interface IGroupCondition
	{
		int Group { get; set; }
		int Value { get; set; }
		bool IsSatisfiedBy(Match match);
	}
}