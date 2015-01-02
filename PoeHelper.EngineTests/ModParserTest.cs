using PoeHelper.Engine;
using Xunit;

namespace PoeHelper.EngineTests
{
	public class ModParserTest
	{
		[Fact]
		public void WhatDoesItTest()
		{
			var target = new ModParser();
			var mods = target.Parse("");
		}
	}
}
