using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;
using PoeHelper.Monitor.Filters;

namespace PoeHelper.Monitor
{
	internal static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		private static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			var filterFile = Path.Combine(Environment.CurrentDirectory, "filters.json");
			var filters = LoadFilters(filterFile);

			var monitorForm = new MonitorForm(new ClipBoardHandler(), new ItemParser(filters));

			monitorForm.Log("Loaded filters: ");
			monitorForm.Log(File.ReadAllText(filterFile));

			Application.Run(monitorForm);
		}

		private static IEnumerable<IModFilter> LoadFilters(string filterFile)
		{
			if (File.Exists(filterFile))
			{
				var json = File.ReadAllText(filterFile);
				

				var binder = new TypedSerializationBinder("PoeHelper.Monitor.{0}, PoeHelper.Monitor");
				return JsonConvert.DeserializeObject<List<IModFilter>>(json, new JsonSerializerSettings
				{
					Binder = binder,
					TypeNameHandling = TypeNameHandling.Auto
				});
			}

			return new IModFilter[0];
		}
	}
}
