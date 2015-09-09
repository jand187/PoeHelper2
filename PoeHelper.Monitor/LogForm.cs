using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms;
using Newtonsoft.Json;
using PoeHelper.Monitor.MatchCondition;

namespace PoeHelper.Monitor
{
	public partial class LogForm : Form, IDisposable
	{
		private readonly ClipBoardHandler clipBoardHandler;
		private readonly ModParser modParser;

		public LogForm(ClipBoardHandler clipBoardHandler, ModParser modParser)
		{
			InitializeComponent();
			this.clipBoardHandler = clipBoardHandler;
			this.clipBoardHandler.MessageReceived += ClipBoardHandlerOnMessageReceived;

			this.modParser = modParser;
		}

		private void ClipBoardHandlerOnMessageReceived(object sender, MessageReceivedDelegateArgs args)
		{
			var mods = modParser.ParseMods(args.Message).ToList();
			var text = mods.Aggregate(string.Empty, (mod, preferredMod) => mod + preferredMod.Header + Environment.NewLine + preferredMod.Description + Environment.NewLine);
			logTextBox.AppendText(text);
			logTextBox.AppendText(Environment.NewLine);
		}

		private void clearButton_Click(object sender, EventArgs e)
		{
			logTextBox.Clear();
		}

		private void LogForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			clipBoardHandler.CloseHandler();
		}
	}

	public class ModParser
	{
		private readonly IEnumerable<IModFilter> modFilters;

		public ModParser()
		{
			modFilters = new List<IModFilter>
			{
				new ModFilter
				{
					Name	= "High Resists",
					Pattern = @"\+(\d+)% to \w+ Resistance",
					MatchConditions = new List<IMatchCondition>
					{
						new GreaterThan
						{
							Group = 1,
							Value = 39,
						}
					}
				},
				new AndModFilter()
				{
					Name	= "Spell dmg (Amulet)",
					ChildFilters = new List<IModFilter>
					{
						new ModFilter
						{
							Name = "Spell dmg (Amulet)",
							Pattern = @"(\d+)% increased Spell Damage",
							MatchConditions = new List<IMatchCondition>
							{
								new GreaterThan
								{
									Group = 1,
									Value = 0
								}
							}
						},
						new ModFilter
						{
							Name = "Amulet",
							Pattern = "Amulet",
							MatchConditions = new List<IMatchCondition>
							{
								new HasText(),
							}
						}
					}
				}
			};

			var json = JsonConvert.SerializeObject(modFilters);
		}

		public IEnumerable<PreferredMod> ParseMods(string message)
		{
			foreach (var filter in modFilters)
			{
				if (filter.FindMatches(message))
				{
					yield return filter.CreateOutput();
					//{
					//	Name = filter.Name,
					//	Matches = filter.Matches,
					//};
				}
			}
		}
	}

	public class PreferredMod
	{
		public string Header { get; set; }
		public string Description { get; set; }
	}
}
