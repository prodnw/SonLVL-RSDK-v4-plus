
namespace SonicRetro.SonLVL
{
	partial class GameConfigEditorDialog
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
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.gameDescription = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.gameName = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.objectForceLoad = new System.Windows.Forms.CheckBox();
			this.browseScriptButton = new System.Windows.Forms.Button();
			this.objectScriptBox = new System.Windows.Forms.TextBox();
			this.label23 = new System.Windows.Forms.Label();
			this.objectNameBox = new System.Windows.Forms.TextBox();
			this.label22 = new System.Windows.Forms.Label();
			this.objectDeleteButton = new System.Windows.Forms.Button();
			this.objectAddButton = new System.Windows.Forms.Button();
			this.objectListBox = new System.Windows.Forms.ListBox();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.variableValue = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.variableName = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.variableDeleteButton = new System.Windows.Forms.Button();
			this.variableAddButton = new System.Windows.Forms.Button();
			this.variableListBox = new System.Windows.Forms.ListBox();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.sfxBrowseButton = new System.Windows.Forms.Button();
			this.sfxFileBox = new System.Windows.Forms.TextBox();
			this.label24 = new System.Windows.Forms.Label();
			this.sfxNameBox = new System.Windows.Forms.TextBox();
			this.label25 = new System.Windows.Forms.Label();
			this.sfxDeleteButton = new System.Windows.Forms.Button();
			this.sfxAddButton = new System.Windows.Forms.Button();
			this.sfxListBox = new System.Windows.Forms.ListBox();
			this.tabPage5 = new System.Windows.Forms.TabPage();
			this.playerName = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.playerDeleteButton = new System.Windows.Forms.Button();
			this.playerAddButton = new System.Windows.Forms.Button();
			this.playerListBox = new System.Windows.Forms.ListBox();
			this.tabPage6 = new System.Windows.Forms.TabPage();
			this.stageBrowseButton = new System.Windows.Forms.Button();
			this.stageHighlight = new System.Windows.Forms.CheckBox();
			this.stageAct = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.stageFolder = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.stageCategory = new System.Windows.Forms.ComboBox();
			this.stageName = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.stageDeleteButton = new System.Windows.Forms.Button();
			this.stageAddButton = new System.Windows.Forms.Button();
			this.stageListBox = new System.Windows.Forms.ListBox();
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.convertButton = new System.Windows.Forms.Button();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.tabPage3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.variableValue)).BeginInit();
			this.tabPage4.SuspendLayout();
			this.tabPage5.SuspendLayout();
			this.tabPage6.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Controls.Add(this.tabPage4);
			this.tabControl1.Controls.Add(this.tabPage5);
			this.tabControl1.Controls.Add(this.tabPage6);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(384, 220);
			this.tabControl1.TabIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.gameDescription);
			this.tabPage1.Controls.Add(this.label2);
			this.tabPage1.Controls.Add(this.gameName);
			this.tabPage1.Controls.Add(this.label1);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(376, 194);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Game Info";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// gameDescription
			// 
			this.gameDescription.AcceptsReturn = true;
			this.gameDescription.AcceptsTab = true;
			this.gameDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gameDescription.Enabled = false;
			this.gameDescription.Location = new System.Drawing.Point(75, 32);
			this.gameDescription.MaxLength = 255;
			this.gameDescription.Multiline = true;
			this.gameDescription.Name = "gameDescription";
			this.gameDescription.Size = new System.Drawing.Size(295, 156);
			this.gameDescription.TabIndex = 3;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 35);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(63, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Description:";
			// 
			// gameName
			// 
			this.gameName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gameName.Enabled = false;
			this.gameName.Location = new System.Drawing.Point(75, 6);
			this.gameName.MaxLength = 255;
			this.gameName.Name = "gameName";
			this.gameName.Size = new System.Drawing.Size(295, 20);
			this.gameName.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(38, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Name:";
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.objectForceLoad);
			this.tabPage2.Controls.Add(this.browseScriptButton);
			this.tabPage2.Controls.Add(this.objectScriptBox);
			this.tabPage2.Controls.Add(this.label23);
			this.tabPage2.Controls.Add(this.objectNameBox);
			this.tabPage2.Controls.Add(this.label22);
			this.tabPage2.Controls.Add(this.objectDeleteButton);
			this.tabPage2.Controls.Add(this.objectAddButton);
			this.tabPage2.Controls.Add(this.objectListBox);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(376, 194);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Object List";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// objectForceLoad
			// 
			this.objectForceLoad.AutoSize = true;
			this.objectForceLoad.Enabled = false;
			this.objectForceLoad.Location = new System.Drawing.Point(193, 116);
			this.objectForceLoad.Name = "objectForceLoad";
			this.objectForceLoad.Size = new System.Drawing.Size(80, 17);
			this.objectForceLoad.TabIndex = 8;
			this.objectForceLoad.Text = "Force Load";
			this.objectForceLoad.UseVisualStyleBackColor = true;
			this.objectForceLoad.CheckedChanged += new System.EventHandler(this.objectForceLoad_CheckedChanged);
			// 
			// browseScriptButton
			// 
			this.browseScriptButton.Enabled = false;
			this.browseScriptButton.Location = new System.Drawing.Point(193, 87);
			this.browseScriptButton.Name = "browseScriptButton";
			this.browseScriptButton.Size = new System.Drawing.Size(75, 23);
			this.browseScriptButton.TabIndex = 7;
			this.browseScriptButton.Text = "Browse...";
			this.browseScriptButton.UseVisualStyleBackColor = true;
			this.browseScriptButton.Click += new System.EventHandler(this.browseScriptButton_Click);
			// 
			// objectScriptBox
			// 
			this.objectScriptBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.objectScriptBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.objectScriptBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
			this.objectScriptBox.Enabled = false;
			this.objectScriptBox.Location = new System.Drawing.Point(193, 61);
			this.objectScriptBox.MaxLength = 255;
			this.objectScriptBox.Name = "objectScriptBox";
			this.objectScriptBox.Size = new System.Drawing.Size(177, 20);
			this.objectScriptBox.TabIndex = 6;
			this.objectScriptBox.TextChanged += new System.EventHandler(this.objectScriptBox_TextChanged);
			// 
			// label23
			// 
			this.label23.AutoSize = true;
			this.label23.Location = new System.Drawing.Point(193, 45);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(37, 13);
			this.label23.TabIndex = 5;
			this.label23.Text = "Script:";
			// 
			// objectNameBox
			// 
			this.objectNameBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.objectNameBox.Enabled = false;
			this.objectNameBox.Location = new System.Drawing.Point(193, 22);
			this.objectNameBox.MaxLength = 255;
			this.objectNameBox.Name = "objectNameBox";
			this.objectNameBox.Size = new System.Drawing.Size(177, 20);
			this.objectNameBox.TabIndex = 4;
			this.objectNameBox.TextChanged += new System.EventHandler(this.objectNameBox_TextChanged);
			// 
			// label22
			// 
			this.label22.AutoSize = true;
			this.label22.Location = new System.Drawing.Point(193, 6);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(38, 13);
			this.label22.TabIndex = 3;
			this.label22.Text = "Name:";
			// 
			// objectDeleteButton
			// 
			this.objectDeleteButton.Enabled = false;
			this.objectDeleteButton.Location = new System.Drawing.Point(87, 159);
			this.objectDeleteButton.Name = "objectDeleteButton";
			this.objectDeleteButton.Size = new System.Drawing.Size(75, 23);
			this.objectDeleteButton.TabIndex = 2;
			this.objectDeleteButton.Text = "Delete";
			this.objectDeleteButton.UseVisualStyleBackColor = true;
			this.objectDeleteButton.Click += new System.EventHandler(this.objectDeleteButton_Click);
			// 
			// objectAddButton
			// 
			this.objectAddButton.Location = new System.Drawing.Point(6, 159);
			this.objectAddButton.Name = "objectAddButton";
			this.objectAddButton.Size = new System.Drawing.Size(75, 23);
			this.objectAddButton.TabIndex = 1;
			this.objectAddButton.Text = "Add";
			this.objectAddButton.UseVisualStyleBackColor = true;
			this.objectAddButton.Click += new System.EventHandler(this.objectAddButton_Click);
			// 
			// objectListBox
			// 
			this.objectListBox.FormattingEnabled = true;
			this.objectListBox.Location = new System.Drawing.Point(6, 6);
			this.objectListBox.Name = "objectListBox";
			this.objectListBox.Size = new System.Drawing.Size(181, 147);
			this.objectListBox.TabIndex = 0;
			this.objectListBox.SelectedIndexChanged += new System.EventHandler(this.objectListBox_SelectedIndexChanged);
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.variableValue);
			this.tabPage3.Controls.Add(this.label3);
			this.tabPage3.Controls.Add(this.variableName);
			this.tabPage3.Controls.Add(this.label4);
			this.tabPage3.Controls.Add(this.variableDeleteButton);
			this.tabPage3.Controls.Add(this.variableAddButton);
			this.tabPage3.Controls.Add(this.variableListBox);
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage3.Size = new System.Drawing.Size(376, 194);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "Variables";
			this.tabPage3.UseVisualStyleBackColor = true;
			// 
			// variableValue
			// 
			this.variableValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.variableValue.Enabled = false;
			this.variableValue.Location = new System.Drawing.Point(211, 61);
			this.variableValue.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
			this.variableValue.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
			this.variableValue.Name = "variableValue";
			this.variableValue.Size = new System.Drawing.Size(159, 20);
			this.variableValue.TabIndex = 6;
			this.variableValue.ValueChanged += new System.EventHandler(this.variableValue_ValueChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(211, 45);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(37, 13);
			this.label3.TabIndex = 5;
			this.label3.Text = "Value:";
			// 
			// variableName
			// 
			this.variableName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.variableName.Enabled = false;
			this.variableName.Location = new System.Drawing.Point(211, 22);
			this.variableName.MaxLength = 255;
			this.variableName.Name = "variableName";
			this.variableName.Size = new System.Drawing.Size(159, 20);
			this.variableName.TabIndex = 4;
			this.variableName.TextChanged += new System.EventHandler(this.variableName_TextChanged);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(211, 6);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(38, 13);
			this.label4.TabIndex = 3;
			this.label4.Text = "Name:";
			// 
			// variableDeleteButton
			// 
			this.variableDeleteButton.Enabled = false;
			this.variableDeleteButton.Location = new System.Drawing.Point(87, 159);
			this.variableDeleteButton.Name = "variableDeleteButton";
			this.variableDeleteButton.Size = new System.Drawing.Size(75, 23);
			this.variableDeleteButton.TabIndex = 2;
			this.variableDeleteButton.Text = "Delete";
			this.variableDeleteButton.UseVisualStyleBackColor = true;
			this.variableDeleteButton.Click += new System.EventHandler(this.variableDeleteButton_Click);
			// 
			// variableAddButton
			// 
			this.variableAddButton.Location = new System.Drawing.Point(6, 159);
			this.variableAddButton.Name = "variableAddButton";
			this.variableAddButton.Size = new System.Drawing.Size(75, 23);
			this.variableAddButton.TabIndex = 1;
			this.variableAddButton.Text = "Add";
			this.variableAddButton.UseVisualStyleBackColor = true;
			this.variableAddButton.Click += new System.EventHandler(this.variableAddButton_Click);
			// 
			// variableListBox
			// 
			this.variableListBox.FormattingEnabled = true;
			this.variableListBox.Location = new System.Drawing.Point(6, 6);
			this.variableListBox.Name = "variableListBox";
			this.variableListBox.Size = new System.Drawing.Size(199, 147);
			this.variableListBox.TabIndex = 0;
			this.variableListBox.SelectedIndexChanged += new System.EventHandler(this.variableListBox_SelectedIndexChanged);
			// 
			// tabPage4
			// 
			this.tabPage4.Controls.Add(this.sfxBrowseButton);
			this.tabPage4.Controls.Add(this.sfxFileBox);
			this.tabPage4.Controls.Add(this.label24);
			this.tabPage4.Controls.Add(this.sfxNameBox);
			this.tabPage4.Controls.Add(this.label25);
			this.tabPage4.Controls.Add(this.sfxDeleteButton);
			this.tabPage4.Controls.Add(this.sfxAddButton);
			this.tabPage4.Controls.Add(this.sfxListBox);
			this.tabPage4.Location = new System.Drawing.Point(4, 22);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage4.Size = new System.Drawing.Size(376, 194);
			this.tabPage4.TabIndex = 3;
			this.tabPage4.Text = "Sound Effects";
			this.tabPage4.UseVisualStyleBackColor = true;
			// 
			// sfxBrowseButton
			// 
			this.sfxBrowseButton.Enabled = false;
			this.sfxBrowseButton.Location = new System.Drawing.Point(211, 87);
			this.sfxBrowseButton.Name = "sfxBrowseButton";
			this.sfxBrowseButton.Size = new System.Drawing.Size(75, 23);
			this.sfxBrowseButton.TabIndex = 7;
			this.sfxBrowseButton.Text = "Browse...";
			this.sfxBrowseButton.UseVisualStyleBackColor = true;
			this.sfxBrowseButton.Click += new System.EventHandler(this.sfxBrowseButton_Click);
			// 
			// sfxFileBox
			// 
			this.sfxFileBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.sfxFileBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.sfxFileBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
			this.sfxFileBox.Enabled = false;
			this.sfxFileBox.Location = new System.Drawing.Point(211, 61);
			this.sfxFileBox.MaxLength = 255;
			this.sfxFileBox.Name = "sfxFileBox";
			this.sfxFileBox.Size = new System.Drawing.Size(159, 20);
			this.sfxFileBox.TabIndex = 6;
			this.sfxFileBox.TextChanged += new System.EventHandler(this.sfxFileBox_TextChanged);
			// 
			// label24
			// 
			this.label24.AutoSize = true;
			this.label24.Location = new System.Drawing.Point(211, 45);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(26, 13);
			this.label24.TabIndex = 5;
			this.label24.Text = "File:";
			// 
			// sfxNameBox
			// 
			this.sfxNameBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.sfxNameBox.Enabled = false;
			this.sfxNameBox.Location = new System.Drawing.Point(211, 22);
			this.sfxNameBox.MaxLength = 255;
			this.sfxNameBox.Name = "sfxNameBox";
			this.sfxNameBox.Size = new System.Drawing.Size(159, 20);
			this.sfxNameBox.TabIndex = 4;
			this.sfxNameBox.TextChanged += new System.EventHandler(this.sfxNameBox_TextChanged);
			// 
			// label25
			// 
			this.label25.AutoSize = true;
			this.label25.Location = new System.Drawing.Point(211, 6);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(38, 13);
			this.label25.TabIndex = 3;
			this.label25.Text = "Name:";
			// 
			// sfxDeleteButton
			// 
			this.sfxDeleteButton.Enabled = false;
			this.sfxDeleteButton.Location = new System.Drawing.Point(87, 159);
			this.sfxDeleteButton.Name = "sfxDeleteButton";
			this.sfxDeleteButton.Size = new System.Drawing.Size(75, 23);
			this.sfxDeleteButton.TabIndex = 2;
			this.sfxDeleteButton.Text = "Delete";
			this.sfxDeleteButton.UseVisualStyleBackColor = true;
			this.sfxDeleteButton.Click += new System.EventHandler(this.sfxDeleteButton_Click);
			// 
			// sfxAddButton
			// 
			this.sfxAddButton.Location = new System.Drawing.Point(6, 159);
			this.sfxAddButton.Name = "sfxAddButton";
			this.sfxAddButton.Size = new System.Drawing.Size(75, 23);
			this.sfxAddButton.TabIndex = 1;
			this.sfxAddButton.Text = "Add";
			this.sfxAddButton.UseVisualStyleBackColor = true;
			this.sfxAddButton.Click += new System.EventHandler(this.sfxAddButton_Click);
			// 
			// sfxListBox
			// 
			this.sfxListBox.FormattingEnabled = true;
			this.sfxListBox.Location = new System.Drawing.Point(6, 6);
			this.sfxListBox.Name = "sfxListBox";
			this.sfxListBox.Size = new System.Drawing.Size(199, 147);
			this.sfxListBox.TabIndex = 0;
			this.sfxListBox.SelectedIndexChanged += new System.EventHandler(this.sfxListBox_SelectedIndexChanged);
			// 
			// tabPage5
			// 
			this.tabPage5.Controls.Add(this.playerName);
			this.tabPage5.Controls.Add(this.label6);
			this.tabPage5.Controls.Add(this.playerDeleteButton);
			this.tabPage5.Controls.Add(this.playerAddButton);
			this.tabPage5.Controls.Add(this.playerListBox);
			this.tabPage5.Location = new System.Drawing.Point(4, 22);
			this.tabPage5.Name = "tabPage5";
			this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage5.Size = new System.Drawing.Size(376, 194);
			this.tabPage5.TabIndex = 4;
			this.tabPage5.Text = "Players";
			this.tabPage5.UseVisualStyleBackColor = true;
			// 
			// playerName
			// 
			this.playerName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.playerName.Enabled = false;
			this.playerName.Location = new System.Drawing.Point(211, 22);
			this.playerName.MaxLength = 255;
			this.playerName.Name = "playerName";
			this.playerName.Size = new System.Drawing.Size(159, 20);
			this.playerName.TabIndex = 4;
			this.playerName.TextChanged += new System.EventHandler(this.playerName_TextChanged);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(211, 6);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(38, 13);
			this.label6.TabIndex = 3;
			this.label6.Text = "Name:";
			// 
			// playerDeleteButton
			// 
			this.playerDeleteButton.Enabled = false;
			this.playerDeleteButton.Location = new System.Drawing.Point(87, 159);
			this.playerDeleteButton.Name = "playerDeleteButton";
			this.playerDeleteButton.Size = new System.Drawing.Size(75, 23);
			this.playerDeleteButton.TabIndex = 2;
			this.playerDeleteButton.Text = "Delete";
			this.playerDeleteButton.UseVisualStyleBackColor = true;
			this.playerDeleteButton.Click += new System.EventHandler(this.playerDeleteButton_Click);
			// 
			// playerAddButton
			// 
			this.playerAddButton.Location = new System.Drawing.Point(6, 159);
			this.playerAddButton.Name = "playerAddButton";
			this.playerAddButton.Size = new System.Drawing.Size(75, 23);
			this.playerAddButton.TabIndex = 1;
			this.playerAddButton.Text = "Add";
			this.playerAddButton.UseVisualStyleBackColor = true;
			this.playerAddButton.Click += new System.EventHandler(this.playerAddButton_Click);
			// 
			// playerListBox
			// 
			this.playerListBox.FormattingEnabled = true;
			this.playerListBox.Location = new System.Drawing.Point(6, 6);
			this.playerListBox.Name = "playerListBox";
			this.playerListBox.Size = new System.Drawing.Size(199, 147);
			this.playerListBox.TabIndex = 0;
			this.playerListBox.SelectedIndexChanged += new System.EventHandler(this.playerListBox_SelectedIndexChanged);
			// 
			// tabPage6
			// 
			this.tabPage6.Controls.Add(this.stageBrowseButton);
			this.tabPage6.Controls.Add(this.stageHighlight);
			this.tabPage6.Controls.Add(this.stageAct);
			this.tabPage6.Controls.Add(this.label9);
			this.tabPage6.Controls.Add(this.stageFolder);
			this.tabPage6.Controls.Add(this.label5);
			this.tabPage6.Controls.Add(this.label8);
			this.tabPage6.Controls.Add(this.stageCategory);
			this.tabPage6.Controls.Add(this.stageName);
			this.tabPage6.Controls.Add(this.label7);
			this.tabPage6.Controls.Add(this.stageDeleteButton);
			this.tabPage6.Controls.Add(this.stageAddButton);
			this.tabPage6.Controls.Add(this.stageListBox);
			this.tabPage6.Location = new System.Drawing.Point(4, 22);
			this.tabPage6.Name = "tabPage6";
			this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage6.Size = new System.Drawing.Size(376, 194);
			this.tabPage6.TabIndex = 5;
			this.tabPage6.Text = "Stages";
			this.tabPage6.UseVisualStyleBackColor = true;
			// 
			// stageBrowseButton
			// 
			this.stageBrowseButton.Enabled = false;
			this.stageBrowseButton.Location = new System.Drawing.Point(211, 165);
			this.stageBrowseButton.Name = "stageBrowseButton";
			this.stageBrowseButton.Size = new System.Drawing.Size(75, 23);
			this.stageBrowseButton.TabIndex = 12;
			this.stageBrowseButton.Text = "Browse...";
			this.stageBrowseButton.UseVisualStyleBackColor = true;
			this.stageBrowseButton.Click += new System.EventHandler(this.stageBrowseButton_Click);
			// 
			// stageHighlight
			// 
			this.stageHighlight.AutoSize = true;
			this.stageHighlight.Location = new System.Drawing.Point(211, 142);
			this.stageHighlight.Name = "stageHighlight";
			this.stageHighlight.Size = new System.Drawing.Size(67, 17);
			this.stageHighlight.TabIndex = 11;
			this.stageHighlight.Text = "Highlight";
			this.stageHighlight.UseVisualStyleBackColor = true;
			this.stageHighlight.CheckedChanged += new System.EventHandler(this.stageHighlight_CheckedChanged);
			// 
			// stageAct
			// 
			this.stageAct.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.stageAct.Enabled = false;
			this.stageAct.Location = new System.Drawing.Point(211, 77);
			this.stageAct.MaxLength = 255;
			this.stageAct.Name = "stageAct";
			this.stageAct.Size = new System.Drawing.Size(159, 20);
			this.stageAct.TabIndex = 8;
			this.stageAct.TextChanged += new System.EventHandler(this.stageAct_TextChanged);
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(211, 61);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(26, 13);
			this.label9.TabIndex = 7;
			this.label9.Text = "Act:";
			// 
			// stageFolder
			// 
			this.stageFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.stageFolder.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.stageFolder.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
			this.stageFolder.Enabled = false;
			this.stageFolder.Location = new System.Drawing.Point(211, 38);
			this.stageFolder.MaxLength = 255;
			this.stageFolder.Name = "stageFolder";
			this.stageFolder.Size = new System.Drawing.Size(159, 20);
			this.stageFolder.TabIndex = 6;
			this.stageFolder.TextChanged += new System.EventHandler(this.stageFolder_TextChanged);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(211, 22);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(39, 13);
			this.label5.TabIndex = 5;
			this.label5.Text = "Folder:";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(6, 9);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(52, 13);
			this.label8.TabIndex = 0;
			this.label8.Text = "Category:";
			// 
			// stageCategory
			// 
			this.stageCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.stageCategory.FormattingEnabled = true;
			this.stageCategory.Items.AddRange(new object[] {
            "Presentation Stages",
            "Regular Stages",
            "Special Stages",
            "Bonus Stages"});
			this.stageCategory.Location = new System.Drawing.Point(64, 6);
			this.stageCategory.Name = "stageCategory";
			this.stageCategory.Size = new System.Drawing.Size(141, 21);
			this.stageCategory.TabIndex = 1;
			this.stageCategory.SelectedIndexChanged += new System.EventHandler(this.stageCategory_SelectedIndexChanged);
			// 
			// stageName
			// 
			this.stageName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.stageName.Enabled = false;
			this.stageName.Location = new System.Drawing.Point(211, 116);
			this.stageName.MaxLength = 255;
			this.stageName.Name = "stageName";
			this.stageName.Size = new System.Drawing.Size(159, 20);
			this.stageName.TabIndex = 10;
			this.stageName.TextChanged += new System.EventHandler(this.stageName_TextChanged);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(211, 100);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(38, 13);
			this.label7.TabIndex = 9;
			this.label7.Text = "Name:";
			// 
			// stageDeleteButton
			// 
			this.stageDeleteButton.Enabled = false;
			this.stageDeleteButton.Location = new System.Drawing.Point(87, 160);
			this.stageDeleteButton.Name = "stageDeleteButton";
			this.stageDeleteButton.Size = new System.Drawing.Size(75, 23);
			this.stageDeleteButton.TabIndex = 4;
			this.stageDeleteButton.Text = "Delete";
			this.stageDeleteButton.UseVisualStyleBackColor = true;
			this.stageDeleteButton.Click += new System.EventHandler(this.stageDeleteButton_Click);
			// 
			// stageAddButton
			// 
			this.stageAddButton.Location = new System.Drawing.Point(6, 160);
			this.stageAddButton.Name = "stageAddButton";
			this.stageAddButton.Size = new System.Drawing.Size(75, 23);
			this.stageAddButton.TabIndex = 3;
			this.stageAddButton.Text = "Add";
			this.stageAddButton.UseVisualStyleBackColor = true;
			this.stageAddButton.Click += new System.EventHandler(this.stageAddButton_Click);
			// 
			// stageListBox
			// 
			this.stageListBox.FormattingEnabled = true;
			this.stageListBox.Location = new System.Drawing.Point(6, 33);
			this.stageListBox.Name = "stageListBox";
			this.stageListBox.Size = new System.Drawing.Size(199, 121);
			this.stageListBox.TabIndex = 2;
			this.stageListBox.SelectedIndexChanged += new System.EventHandler(this.stageListBox_SelectedIndexChanged);
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okButton.Location = new System.Drawing.Point(216, 226);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 2;
			this.okButton.Text = "&OK";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(297, 226);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 3;
			this.cancelButton.Text = "&Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// convertButton
			// 
			this.convertButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.convertButton.Location = new System.Drawing.Point(12, 226);
			this.convertButton.Name = "convertButton";
			this.convertButton.Size = new System.Drawing.Size(93, 23);
			this.convertButton.TabIndex = 1;
			this.convertButton.Text = "Convert to XML";
			this.convertButton.UseVisualStyleBackColor = true;
			this.convertButton.Click += new System.EventHandler(this.convertButton_Click);
			// 
			// GameConfigEditorDialog
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(384, 261);
			this.Controls.Add(this.convertButton);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.tabControl1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "GameConfigEditorDialog";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "Game Config Editor";
			this.Load += new System.EventHandler(this.GameConfigEditorDialog_Load);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			this.tabPage3.ResumeLayout(false);
			this.tabPage3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.variableValue)).EndInit();
			this.tabPage4.ResumeLayout(false);
			this.tabPage4.PerformLayout();
			this.tabPage5.ResumeLayout(false);
			this.tabPage5.PerformLayout();
			this.tabPage6.ResumeLayout(false);
			this.tabPage6.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox gameName;
		private System.Windows.Forms.TextBox gameDescription;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button browseScriptButton;
		private System.Windows.Forms.TextBox objectScriptBox;
		private System.Windows.Forms.Label label23;
		private System.Windows.Forms.TextBox objectNameBox;
		private System.Windows.Forms.Label label22;
		private System.Windows.Forms.Button objectDeleteButton;
		private System.Windows.Forms.Button objectAddButton;
		private System.Windows.Forms.ListBox objectListBox;
		private System.Windows.Forms.CheckBox objectForceLoad;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox variableName;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button variableDeleteButton;
		private System.Windows.Forms.Button variableAddButton;
		private System.Windows.Forms.ListBox variableListBox;
		private System.Windows.Forms.NumericUpDown variableValue;
		private System.Windows.Forms.TabPage tabPage4;
		private System.Windows.Forms.Button sfxBrowseButton;
		private System.Windows.Forms.TextBox sfxFileBox;
		private System.Windows.Forms.Label label24;
		private System.Windows.Forms.TextBox sfxNameBox;
		private System.Windows.Forms.Label label25;
		private System.Windows.Forms.Button sfxDeleteButton;
		private System.Windows.Forms.Button sfxAddButton;
		private System.Windows.Forms.ListBox sfxListBox;
		private System.Windows.Forms.TabPage tabPage5;
		private System.Windows.Forms.TextBox playerName;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button playerDeleteButton;
		private System.Windows.Forms.Button playerAddButton;
		private System.Windows.Forms.ListBox playerListBox;
		private System.Windows.Forms.TabPage tabPage6;
		private System.Windows.Forms.TextBox stageName;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Button stageDeleteButton;
		private System.Windows.Forms.Button stageAddButton;
		private System.Windows.Forms.ListBox stageListBox;
		private System.Windows.Forms.ComboBox stageCategory;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox stageAct;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox stageFolder;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.CheckBox stageHighlight;
		private System.Windows.Forms.Button stageBrowseButton;
		private System.Windows.Forms.Button convertButton;
	}
}