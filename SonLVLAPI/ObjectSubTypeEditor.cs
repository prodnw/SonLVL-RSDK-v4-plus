using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace SonicRetro.SonLVL.API
{
	public class SubTypeControl : UserControl
	{
		internal ImageList imageList1;
		private IContainer components;
		internal ListView listView1;
		private byte id;
		private NumericUpDown numericUpDown1;

		public byte value { get; private set; }
		private IWindowsFormsEditorService edSvc;

		public SubTypeControl(byte id, byte val, IWindowsFormsEditorService edSvc)
		{
			this.id = id;
			value = val;
			this.edSvc = edSvc;
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.listView1 = new System.Windows.Forms.ListView();
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
			this.SuspendLayout();
			// 
			// imageList1
			// 
			this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imageList1.ImageSize = new System.Drawing.Size(32, 32);
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// listView1
			// 
			this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listView1.HideSelection = false;
			this.listView1.LargeImageList = this.imageList1;
			this.listView1.Location = new System.Drawing.Point(0, 0);
			this.listView1.MultiSelect = false;
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(150, 130);
			this.listView1.TabIndex = 1;
			this.listView1.TileSize = new System.Drawing.Size(120, 48);
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = System.Windows.Forms.View.Tile;
			this.listView1.ItemActivate += new System.EventHandler(this.listView1_ItemActivate);
			this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
			// 
			// numericUpDown1
			// 
			this.numericUpDown1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.numericUpDown1.Location = new System.Drawing.Point(0, 130);
			this.numericUpDown1.Maximum = new decimal(new int[] {
			255,
			0,
			0,
			0});
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new System.Drawing.Size(150, 20);
			this.numericUpDown1.TabIndex = 2;
			this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
			// 
			// SubTypeControl
			// 
			this.Controls.Add(this.listView1);
			this.Controls.Add(this.numericUpDown1);
			this.Name = "SubTypeControl";
			this.Load += new System.EventHandler(this.SubTypeControl_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SubTypeControl_KeyDown);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
			this.ResumeLayout(false);

		}

		private void SubTypeControl_Load(object sender, EventArgs e)
		{
			listView1.BeginUpdate();
			listView1.Items.Clear();
			imageList1.Images.Clear();
			if (id < LevelData.ObjTypes.Count)
				foreach (byte item in LevelData.ObjTypes[id].Subtypes)
				{
					imageList1.Images.Add(LevelData.ObjTypes[id].SubtypeImage(item).GetBitmap().ToBitmap(LevelData.BmpPal).Resize(imageList1.ImageSize));
					listView1.Items.Add(new ListViewItem(LevelData.ObjTypes[id].SubtypeName(item), imageList1.Images.Count - 1) { Tag = item, Selected = item == value });
				}
			listView1.EndUpdate();
			numericUpDown1.Value = value;
		}

		private void listView1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (listView1.SelectedIndices.Count == 0) return;
			numericUpDown1.Value = value = (byte)listView1.SelectedItems[0].Tag;
		}

		private void listView1_ItemActivate(object sender, EventArgs e)
		{
			edSvc.CloseDropDown();
		}

		private void numericUpDown1_ValueChanged(object sender, EventArgs e)
		{
			value = (byte)numericUpDown1.Value;
		}

		private void SubTypeControl_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter) edSvc.CloseDropDown();
		}
	}

	[System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
	public class SubTypeEditor : UITypeEditor
	{
		public SubTypeEditor()
		{
		}

		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.DropDown;
		}

		// Displays the UI for value selection.
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			ObjectEntry entry = null;

			if (context.Instance is Entry[] entries)
			{
				// If we're selecting multiple objects, let's check a bit more, only show it if all selected objects are of the same type..
				var oes = Array.ConvertAll(entries, ent => (ObjectEntry)ent);
				if (oes.Length < 1 || oes.Any((obj) => obj.Type != oes[0].Type)) return value; // First check *should* never be true by this point, but just in case..
				entry = oes[0];
			}
			else if (context.Instance is ObjectEntry oe)
				entry = oe;
			else
				return value;

			// Worst case scenario let's just show a blank Subtype form, we don't need to hide it altogether
			//if (!LevelData.ObjTypes.ContainsKey(((ObjectEntry)context.Instance).ID)) return value;

			// Uses the IWindowsFormsEditorService to display a 
			// drop-down UI in the Properties window.
			IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
			if (edSvc != null)
			{
				// Display an angle selection control and retrieve the value.
				// If we're selecting objects with differing Property Values, just show the value of the first one
				byte sub = (byte)(value ?? entry.PropertyValue);
				SubTypeControl SubTypeControl = new SubTypeControl(entry.Type, sub, edSvc);
				edSvc.DropDownControl(SubTypeControl);
				return SubTypeControl.value;
			}
			return value;
		}

		public override bool GetPaintValueSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		public override void PaintValue(PaintValueEventArgs e)
		{
			if (e.Value == null) return;

			ObjectEntry entry = null;

			if (e.Context.Instance is Entry[] entries)
			{
				// If we're selecting multiple objects, let's check a bit more..
				var oes = Array.ConvertAll(entries, ent => (ObjectEntry)ent);
				if (oes.Length < 1 || oes.Any((obj) => obj.Type != oes[0].Type || obj.PropertyValue != oes[0].PropertyValue)) return; // First and last check *should* never be true by this point, but just in case..
				entry = oes[0];
			}
			else if (e.Context.Instance is ObjectEntry oe)
				entry = oe;
			else
				return;

			if (entry.Type >= LevelData.ObjTypes.Count) return;
			byte sub = (byte)e.Value;
			e.Graphics.DrawImage(LevelData.ObjTypes[entry.Type].SubtypeImage(sub).GetBitmap().ToBitmap(LevelData.BmpPal).Resize(e.Bounds.Size), e.Bounds);
		}

		public override bool IsDropDownResizable { get { return true; } }
	}
}
