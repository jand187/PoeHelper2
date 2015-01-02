using PoeHelper.Engine.Model;

namespace PoeHelper.Engine.ModParsers
{
	public interface IModParser
	{
		IItemMod Parse(string modText);
		bool CanParse(string line);
	}
}