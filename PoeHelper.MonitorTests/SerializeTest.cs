using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using PoeHelper.Monitor;
using Xunit;

namespace PoeHelper.MonitorTests
{
	public class SerializeTest
	{
		[Fact]
		public void WhatDoesItTest()
		{
			var list = new List<IDummy>
			{
				new Dummy1
				{
					Name = "Hansemand",
					Description = "blaq blabl"
				},
				new Dummy2
				{
					Age = 45
				}
			};

			var binder = new TypedSerializationBinder("PoeHelper.MonitorTests.{0}, PoeHelper.MonitorTests");
			var json = JsonConvert.SerializeObject(list, Formatting.Indented, new JsonSerializerSettings
			{
				Binder = binder,
				TypeNameHandling = TypeNameHandling.Auto
			});
			Console.WriteLine(json);

			var dummies = JsonConvert.DeserializeObject<List<IDummy>>(json, new JsonSerializerSettings
			{
				Binder = binder,
				TypeNameHandling = TypeNameHandling.Auto
			});
		}
	}

	public class Dummy2 : IDummy
	{
		public int Age { get; set; }
	}

	public class Dummy1 : IDummy
	{
		public string Name { get; set; }
		public string Description { get; set; }
	}

	public interface IDummy
	{
	}
}
