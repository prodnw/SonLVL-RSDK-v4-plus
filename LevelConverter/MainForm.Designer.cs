namespace SonicRetro.SonLVL.LevelConverter
{
	partial class MainForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.fileSelector1 = new SonicRetro.SonLVL.API.FileSelector();
			this.srcVersion = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.dstGameConfig = new SonicRetro.SonLVL.API.FileSelector();
			this.dstGCLabel = new System.Windows.Forms.Label();
			this.srcGameConfig = new SonicRetro.SonLVL.API.FileSelector();
			this.dstVersion = new System.Windows.Forms.ComboBox();
			this.objectMode = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.srcGCLabel = new System.Windows.Forms.Label();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			((System.ComponentModel.ISupportInitialize)(this.fileSelector1)).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dstGameConfig)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.srcGameConfig)).BeginInit();
			this.SuspendLayout();
			// 
			// fileSelector1
			// 
			this.fileSelector1.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.fileSelector1.DefaultExt = "bin";
			this.fileSelector1.FileName = "";
			this.fileSelector1.Filter = "Scene Files|Act*.bin;Scene*.bin";
			this.fileSelector1.Location = new System.Drawing.Point(133, 30);
			this.fileSelector1.Name = "fileSelector1";
			this.fileSelector1.Size = new System.Drawing.Size(302, 24);
			this.fileSelector1.TabIndex = 3;
			this.fileSelector1.FileNameChanged += new System.EventHandler(this.fileSelector1_FileNameChanged);
			// 
			// srcVersion
			// 
			this.srcVersion.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.srcVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.srcVersion.FormattingEnabled = true;
			this.srcVersion.Items.AddRange(new object[] {
            "V3 (Sonic CD)",
            "V4 (Sonic 1/2)",
            "V5 (Sonic Mania)"});
			this.srcVersion.Location = new System.Drawing.Point(133, 3);
			this.srcVersion.Name = "srcVersion";
			this.srcVersion.Size = new System.Drawing.Size(155, 21);
			this.srcVersion.TabIndex = 1;
			this.srcVersion.SelectedIndexChanged += new System.EventHandler(this.srcVersion_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 7);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(78, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "RSDK Version:";
			// 
			// label2
			// 
			this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(3, 35);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(41, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Scene:";
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.Controls.Add(this.dstGameConfig, 1, 5);
			this.tableLayoutPanel1.Controls.Add(this.dstGCLabel, 0, 5);
			this.tableLayoutPanel1.Controls.Add(this.srcGameConfig, 1, 4);
			this.tableLayoutPanel1.Controls.Add(this.dstVersion, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.objectMode, 1, 3);
			this.tableLayoutPanel1.Controls.Add(this.label3, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.fileSelector1, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.srcVersion, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.button1, 1, 6);
			this.tableLayoutPanel1.Controls.Add(this.label4, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.srcGCLabel, 0, 4);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 7;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(484, 552);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// dstGameConfig
			// 
			this.dstGameConfig.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.dstGameConfig.DefaultExt = "bin";
			this.dstGameConfig.FileName = "";
			this.dstGameConfig.Filter = "GameConfig.bin|GameConfig.bin";
			this.dstGameConfig.Location = new System.Drawing.Point(133, 144);
			this.dstGameConfig.Name = "dstGameConfig";
			this.dstGameConfig.Size = new System.Drawing.Size(302, 24);
			this.dstGameConfig.TabIndex = 11;
			this.dstGameConfig.Visible = false;
			this.dstGameConfig.FileNameChanged += new System.EventHandler(this.dstGameConfig_FileNameChanged);
			// 
			// dstGCLabel
			// 
			this.dstGCLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.dstGCLabel.AutoSize = true;
			this.dstGCLabel.Location = new System.Drawing.Point(3, 149);
			this.dstGCLabel.Name = "dstGCLabel";
			this.dstGCLabel.Size = new System.Drawing.Size(124, 13);
			this.dstGCLabel.TabIndex = 10;
			this.dstGCLabel.Text = "Destination GameConfig:";
			this.dstGCLabel.Visible = false;
			// 
			// srcGameConfig
			// 
			this.srcGameConfig.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.srcGameConfig.DefaultExt = "bin";
			this.srcGameConfig.FileName = "";
			this.srcGameConfig.Filter = "GameConfig.bin|GameConfig.bin";
			this.srcGameConfig.Location = new System.Drawing.Point(133, 114);
			this.srcGameConfig.Name = "srcGameConfig";
			this.srcGameConfig.Size = new System.Drawing.Size(302, 24);
			this.srcGameConfig.TabIndex = 9;
			this.srcGameConfig.Visible = false;
			this.srcGameConfig.FileNameChanged += new System.EventHandler(this.srcGameConfig_FileNameChanged);
			// 
			// dstVersion
			// 
			this.dstVersion.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.dstVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.dstVersion.FormattingEnabled = true;
			this.dstVersion.Items.AddRange(new object[] {
            "V3 (Sonic CD)",
            "V4 (Sonic 1/2)",
            "V5 (Sonic Mania)"});
			this.dstVersion.Location = new System.Drawing.Point(133, 60);
			this.dstVersion.Name = "dstVersion";
			this.dstVersion.Size = new System.Drawing.Size(155, 21);
			this.dstVersion.TabIndex = 5;
			this.dstVersion.SelectedIndexChanged += new System.EventHandler(this.dstVersion_SelectedIndexChanged);
			// 
			// objectMode
			// 
			this.objectMode.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.objectMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.objectMode.FormattingEnabled = true;
			this.objectMode.Items.AddRange(new object[] {
            "Don\'t Include Objects",
            "Match Globals By Name, Add Rest to Stage List",
            "Match Globals By Name, Delete Rest",
            "Delete Global Objects From Scene",
            "Add Global Objects to Stage List",
            "Leave Objects As-Is"});
			this.objectMode.Location = new System.Drawing.Point(133, 87);
			this.objectMode.Name = "objectMode";
			this.objectMode.Size = new System.Drawing.Size(302, 21);
			this.objectMode.TabIndex = 7;
			this.objectMode.SelectedIndexChanged += new System.EventHandler(this.objectMode_SelectedIndexChanged);
			// 
			// label3
			// 
			this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(3, 91);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(46, 13);
			this.label3.TabIndex = 6;
			this.label3.Text = "Objects:";
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.Enabled = false;
			this.button1.Location = new System.Drawing.Point(406, 174);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 12;
			this.button1.Text = "Convert...";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// label4
			// 
			this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(3, 64);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(63, 13);
			this.label4.TabIndex = 4;
			this.label4.Text = "Convert To:";
			// 
			// srcGCLabel
			// 
			this.srcGCLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.srcGCLabel.AutoSize = true;
			this.srcGCLabel.Location = new System.Drawing.Point(3, 119);
			this.srcGCLabel.Name = "srcGCLabel";
			this.srcGCLabel.Size = new System.Drawing.Size(105, 13);
			this.srcGCLabel.TabIndex = 8;
			this.srcGCLabel.Text = "Source GameConfig:";
			this.srcGCLabel.Visible = false;
			// 
			// saveFileDialog1
			// 
			this.saveFileDialog1.DefaultExt = "bin";
			this.saveFileDialog1.RestoreDirectory = true;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.ClientSize = new System.Drawing.Size(484, 552);
			this.Controls.Add(this.tableLayoutPanel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.Text = "RSDK Level Converter";
			this.Load += new System.EventHandler(this.MainForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.fileSelector1)).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dstGameConfig)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.srcGameConfig)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private API.FileSelector fileSelector1;
		private System.Windows.Forms.ComboBox srcVersion;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox objectMode;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox dstVersion;
		private System.Windows.Forms.Label srcGCLabel;
		private API.FileSelector srcGameConfig;
		private API.FileSelector dstGameConfig;
		private System.Windows.Forms.Label dstGCLabel;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
	}
}

