using System;
using System.Linq;
using System.Windows.Forms;

namespace PoeHelper.Monitor
{
	public partial class MonitorForm : Form
	{
		private readonly ClipBoardHandler clipBoardHandler;
		private readonly IItemParser itemParser;

		public MonitorForm(ClipBoardHandler clipBoardHandler, IItemParser parser)
		{
			InitializeComponent();
			this.clipBoardHandler = clipBoardHandler;
			this.clipBoardHandler.MessageReceived += ClipBoardHandlerOnMessageReceived;
			itemParser = parser;
		}

		private void ClipBoardHandlerOnMessageReceived(object sender, MessageReceivedDelegateArgs args)
		{
			var foundMods = itemParser.Parse(args.Message).ToList();

			var text = foundMods.Aggregate(string.Empty, (s, result) => s + result + Environment.NewLine);
			logTextBox.AppendText(text);
			logTextBox.AppendText(Environment.NewLine);
		}

		private void MonitorForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			clipBoardHandler.CloseHandler();
		}

		public void Log(string message)
		{
			logTextBox.AppendText(message);
		}

		private void reloadFilerButton_Click(object sender, System.EventArgs e)
		{
			Application.Restart();
		}

		private void clearButton_Click(object sender, System.EventArgs e)
		{
			logTextBox.Clear();
		}
	}
}
