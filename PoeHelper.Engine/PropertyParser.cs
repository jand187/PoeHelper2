using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace PoeHelper.Engine
{
	public class PropertyParser
	{
		public IEnumerable<Requirement> Parse(IEnumerable<string> lines)
		{
			var keys = new[]
			{
				"Level",
				"Str",
				"Dex",
				"Int"
			};
		
			return keys.Where(k => lines.Any(l => l.StartsWith(k))).Select(s => new Requirement
			{
				Name = s,
				Value = Convert.ToInt32(lines.Single(l=>l.StartsWith(s)).Replace(s + ": ", string.Empty))
			});
		}

		private int GetProperty(IEnumerable<string> lines, string key)
		{
			return Convert.ToInt32(lines.Single(l => l.StartsWith(key)).Replace(key, string.Empty));
		}
	}
}
