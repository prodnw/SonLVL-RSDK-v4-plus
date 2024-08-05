namespace SonicRetro.SonLVL
{
	partial class GoToDialog
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
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.gotoEntity = new System.Windows.Forms.RadioButton();
            this.gotoPosition = new System.Windows.Forms.RadioButton();
            this.entityPos = new System.Windows.Forms.NumericUpDown();
            this.xLabel = new System.Windows.Forms.Label();
            this.xpos = new System.Windows.Forms.NumericUpDown();
            this.ypos = new System.Windows.Forms.NumericUpDown();
            this.yLabel = new System.Windows.Forms.Label();
            this.entityCountLabel = new System.Windows.Forms.Label();
            this.widthLabel = new System.Windows.Forms.Label();
            this.heightLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.entityPos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xpos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ypos)).BeginInit();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(57, 209);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(150, 44);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "&OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(234, 209);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(150, 44);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "&Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // gotoEntity
            // 
            this.gotoEntity.AutoSize = true;
            this.gotoEntity.Location = new System.Drawing.Point(20, 29);
            this.gotoEntity.Name = "gotoEntity";
            this.gotoEntity.Size = new System.Drawing.Size(211, 29);
            this.gotoEntity.TabIndex = 2;
            this.gotoEntity.Text = "Go To Entity Pos:";
            this.gotoEntity.UseVisualStyleBackColor = true;
            this.gotoEntity.CheckedChanged += new System.EventHandler(this.gotoEntity_CheckedChanged);
            // 
            // gotoPosition
            // 
            this.gotoPosition.AutoSize = true;
            this.gotoPosition.Checked = true;
            this.gotoPosition.Location = new System.Drawing.Point(20, 74);
            this.gotoPosition.Name = "gotoPosition";
            this.gotoPosition.Size = new System.Drawing.Size(191, 29);
            this.gotoPosition.TabIndex = 3;
            this.gotoPosition.TabStop = true;
            this.gotoPosition.Text = "Go To Position:";
            this.gotoPosition.UseVisualStyleBackColor = true;
            this.gotoPosition.CheckedChanged += new System.EventHandler(this.gotoPosition_CheckedChanged);
            // 
            // entityPos
            // 
            this.entityPos.Enabled = false;
            this.entityPos.Location = new System.Drawing.Point(237, 29);
            this.entityPos.Minimum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.entityPos.Name = "entityPos";
            this.entityPos.Size = new System.Drawing.Size(120, 31);
            this.entityPos.TabIndex = 4;
            this.entityPos.Value = new decimal(new int[] {
            32,
            0,
            0,
            0});
            // 
            // xLabel
            // 
            this.xLabel.AutoSize = true;
            this.xLabel.Location = new System.Drawing.Point(74, 117);
            this.xLabel.Name = "xLabel";
            this.xLabel.Size = new System.Drawing.Size(32, 25);
            this.xLabel.TabIndex = 5;
            this.xLabel.Text = "X:";
            // 
            // xpos
            // 
            this.xpos.Location = new System.Drawing.Point(124, 115);
            this.xpos.Name = "xpos";
            this.xpos.Size = new System.Drawing.Size(120, 31);
            this.xpos.TabIndex = 7;
            // 
            // ypos
            // 
            this.ypos.Location = new System.Drawing.Point(124, 152);
            this.ypos.Name = "ypos";
            this.ypos.Size = new System.Drawing.Size(120, 31);
            this.ypos.TabIndex = 9;
            // 
            // yLabel
            // 
            this.yLabel.AutoSize = true;
            this.yLabel.Location = new System.Drawing.Point(74, 154);
            this.yLabel.Name = "yLabel";
            this.yLabel.Size = new System.Drawing.Size(33, 25);
            this.yLabel.TabIndex = 8;
            this.yLabel.Text = "Y:";
            // 
            // entityCountLabel
            // 
            this.entityCountLabel.AutoSize = true;
            this.entityCountLabel.Location = new System.Drawing.Point(362, 35);
            this.entityCountLabel.Name = "entityCountLabel";
            this.entityCountLabel.Size = new System.Drawing.Size(54, 25);
            this.entityCountLabel.TabIndex = 10;
            this.entityCountLabel.Text = " / 32";
            // 
            // widthLabel
            // 
            this.widthLabel.AutoSize = true;
            this.widthLabel.Location = new System.Drawing.Point(249, 117);
            this.widthLabel.Name = "widthLabel";
            this.widthLabel.Size = new System.Drawing.Size(42, 25);
            this.widthLabel.TabIndex = 11;
            this.widthLabel.Text = " / 0";
            // 
            // heightLabel
            // 
            this.heightLabel.AutoSize = true;
            this.heightLabel.Location = new System.Drawing.Point(249, 154);
            this.heightLabel.Name = "heightLabel";
            this.heightLabel.Size = new System.Drawing.Size(42, 25);
            this.heightLabel.TabIndex = 12;
            this.heightLabel.Text = " / 0";
            // 
            // GoToDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 275);
            this.Controls.Add(this.heightLabel);
            this.Controls.Add(this.widthLabel);
            this.Controls.Add(this.entityCountLabel);
            this.Controls.Add(this.ypos);
            this.Controls.Add(this.yLabel);
            this.Controls.Add(this.xpos);
            this.Controls.Add(this.xLabel);
            this.Controls.Add(this.entityPos);
            this.Controls.Add(this.gotoPosition);
            this.Controls.Add(this.gotoEntity);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GoToDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Go To...";
            ((System.ComponentModel.ISupportInitialize)(this.entityPos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xpos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ypos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		internal System.Windows.Forms.RadioButton gotoEntity;
		internal System.Windows.Forms.RadioButton gotoPosition;
		internal System.Windows.Forms.NumericUpDown entityPos;
		private System.Windows.Forms.Label xLabel;
		private System.Windows.Forms.Label yLabel;
		internal System.Windows.Forms.NumericUpDown xpos;
		internal System.Windows.Forms.NumericUpDown ypos;
		internal System.Windows.Forms.Label entityCountLabel;
		internal System.Windows.Forms.Label widthLabel;
		internal System.Windows.Forms.Label heightLabel;
	}
}