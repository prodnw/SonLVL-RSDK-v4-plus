namespace SonicRetro.SonLVL.GUI
{
	partial class NewModDialog
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
			this.components = new System.ComponentModel.Container();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.buttonOK = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.textModName = new System.Windows.Forms.TextBox();
			this.textModAuthor = new System.Windows.Forms.TextBox();
			this.checkOpenFolder = new System.Windows.Forms.CheckBox();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.textModDescription = new System.Windows.Forms.TextBox();
			this.textVersion = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.loadTextScripts = new System.Windows.Forms.CheckBox();
			this.disablePauseFocus = new System.Windows.Forms.CheckBox();
			this.redirectSave = new System.Windows.Forms.CheckBox();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(62, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Mod Name:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 95);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(63, 13);
			this.label2.TabIndex = 6;
			this.label2.Text = "Description:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(34, 43);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(41, 13);
			this.label3.TabIndex = 2;
			this.label3.Text = "Author:";
			// 
			// buttonOK
			// 
			this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.buttonOK.Location = new System.Drawing.Point(216, 217);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(75, 23);
			this.buttonOK.TabIndex = 12;
			this.buttonOK.Text = "&OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.buttonCancel.Location = new System.Drawing.Point(297, 217);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 13;
			this.buttonCancel.Text = "&Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			// 
			// textModName
			// 
			this.textModName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textModName.Location = new System.Drawing.Point(81, 12);
			this.textModName.Name = "textModName";
			this.textModName.Size = new System.Drawing.Size(256, 20);
			this.textModName.TabIndex = 1;
			// 
			// textModAuthor
			// 
			this.textModAuthor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textModAuthor.Location = new System.Drawing.Point(81, 40);
			this.textModAuthor.Name = "textModAuthor";
			this.textModAuthor.Size = new System.Drawing.Size(256, 20);
			this.textModAuthor.TabIndex = 3;
			// 
			// checkOpenFolder
			// 
			this.checkOpenFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.checkOpenFolder.AutoSize = true;
			this.checkOpenFolder.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkOpenFolder.Location = new System.Drawing.Point(12, 222);
			this.checkOpenFolder.Name = "checkOpenFolder";
			this.checkOpenFolder.Size = new System.Drawing.Size(87, 18);
			this.checkOpenFolder.TabIndex = 11;
			this.checkOpenFolder.Text = "Open folder";
			this.toolTip1.SetToolTip(this.checkOpenFolder, "Open the newly created mod\'s folder upon completion.");
			this.checkOpenFolder.UseVisualStyleBackColor = true;
			// 
			// toolTip1
			// 
			this.toolTip1.ShowAlways = true;
			// 
			// textModDescription
			// 
			this.textModDescription.AcceptsReturn = true;
			this.textModDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textModDescription.Location = new System.Drawing.Point(81, 92);
			this.textModDescription.Multiline = true;
			this.textModDescription.Name = "textModDescription";
			this.textModDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textModDescription.Size = new System.Drawing.Size(256, 64);
			this.textModDescription.TabIndex = 7;
			// 
			// textVersion
			// 
			this.textVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textVersion.Location = new System.Drawing.Point(81, 66);
			this.textVersion.Name = "textVersion";
			this.textVersion.Size = new System.Drawing.Size(256, 20);
			this.textVersion.TabIndex = 5;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(30, 69);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(45, 13);
			this.label4.TabIndex = 4;
			this.label4.Text = "Version:";
			// 
			// loadTextScripts
			// 
			this.loadTextScripts.AutoSize = true;
			this.loadTextScripts.Location = new System.Drawing.Point(12, 162);
			this.loadTextScripts.Name = "loadTextScripts";
			this.loadTextScripts.Size = new System.Drawing.Size(109, 17);
			this.loadTextScripts.TabIndex = 14;
			this.loadTextScripts.Text = "Load Text Scripts";
			this.loadTextScripts.UseVisualStyleBackColor = true;
			// 
			// disablePauseFocus
			// 
			this.disablePauseFocus.AutoSize = true;
			this.disablePauseFocus.Location = new System.Drawing.Point(127, 162);
			this.disablePauseFocus.Name = "disablePauseFocus";
			this.disablePauseFocus.Size = new System.Drawing.Size(126, 17);
			this.disablePauseFocus.TabIndex = 15;
			this.disablePauseFocus.Text = "Disable Pause Focus";
			this.disablePauseFocus.UseVisualStyleBackColor = true;
			// 
			// redirectSave
			// 
			this.redirectSave.AutoSize = true;
			this.redirectSave.Location = new System.Drawing.Point(259, 162);
			this.redirectSave.Name = "redirectSave";
			this.redirectSave.Size = new System.Drawing.Size(94, 17);
			this.redirectSave.TabIndex = 16;
			this.redirectSave.Text = "Redirect Save";
			this.redirectSave.UseVisualStyleBackColor = true;
			// 
			// checkBox1
			// 
			this.checkBox1.AutoSize = true;
			this.checkBox1.Location = new System.Drawing.Point(12, 185);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(80, 17);
			this.checkBox1.TabIndex = 17;
			this.checkBox1.Text = "checkBox1";
			this.checkBox1.UseVisualStyleBackColor = true;
			// 
			// NewModDialog
			// 
			this.AcceptButton = this.buttonOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(384, 252);
			this.Controls.Add(this.checkBox1);
			this.Controls.Add(this.redirectSave);
			this.Controls.Add(this.disablePauseFocus);
			this.Controls.Add(this.loadTextScripts);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.checkOpenFolder);
			this.Controls.Add(this.textModName);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.textModAuthor);
			this.Controls.Add(this.textModDescription);
			this.Controls.Add(this.textVersion);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "NewModDialog";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "New Mod";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.TextBox textModName;
		private System.Windows.Forms.TextBox textModAuthor;
		private System.Windows.Forms.CheckBox checkOpenFolder;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.TextBox textModDescription;
        private System.Windows.Forms.TextBox textVersion;
        private System.Windows.Forms.Label label4;
		private System.Windows.Forms.CheckBox loadTextScripts;
		private System.Windows.Forms.CheckBox disablePauseFocus;
		private System.Windows.Forms.CheckBox redirectSave;
		private System.Windows.Forms.CheckBox checkBox1;
	}
}