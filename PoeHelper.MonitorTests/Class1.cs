using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using PoeHelper.Monitor;
using PoeHelper.Monitor.Filters;
using PoeHelper.Monitor.GroupCondition;
using Xunit;

namespace ClassLibrary1
{
	public class Class1
	{
		[Fact]
		public void FirstTest()
		{
			var modFilters = new List<IModFilter>
			{
				new AndModFilter
				{
					Name = "Amuler Spell Damage",
					ChildFilters = new List<IModFilter>
					{
						new RegexModFilter
						{
							Name = "Amulet",
							Pattern = @"Amulet",
							GroupConditions = new List<IGroupCondition>
							{
								new AtLeast
								{
									Group = 1,
									Value = 12
								}
							}
						},
						new RegexModFilter
						{
							Name = "gnu: >=24",
							Pattern = @"(\d+)% increased Spell Damage",
							GroupConditions = new List<IGroupCondition>
							{
								new AtLeast
								{
									Group = 1,
									Value = 12
								}
							}
						}
					}
				}
			};

			var binder = new TypedSerializationBinder("PoeHelper.MonitorTests.{0}, PoeHelper.MonitorTests");
			var json = JsonConvert.SerializeObject(modFilters, Formatting.Indented, new JsonSerializerSettings
			{
				Binder = binder,
				TypeNameHandling = TypeNameHandling.Auto
			});

			var list = JsonConvert.DeserializeObject<List<IModFilter>>(json, new JsonSerializerSettings
			{
				Binder = binder,
				TypeNameHandling = TypeNameHandling.Auto
			});

			var text = @"hest: 49
gnu: 24
hest: 39
ged: 58
hest: 23";
			var results = modFilters.SelectMany(m => m.FindMatches(text)).ToList();
			var allMatches = results.Aggregate(string.Empty, (s, result) => s + result.Matches + Environment.NewLine);

			var x = 1;
		}
	}

	//public class AtLeast : IGroupCondition
	//{
	//	public int Group { get; set; }
	//	public int Value { get; set; }

	//	public bool IsSatisfiedBy(Match match)
	//	{
	//		return Convert.ToInt32(match.Groups[Group].Value) >= Value;
	//	}
	//}

	//public interface IGroupCondition
	//{
	//	int Group { get; set; }
	//	int Value { get; set; }
	//	bool IsSatisfiedBy(Match match);
	//}

	//public interface IModFilterResult
	//{
	//	string Name { get; set; }
	//	string Matches { get; set; }
	//}

	//public interface IModFilter
	//{
	//	string Name { get; set; }
	//	IEnumerable<IModFilterResult> FindMatches(string text);
	//}

	//public class RegexModFilter : IModFilter
	//{
	//	private Regex regex;
	//	public string Pattern { get; set; }
	//	public IEnumerable<IGroupCondition> GroupConditions { get; set; }

	//	public IEnumerable<IModFilterResult> FindMatches(string text)
	//	{
	//		regex = new Regex(Pattern);
	//		var matches = regex.Matches(text).Cast<Match>().ToList();

	//		foreach (var match in matches)
	//		{
	//			if (GroupConditions.All(g => g.IsSatisfiedBy(match)))
	//				yield return new RegexModFilterResult
	//				{
	//					Name = ">39",
	//					Matches = match.Groups[0].Value
	//				};
	//		}
	//	}

	//	public string Name { get; set; }
	//}

	//public class AndModFilter : IModFilter
	//{
	//	public IEnumerable<IModFilter> ChildFilters { get; set; }
	//	public string Name { get; set; }

	//	public IEnumerable<IModFilterResult> FindMatches(string text)
	//	{
	//		if (ChildFilters == null)
	//			return new IModFilterResult[0];

	//		if (ChildFilters.Any(c => !c.FindMatches(text).Any()))
	//			return new IModFilterResult[0];

	//		return ChildFilters.SelectMany(c => c.FindMatches(text));
	//	}
	//}

	//public class RegexModFilterResult : IModFilterResult
	//{
	//	public string Name { get; set; }
	//	public string Matches { get; set; }
	//}
}
