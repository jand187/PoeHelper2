namespace PoeHelper.Monitor
{
	partial class MonitorForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.clearButton = new System.Windows.Forms.Button();
			this.logTextBox = new System.Windows.Forms.TextBox();
			this.reloadFilerButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// clearButton
			// 
			this.clearButton.Location = new System.Drawing.Point(729, 347);
			this.clearButton.Name = "clearButton";
			this.clearButton.Size = new System.Drawing.Size(75, 23);
			this.clearButton.TabIndex = 3;
			this.clearButton.Text = "Clear";
			this.clearButton.UseVisualStyleBackColor = true;
			this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
			// 
			// logTextBox
			// 
			this.logTextBox.Dock = System.Windows.Forms.DockStyle.Top;
			this.logTextBox.Location = new System.Drawing.Point(0, 0);
			this.logTextBox.Multiline = true;
			this.logTextBox.Name = "logTextBox";
			this.logTextBox.ReadOnly = true;
			this.logTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.logTextBox.Size = new System.Drawing.Size(816, 341);
			this.logTextBox.TabIndex = 2;
			// 
			// reloadFilerButton
			// 
			this.reloadFilerButton.Location = new System.Drawing.Point(627, 347);
			this.reloadFilerButton.Name = "reloadFilerButton";
			this.reloadFilerButton.Size = new System.Drawing.Size(96, 23);
			this.reloadFilerButton.TabIndex = 4;
			this.reloadFilerButton.Text = "Reload filters";
			this.reloadFilerButton.UseVisualStyleBackColor = true;
			this.reloadFilerButton.Click += new System.EventHandler(this.reloadFilerButton_Click);
			// 
			// MonitorForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(816, 382);
			this.Controls.Add(this.reloadFilerButton);
			this.Controls.Add(this.clearButton);
			this.Controls.Add(this.logTextBox);
			this.Name = "MonitorForm";
			this.Text = "MonitorForm";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MonitorForm_FormClosing);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button clearButton;
		private System.Windows.Forms.TextBox logTextBox;
		private System.Windows.Forms.Button reloadFilerButton;
	}
}