using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PoeHelper.Monitor
{
	public class ClipBoardHandler : Form
	{
		private const int WmDrawclipboard = 0x0308; // WM_DRAWCLIPBOARD message
		private readonly IntPtr clipboardViewerNext;

		public ClipBoardHandler()
		{
			clipboardViewerNext = SetClipboardViewer(Handle);
		}

		public event MessageReceivedDelegate MessageReceived;

		[DllImport("User32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SetClipboardViewer(IntPtr hWndNewViewer);

		[DllImport("User32.dll", CharSet = CharSet.Auto)]
		public static extern bool ChangeClipboardChain(IntPtr hWndRemove, IntPtr hWndNewNext);

		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);
			if (m.Msg != WmDrawclipboard) return;

			var iData = Clipboard.GetDataObject();
			if (iData == null || !iData.GetDataPresent(DataFormats.Text)) return;

			var data = (string) iData.GetData(DataFormats.Text);

			if (MessageReceived != null)
				MessageReceived(this, new MessageReceivedDelegateArgs
				{
					Message = data
				});
		}

		public void CloseHandler()
		{
			ChangeClipboardChain(Handle, clipboardViewerNext);
		}
	}

	public delegate void MessageReceivedDelegate(object sender, MessageReceivedDelegateArgs args);

	public class MessageReceivedDelegateArgs
	{
		public string Message { get; set; }
	}
}
