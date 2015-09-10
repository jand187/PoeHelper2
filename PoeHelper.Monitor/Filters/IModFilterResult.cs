namespace PoeHelper.Monitor.Filters
{
	public interface IModFilterResult
	{
		string Name { get; set; }
		string Matches { get; set; }
	}
}