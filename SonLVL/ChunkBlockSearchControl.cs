using SonicRetro.SonLVL.API;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace SonicRetro.SonLVL
{
	public partial class ChunkBlockSearchControl : UserControl
	{
		public ChunkBlockSearchControl()
		{
			InitializeComponent();
			solidity1.SelectedIndex = solidity2.SelectedIndex = 0;
		}

		public bool Hexadecimal
		{
			get => block.Hexadecimal;
			set => block.Hexadecimal = value;
		}

		bool initializing;

		public void UpdateStuff()
		{
			initializing = true;
			block.Maximum = LevelData.NewTiles.Length - 1;
			blockList.Images = LevelData.NewTileBmps;
			blockList.ChangeSize();
			blockList.SelectedIndex = block.Value >= LevelData.NewTiles.Length ? -1 : (int)block.Value;
			initializing = false;
		}

		private void block_ValueChanged(object sender, EventArgs e)
		{
			if (!initializing)
			{
				initializing = true;
				blockList.SelectedIndex = block.Value >= LevelData.NewTiles.Length ? -1 : (int)block.Value;
				initializing = false;
			}
		}

		private void blockList_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!initializing && blockList.SelectedIndex > -1)
				block.Value = blockList.SelectedIndex;
		}

		private void searchBlock_CheckedChanged(object sender, EventArgs e)
		{
			blockList.Enabled = block.Enabled = searchBlock.Checked;
		}

		[Browsable(false)]
		public bool? XFlip
		{
			get
			{
				if (xFlip.CheckState == CheckState.Indeterminate)
					return null;
				else
					return xFlip.Checked;
			}
		}

		[Browsable(false)]
		public bool? YFlip
		{
			get
			{
				if (yFlip.CheckState == CheckState.Indeterminate)
					return null;
				else
					return yFlip.Checked;
			}
		}

		[Browsable(false)]
		public RSDKv3_4.Tiles128x128.Block.Tile.Solidities? Solidity1
		{
			get
			{
				if (solidity1.SelectedIndex == 0)
					return null;
				else
					return (RSDKv3_4.Tiles128x128.Block.Tile.Solidities)(solidity1.SelectedIndex - 1);
			}
		}

		[Browsable(false)]
		public RSDKv3_4.Tiles128x128.Block.Tile.Solidities? Solidity2
		{
			get
			{
				if (solidity2.SelectedIndex == 0)
					return null;
				else
					return (RSDKv3_4.Tiles128x128.Block.Tile.Solidities)(solidity2.SelectedIndex - 1);
			}
		}

		[Browsable(false)]
		public RSDKv3_4.Tiles128x128.Block.Tile.VisualPlanes? Plane
		{
			get
			{
				if (highPlane.CheckState == CheckState.Indeterminate)
					return null;
				else
					return highPlane.Checked ? RSDKv3_4.Tiles128x128.Block.Tile.VisualPlanes.High : RSDKv3_4.Tiles128x128.Block.Tile.VisualPlanes.Low;
			}
		}

		[Browsable(false)]
		public ushort? Block
		{
			get
			{
				if (searchBlock.Checked)
					return (ushort)block.Value;
				else
					return null;
			}
		}
	}
}
