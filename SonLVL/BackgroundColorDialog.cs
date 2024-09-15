using System;
using System.Drawing;
using System.Windows.Forms;
using SonicRetro.SonLVL.API;

namespace SonicRetro.SonLVL
{
	public partial class BackgroundColorDialog : Form
	{
		Graphics PalettePanelGfx;
		Bitmap palette;
		Point selection;

		public BackgroundColorDialog()
		{
			InitializeComponent();

			PalettePanelGfx = palettePanel.CreateGraphics();
			PalettePanelGfx.SetOptions();

			BitmapBits bitmap = new BitmapBits(512, 512);
			if (LevelData.NewPalette[0] == Color.Empty)
			{
				bitmap.FillRectangle(0, 0, 0, 512, 512);
				palette = bitmap.ToBitmap(new Color[] { Color.Black });
			}
			else
			{
				for (int y = 0; y < 16; y++)
					for (int x = 0; x < 16; x++)
						bitmap.FillRectangle((byte)((y * 16) + x), x * 32, y * 32, 32, 32);
				palette = bitmap.ToBitmap(LevelData.NewPalette);
			}
		}

		private void DrawPalette()
		{
			PalettePanelGfx.DrawImage(palette, 0, 0, 256, 256);
			PalettePanelGfx.DrawRectangle(Pens.Yellow, selection.X * 16, selection.Y * 16, 15, 15);
			if (!useLevelColor.Checked)
				PalettePanelGfx.FillRectangle(new SolidBrush(Color.FromArgb(120, Color.Gray)), 0, 0, 256, 256);
		}
		
		private void palettePanel_MouseDown(object sender, MouseEventArgs e)
		{
			if (!useLevelColor.Checked) return;

			selection = e.Location;
			selection.X /= 16; selection.Y = Math.Min(selection.Y / 16, 15);
			index.Value = selection.X + (selection.Y * 16);
			DrawPalette();
		}

		private void palettePanel_Paint(object sender, PaintEventArgs e)
		{
			DrawPalette();
		}
	
		private void useLevelColor_CheckedChanged(object sender, EventArgs e)
		{
			index.Enabled = useLevelColor.Checked;
			DrawPalette();
		}

		private void index_ValueChanged(object sender, EventArgs e)
		{
			selection.X = (int)index.Value & 15;
			selection.Y = (int)index.Value / 16;
			DrawPalette();
		}

		private void constantColor_Click(object sender, EventArgs e)
		{
			ColorDialog a = new ColorDialog
			{
				AllowFullOpen = true,
				AnyColor = true,
				FullOpen = true,
				SolidColorOnly = true,
				Color = constantColorBox.BackColor
			};
			if (a.ShowDialog() == DialogResult.OK)
				constantColorBox.BackColor = a.Color;
		}

		private void useConstantColor_CheckedChanged(object sender, EventArgs e)
		{
			colorChange.Enabled = useConstantColor.Checked;
			constantColorOverlay.Visible = !useConstantColor.Checked;
		}
	}
}
