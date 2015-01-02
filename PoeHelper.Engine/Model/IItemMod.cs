namespace PoeHelper.Engine.Model
{
	public interface IItemMod
	{
		string Name { get; set; }
		ModTypes ModType { get; set; }
	}
}
