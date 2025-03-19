namespace SonicRetro.SonLVL.GUI
{
	partial class AddGroupDialog
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
            this.XDist = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.YDist = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.Rows = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.Columns = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.XDist)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.YDist)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Rows)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Columns)).BeginInit();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(15, 148);
            this.okButton.Margin = new System.Windows.Forms.Padding(4);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(100, 28);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "&OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(123, 148);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(4);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(100, 28);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "&Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // XDist
            // 
            this.XDist.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.XDist.Location = new System.Drawing.Point(104, 15);
            this.XDist.Margin = new System.Windows.Forms.Padding(4);
            this.XDist.Maximum = new decimal(new int[] {
            4095,
            0,
            0,
            0});
            this.XDist.Name = "XDist";
            this.XDist.Size = new System.Drawing.Size(63, 22);
            this.XDist.TabIndex = 2;
            this.XDist.ValueChanged += new System.EventHandler(this.value_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 17);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "X distance:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 49);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Y distance:";
            // 
            // YDist
            // 
            this.YDist.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.YDist.Location = new System.Drawing.Point(104, 47);
            this.YDist.Margin = new System.Windows.Forms.Padding(4);
            this.YDist.Maximum = new decimal(new int[] {
            4095,
            0,
            0,
            0});
            this.YDist.Name = "YDist";
            this.YDist.Size = new System.Drawing.Size(63, 22);
            this.YDist.TabIndex = 4;
            this.YDist.ValueChanged += new System.EventHandler(this.value_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 81);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "Rows:";
            // 
            // Rows
            // 
            this.Rows.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Rows.Location = new System.Drawing.Point(104, 79);
            this.Rows.Margin = new System.Windows.Forms.Padding(4);
            this.Rows.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Rows.Name = "Rows";
            this.Rows.Size = new System.Drawing.Size(63, 22);
            this.Rows.TabIndex = 6;
            this.Rows.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Rows.ValueChanged += new System.EventHandler(this.value_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 113);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 16);
            this.label4.TabIndex = 9;
            this.label4.Text = "Columns:";
            // 
            // Columns
            // 
            this.Columns.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Columns.Location = new System.Drawing.Point(104, 111);
            this.Columns.Margin = new System.Windows.Forms.Padding(4);
            this.Columns.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Columns.Name = "Columns";
            this.Columns.Size = new System.Drawing.Size(63, 22);
            this.Columns.TabIndex = 8;
            this.Columns.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Columns.ValueChanged += new System.EventHandler(this.value_ValueChanged);
            // 
            // AddGroupDialog
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(239, 191);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Columns);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Rows);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.YDist);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.XDist);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddGroupDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "AddGroupDialog";
            ((System.ComponentModel.ISupportInitialize)(this.XDist)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.YDist)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Rows)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Columns)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		internal System.Windows.Forms.NumericUpDown XDist;
		internal System.Windows.Forms.NumericUpDown YDist;
		internal System.Windows.Forms.NumericUpDown Rows;
		private System.Windows.Forms.Label label4;
		internal System.Windows.Forms.NumericUpDown Columns;
	}
}

