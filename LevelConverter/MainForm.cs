using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using SonicRetro.SonLVL.API;
using System.Linq;

namespace SonicRetro.SonLVL.LevelConverter
{
	public partial class MainForm : Form
	{
		public static MainForm Instance { get; private set; }

		public MainForm()
		{
			Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
			Instance = this;
			LevelData.LogEvent += new LevelData.LogEventHandler(Log);
			InitializeComponent();
		}

		void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
		{
			Log(e.Exception.ToString().Split(new string[] { Environment.NewLine }, StringSplitOptions.None));
			File.WriteAllLines("LevelConverter.log", LogFile.ToArray());
			using (ErrorDialog ed = new ErrorDialog("Unhandled Exception " + e.Exception.GetType().Name + "\nLog file has been saved.\n\nDo you want to try to continue running?", true))
			{
				if (ed.ShowDialog(this) == System.Windows.Forms.DialogResult.Cancel)
					Close();
			}
		}

		internal void Log(params string[] lines)
		{
			LogFile.AddRange(lines);
		}

		internal List<string> LogFile = new List<string>();
		string Dir = Environment.CurrentDirectory;
		bool ConvertKnownObjs = true;
		bool DontChangeObjects;
		List<string> Levels;

		private void fileSelector1_FileNameChanged(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(fileSelector1.FileName)) return;
			if (!File.Exists(fileSelector1.FileName)) return;
			try
			{
				LevelData.LoadGame(fileSelector1.FileName);
			}
			catch (ArgumentException)
			{
				return;
			}
			catch (IOException)
			{
				return;
			}
			catch (UnauthorizedAccessException)
			{
				return;
			}
			catch (NotSupportedException)
			{
				return;
			}
			catch (System.Security.SecurityException)
			{
				return;
			}
			comboBox1.Items.Clear();
			Levels = new List<string>();
			foreach (KeyValuePair<string, LevelInfo> item in LevelData.Game.Levels)
			{
				Levels.Add(item.Key);
				comboBox1.Items.Add(LevelData.Game.GetLevelInfo(item.Key).DisplayName);
			}
		}

		private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBox1.SelectedIndex == -1 || comboBox2.SelectedIndex == -1)
				button1.Enabled = false;
			else
				button1.Enabled = true;
			button2.Enabled = File.Exists(fileSelector1.FileName) & comboBox2.SelectedIndex != -1;
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			ConvertKnownObjs = checkBox1.Checked;
		}

		private void checkBox2_CheckedChanged(object sender, EventArgs e)
		{
			DontChangeObjects = checkBox2.Checked;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			ConvertLevel();
			string level = (string)comboBox1.SelectedItem;
			string OutDir = Path.Combine(Dir, level);
			System.Diagnostics.Process.Start(OutDir);
		}

		private void ConvertLevel()
		{
			string level = (string)comboBox1.SelectedItem;
			string OutDir = Path.Combine(Dir, level);
			if (Directory.Exists(OutDir)) { Directory.Delete(OutDir, true); System.Threading.Thread.Sleep(100); }
			Directory.CreateDirectory(OutDir);
			EngineVersion OutFmt = EngineVersion.Invalid;
			switch (comboBox2.SelectedIndex)
			{
				case 0:
					OutFmt = EngineVersion.V4;
					break;
				case 1:
					OutFmt = EngineVersion.S2;
					break;
				case 2:
					OutFmt = EngineVersion.S3K;
					break;
				case 3:
					OutFmt = EngineVersion.SKC;
					break;
				case 4:
					OutFmt = EngineVersion.SCDPC;
					break;
				case 5:
					OutFmt = EngineVersion.S2NA;
					break;
			}
			LevelData.LoadLevel(Levels[comboBox1.SelectedIndex], false);
			if (LevelData.Level.LayoutFormat == EngineVersion.S2 | LevelData.Level.LayoutFormat == EngineVersion.SCDPC)
			{
				int xend = 0;
				int yend = 0;
				for (int y = 0; y < LevelData.FGHeight; y++)
					for (int x = 0; x < LevelData.FGWidth; x++)
						if (LevelData.Scene.layout[y][x] > 0)
						{
							xend = Math.Max(xend, x);
							yend = Math.Max(yend, y);
						}
				xend++;
				yend++;
				ushort[,] tmp = new ushort[xend, yend];
				for (int y = 0; y < yend; y++)
					for (int x = 0; x < xend; x++)
						tmp[x, y] = LevelData.Scene.layout[y][x];
				LevelData.Layout.FGLayout = tmp;
				xend = 0;
				yend = 0;
				for (int y = 0; y < LevelData.BGHeight; y++)
					for (int x = 0; x < LevelData.BGWidth; x++)
						if (LevelData.Background.layers[bglayer].layout[y][x] > 0)
						{
							xend = Math.Max(xend, x);
							yend = Math.Max(yend, y);
						}
				xend++;
				yend++;
				tmp = new ushort[xend, yend];
				for (int y = 0; y < yend; y++)
					for (int x = 0; x < xend; x++)
						tmp[x, y] = LevelData.Background.layers[bglayer].layout[y][x];
				LevelData.Layout.BGLayout = tmp;
			}
			GameInfo Output = new GameInfo() { EngineVersion = OutFmt };
			LevelInfo Level = new LevelInfo();
			Output.Levels = new Dictionary<string, LevelInfo>() { { level, Level } };
			bool LE = LevelData.littleendian;
			LevelData.littleendian = false;
			switch (OutFmt)
			{
				case EngineVersion.SCDPC:
				case EngineVersion.SKC:
					LevelData.littleendian = true;
					break;
			}
			CompressionType cmp = CompressionType.Uncompressed;
			List<byte> tmp2 = new List<byte>();
			if (OutFmt != EngineVersion.SCDPC)
			{
				switch (OutFmt)
				{
					case EngineVersion.V4:
					case EngineVersion.S2NA:
						cmp = CompressionType.Nemesis;
						break;
					case EngineVersion.S2:
						cmp = CompressionType.Kosinski;
						break;
					case EngineVersion.S3K:
					case EngineVersion.SKC:
						cmp = CompressionType.KosinskiM;
						break;
					default:
						cmp = CompressionType.Uncompressed;
						break;
				}
				foreach (byte[] tile in LevelData.Tiles)
					tmp2.AddRange(tile);
				Compression.Compress(tmp2.ToArray(), Path.Combine(OutDir, "Tiles.bin"), cmp);
			}
			else
			{
				List<ushort>[] tilepals = new List<ushort>[4];
				for (int i = 0; i < 4; i++)
					tilepals[i] = new List<ushort>();
				foreach (Block blk in LevelData.NewTiles)
					for (int y = 0; y < 2; y++)
						for (int x = 0; x < 2; x++)
							if (!tilepals[blk.Tiles[x, y].Palette].Contains(blk.Tiles[x, y].Tile))
								tilepals[blk.Tiles[x, y].Palette].Add(blk.Tiles[x, y].Tile);
				foreach (Block blk in LevelData.NewTiles)
					for (int y = 0; y < 2; y++)
						for (int x = 0; x < 2; x++)
						{
							byte pal = blk.Tiles[x, y].Palette;
							int c = 0;
							for (int i = pal - 1; i >= 0; i--)
								c += tilepals[i].Count;
							blk.Tiles[x, y].Tile = (ushort)(tilepals[pal].IndexOf(blk.Tiles[x, y].Tile) + c);
						}
				List<byte[]> tiles = new List<byte[]>();
				for (int p = 0; p < 4; p++)
					foreach (ushort item in tilepals[p])
						if (LevelData.Tiles[item] != null)
							tiles.Add(LevelData.Tiles[item]);
						else
							tiles.Add(new byte[32]);
				LevelData.Tiles.Clear();
				LevelData.Tiles.AddFile(tiles, -1);
				tmp2 = new List<byte> { 0x53, 0x43, 0x52, 0x4C };
				tmp2.AddRange(ByteConverter.GetBytes(0x18 + (LevelData.NewTiles.Length * 4) + (LevelData.NewTiles.Length * 32)));
				tmp2.AddRange(ByteConverter.GetBytes(LevelData.NewTiles.Length));
				tmp2.AddRange(ByteConverter.GetBytes(0x18 + (LevelData.NewTiles.Length * 4)));
				for (int i = 0; i < 4; i++)
					tmp2.AddRange(ByteConverter.GetBytes((ushort)tilepals[i].Count));
				for (int i = 0; i < LevelData.NewTiles.Length; i++)
				{
					tmp2.AddRange(ByteConverter.GetBytes((ushort)8));
					tmp2.AddRange(ByteConverter.GetBytes((ushort)8));
				}
				foreach (byte[] tile in LevelData.Tiles)
					tmp2.AddRange(tile);
				Compression.Compress(tmp2.ToArray(), Path.Combine(OutDir, "Tiles.bin"), CompressionType.SZDD);
			}
			Level.Tiles = new[] { new SonicRetro.SonLVL.API.FileInfo("Tiles.bin") };
			tmp2 = new List<byte>();
			if (OutFmt == EngineVersion.SKC)
				LevelData.littleendian = false;
			foreach (Block b in LevelData.NewTiles)
			{
				tmp2.AddRange(b.GetBytes());
			}
			if (OutFmt == EngineVersion.SKC)
				LevelData.littleendian = true;
			switch (OutFmt)
			{
				case EngineVersion.V4:
					cmp = CompressionType.Enigma;
					break;
				case EngineVersion.S2:
				case EngineVersion.S3K:
				case EngineVersion.SKC:
					cmp = CompressionType.Kosinski;
					break;
				default:
					cmp = CompressionType.Uncompressed;
					break;
			}
			Compression.Compress(tmp2.ToArray(), Path.Combine(OutDir, "Blocks.bin"), cmp);
			Level.Blocks = new[] { new SonicRetro.SonLVL.API.FileInfo("Blocks.bin") };
			byte chunktypes = 0;
			int chunksz = 16;
			switch (LevelData.Level.ChunkFormat)
			{
				case EngineVersion.V4:
				case EngineVersion.SCDPC:
					chunktypes = 0;
					break;
				case EngineVersion.S2:
				case EngineVersion.S2NA:
				case EngineVersion.S3K:
				case EngineVersion.SKC:
					chunktypes = 1;
					break;
			}
			switch (OutFmt)
			{
				case EngineVersion.S2:
				case EngineVersion.S2NA:
				case EngineVersion.S3K:
				case EngineVersion.SKC:
					chunktypes |= 2;
					chunksz = 8;
					break;
			}
			LevelData.Level.ChunkFormat = OutFmt;
			List<Chunk> tmpchnk = new List<Chunk>();
			switch (chunktypes)
			{
				case 0: // S1 -> S1
					tmpchnk = LevelData.Chunks.ToList();
					tmpchnk.RemoveAt(0);
					break;
				case 1: // S2 -> S1
					tmpchnk = new List<Chunk>() { new Chunk() };
					List<int> chnks = new List<int>() { 0 };
					int chnk;
					ushort[,] newFG1 = new ushort[(int)Math.Ceiling(LevelData.FGWidth / 2d), (int)Math.Ceiling(LevelData.FGHeight / 2d)];
					LevelData.Layout.FGLoop = new bool[newFG1.GetLength(0), newFG1.GetLength(1)];
					for (int y = 0; y < LevelData.FGHeight; y += 2)
					{
						for (int x = 0; x < LevelData.FGWidth; x += 2)
						{
							chnk = LevelData.Scene.layout[y][x];
							chnk |= (x + 1 < LevelData.FGWidth ? LevelData.Scene.layout[y][x + 1] : 0) << 8;
							chnk |= (y + 1 < LevelData.FGHeight ? LevelData.Scene.layout[y + 1][x] : 0) << 16;
							chnk |= (x + 1 < LevelData.FGWidth & y + 1 < LevelData.FGHeight ? LevelData.Scene.layout[y + 1][x + 1] : 0) << 24;
							if (chnks.IndexOf(chnk) > -1)
								newFG1[x / 2, y / 2] = (byte)chnks.IndexOf(chnk);
							else
							{
								newFG1[x / 2, y / 2] = (byte)chnks.Count;
								Chunk newchnk = new Chunk();
								for (int i = 0; i < 8; i++)
								{
									for (int j = 0; j < 8; j++)
									{
										newchnk.tiles[j][i].tileIndex = LevelData.NewChunks.chunkList[chnk & 0xFF].tiles[j][i].tileIndex;
										newchnk.tiles[j].Solid1 = LevelData.NewChunks.chunkList[chnk & 0xFF][i].tileIndexs[i, j].Solid1;
										newchnk.tiles[j].XFlip = LevelData.NewChunks.chunkList[chnk & 0xFF][i].tileIndexs[i, j].XFlip;
										newchnk.tiles[j].YFlip = LevelData.NewChunks.chunkList[chnk & 0xFF][i].tileIndexs[i, j].YFlip;
									}
								}
								for (int i = 0; i < 8; i++)
								{
									for (int j = 0; j < 8; j++)
									{
										newchnk.tiles[j][i + 8].tileIndex = LevelData.NewChunks.chunkList[(chnk >> 8) & 0xFF].tiles[j][i].tileIndex;
										newchnk.tiles[j].Solid1 = LevelData.NewChunks.chunkList[(chnk >> 8) & 0xFF][i + 8].tileIndexs[i, j].Solid1;
										newchnk.tiles[j].XFlip = LevelData.NewChunks.chunkList[(chnk >> 8) & 0xFF][i + 8].tileIndexs[i, j].XFlip;
										newchnk.tiles[j].YFlip = LevelData.NewChunks.chunkList[(chnk >> 8) & 0xFF][i + 8].tileIndexs[i, j].YFlip;
									}
								}
								for (int i = 0; i < 8; i++)
								{
									for (int j = 0; j < 8; j++)
									{
										newchnk.tiles[j + 8][i].tileIndex = LevelData.NewChunks.chunkList[(chnk >> 16) & 0xFF].tiles[j][i].tileIndex;
										newchnk.tiles[j + 8].Solid1 = LevelData.NewChunks.chunkList[(chnk >> 16) & 0xFF][i].tileIndexs[i, j].Solid1;
										newchnk.tiles[j + 8].XFlip = LevelData.NewChunks.chunkList[(chnk >> 16) & 0xFF][i].tileIndexs[i, j].XFlip;
										newchnk.tiles[j + 8].YFlip = LevelData.NewChunks.chunkList[(chnk >> 16) & 0xFF][i].tileIndexs[i, j].YFlip;
									}
								}
								for (int i = 0; i < 8; i++)
								{
									for (int j = 0; j < 8; j++)
									{
										newchnk.tiles[j + 8][i + 8].tileIndex = LevelData.NewChunks.chunkList[(chnk >> 24) & 0xFF].tiles[j][i].tileIndex;
										newchnk.tiles[j + 8].Solid1 = LevelData.NewChunks.chunkList[(chnk >> 24) & 0xFF][i + 8].tileIndexs[i, j].Solid1;
										newchnk.tiles[j + 8].XFlip = LevelData.NewChunks.chunkList[(chnk >> 24) & 0xFF][i + 8].tileIndexs[i, j].XFlip;
										newchnk.tiles[j + 8].YFlip = LevelData.NewChunks.chunkList[(chnk >> 24) & 0xFF][i + 8].tileIndexs[i, j].YFlip;
									}
								}
								tmpchnk.Add(newchnk);
								chnks.Add(chnk);
							}
						}
					}
					LevelData.Layout.FGLayout = newFG1;
					ushort[,] newBG1 = new ushort[(int)Math.Ceiling(LevelData.BGWidth / 2d), (int)Math.Ceiling(LevelData.BGHeight / 2d)];
					LevelData.Layout.BGLoop = new bool[newBG1.GetLength(0), newBG1.GetLength(1)];
					for (int y = 0; y < LevelData.BGHeight; y += 2)
					{
						for (int x = 0; x < LevelData.BGWidth; x += 2)
						{
							chnk = LevelData.Background.layers[bglayer].layout[y][x];
							chnk |= (x + 1 < LevelData.BGWidth ? LevelData.Background.layers[bglayer].layout[y][x + 1] : 0) << 8;
							chnk |= (y + 1 < LevelData.BGHeight ? LevelData.Background.layers[bglayer].layout[y + 1][x] : 0) << 16;
							chnk |= (x + 1 < LevelData.BGWidth & y + 1 < LevelData.BGHeight ? LevelData.Background.layers[bglayer].layout[y + 1][x + 1] : 0) << 24;
							if (chnks.IndexOf(chnk) > -1)
								newBG1[x / 2, y / 2] = (byte)chnks.IndexOf(chnk);
							else
							{
								newBG1[x / 2, y / 2] = (byte)chnks.Count;
								Chunk newchnk = new Chunk();
								for (int i = 0; i < 8; i++)
								{
									for (int j = 0; j < 8; j++)
									{
										newchnk.tiles[j][i].tileIndex = LevelData.NewChunks.chunkList[chnk & 0xFF].tiles[j][i].tileIndex;
										newchnk.tiles[j].Solid1 = LevelData.NewChunks.chunkList[chnk & 0xFF][i].tileIndexs[i, j].Solid1;
										newchnk.tiles[j].XFlip = LevelData.NewChunks.chunkList[chnk & 0xFF][i].tileIndexs[i, j].XFlip;
										newchnk.tiles[j].YFlip = LevelData.NewChunks.chunkList[chnk & 0xFF][i].tileIndexs[i, j].YFlip;
									}
								}
								for (int i = 0; i < 8; i++)
								{
									for (int j = 0; j < 8; j++)
									{
										newchnk.tiles[j][i + 8].tileIndex = LevelData.NewChunks.chunkList[(chnk >> 8) & 0xFF].tiles[j][i].tileIndex;
										newchnk.tiles[j].Solid1 = LevelData.NewChunks.chunkList[(chnk >> 8) & 0xFF][i + 8].tileIndexs[i, j].Solid1;
										newchnk.tiles[j].XFlip = LevelData.NewChunks.chunkList[(chnk >> 8) & 0xFF][i + 8].tileIndexs[i, j].XFlip;
										newchnk.tiles[j].YFlip = LevelData.NewChunks.chunkList[(chnk >> 8) & 0xFF][i + 8].tileIndexs[i, j].YFlip;
									}
								}
								for (int i = 0; i < 8; i++)
								{
									for (int j = 0; j < 8; j++)
									{
										newchnk.tiles[j + 8][i].tileIndex = LevelData.NewChunks.chunkList[(chnk >> 16) & 0xFF].tiles[j][i].tileIndex;
										newchnk.tiles[j + 8].Solid1 = LevelData.NewChunks.chunkList[(chnk >> 16) & 0xFF][i].tileIndexs[i, j].Solid1;
										newchnk.tiles[j + 8].XFlip = LevelData.NewChunks.chunkList[(chnk >> 16) & 0xFF][i].tileIndexs[i, j].XFlip;
										newchnk.tiles[j + 8].YFlip = LevelData.NewChunks.chunkList[(chnk >> 16) & 0xFF][i].tileIndexs[i, j].YFlip;
									}
								}
								for (int i = 0; i < 8; i++)
								{
									for (int j = 0; j < 8; j++)
									{
										newchnk.tiles[j + 8][i + 8].tileIndex = LevelData.NewChunks.chunkList[(chnk >> 24) & 0xFF].tiles[j][i].tileIndex;
										newchnk.tiles[j + 8].Solid1 = LevelData.NewChunks.chunkList[(chnk >> 24) & 0xFF][i + 8].tileIndexs[i, j].Solid1;
										newchnk.tiles[j + 8].XFlip = LevelData.NewChunks.chunkList[(chnk >> 24) & 0xFF][i + 8].tileIndexs[i, j].XFlip;
										newchnk.tiles[j + 8].YFlip = LevelData.NewChunks.chunkList[(chnk >> 24) & 0xFF][i + 8].tileIndexs[i, j].YFlip;
									}
								}
								tmpchnk.Add(newchnk);
								chnks.Add(chnk);
							}
						}
					}
					LevelData.Layout.BGLayout = newBG1;
					tmpchnk.RemoveAt(0);
					break;
				case 2: // S1 -> S2
					tmpchnk = new List<Chunk>() { new Chunk() };
					Dictionary<ushort, ushort[]> cnkinds = new Dictionary<ushort, ushort[]>() { { 0, new ushort[4] } };
					List<ushort> usedcnks =
						LevelData.Layout.FGLayout.Cast<ushort>().Concat(LevelData.Layout.BGLayout.Cast<ushort>()).Distinct().ToList();
					if (usedcnks.Contains(0))
						usedcnks.Remove(0);
					foreach (ushort usedcnk in usedcnks)
					{
						Chunk item = LevelData.NewChunks.chunkList[usedcnk];
						Chunk[] newchnk = new Chunk[4];
						for (int i = 0; i < 4; i++)
							newchnk[i] = new Chunk();
						for (int y = 0; y < chunksz; y++)
							for (int x = 0; x < chunksz; x++)
							{
								S2ChunkBlock blk = (S2ChunkBlock)newchnk[0].tiles[y][x];
								blk.Block = item.tiles[y][x].tileIndex;
								blk.Solid1 = item.tiles[y][x].Solid1;
								blk.Solid2 = blk.Solid1;
								blk.XFlip = item.tiles[y][x].XFlip;
								blk.YFlip = item.tiles[y][x].YFlip;
								blk = (S2ChunkBlock)newchnk[1].tiles[y][x];
								blk.Block = item.tiles[y][x + chunksz].tileIndex;
								blk.Solid1 = item.tiles[y][x + chunksz].Solid1;
								blk.Solid2 = blk.Solid1;
								blk.XFlip = item.tiles[y][x + chunksz].XFlip;
								blk.YFlip = item.tiles[y][x + chunksz].YFlip;
								blk = (S2ChunkBlock)newchnk[2].tiles[y][x];
								blk.Block = item.tiles[y + chunksz][x].tileIndex;
								blk.Solid1 = item.tiles[y + chunksz][x].Solid1;
								blk.Solid2 = blk.Solid1;
								blk.XFlip = item.tiles[y + chunksz][x].XFlip;
								blk.YFlip = item.tiles[y + chunksz][x].YFlip;
								blk = (S2ChunkBlock)newchnk[3].tiles[y][x];
								blk.Block = item.tiles[y + chunksz][x + chunksz].tileIndex;
								blk.Solid1 = item.tiles[y + chunksz][x + chunksz].Solid1;
								blk.Solid2 = blk.Solid1;
								blk.XFlip = item.tiles[y + chunksz][x + chunksz].XFlip;
								blk.YFlip = item.tiles[y + chunksz][x + chunksz].YFlip;
							}
						ushort[] ids = new ushort[4];
						for (int i = 0; i < 4; i++)
						{
							byte[] b = newchnk[i].GetBytes();
							int match = -1;
							for (int c = 0; c < tmpchnk.Count; c++)
								if (b.FastArrayEqual(tmpchnk[c].GetBytes()))
								{
									match = c;
									break;
								}
							if (match != -1)
								ids[i] = (ushort)match;
							else
							{
								ids[i] = (ushort)tmpchnk.Count;
								tmpchnk.Add(newchnk[i]);
							}
						}
						cnkinds.Add(usedcnk, ids);
					}
					ushort[,] newFG = new ushort[LevelData.FGWidth * 2, LevelData.FGHeight * 2];
					for (int y = 0; y < LevelData.FGHeight; y++)
						for (int x = 0; x < LevelData.FGWidth; x++)
							if (LevelData.Scene.layout[y][x] != 0)
							{
								ushort[] ids = cnkinds[LevelData.Scene.layout[y][x]];
								newFG[x * 2, y * 2] = ids[0];
								newFG[(x * 2) + 1, y * 2] = ids[1];
								newFG[x * 2, (y * 2) + 1] = ids[2];
								newFG[(x * 2) + 1, (y * 2) + 1] = ids[3];
							}
					LevelData.Layout.FGLayout = newFG;
					ushort[,] newBG = new ushort[LevelData.BGWidth * 2, LevelData.BGHeight * 2];
					for (int y = 0; y < LevelData.BGHeight; y++)
						for (int x = 0; x < LevelData.BGWidth; x++)
							if (LevelData.Background.layers[bglayer].layout[y][x] != 0)
							{
								ushort[] ids = cnkinds[LevelData.Background.layers[bglayer].layout[y][x]];
								newBG[x * 2, y * 2] = ids[0];
								newBG[(x * 2) + 1, y * 2] = ids[1];
								newBG[x * 2, (y * 2) + 1] = ids[2];
								newBG[(x * 2) + 1, (y * 2) + 1] = ids[3];
							}
					LevelData.Layout.BGLayout = newBG;
					break;
				case 3: // S2 -> S2
					tmpchnk = LevelData.Chunks.ToList();
					break;
			}
			tmp2 = new List<byte>();
			if (OutFmt == EngineVersion.SKC)
				LevelData.littleendian = false;
			foreach (Chunk c in tmpchnk)
			{
				tmp2.AddRange(c.GetBytes());
			}
			if (OutFmt == EngineVersion.SKC)
				LevelData.littleendian = true;
			switch (OutFmt)
			{
				case EngineVersion.V4:
				case EngineVersion.S2:
				case EngineVersion.S3K:
				case EngineVersion.SKC:
					cmp = CompressionType.Kosinski;
					break;
				default:
					cmp = CompressionType.Uncompressed;
					break;
			}
			Compression.Compress(tmp2.ToArray(), Path.Combine(OutDir, "Chunks.bin"), cmp);
			Level.Chunks = new[] { new SonicRetro.SonLVL.API.FileInfo("Chunks.bin") };
			ushort fgw = (ushort)LevelData.FGWidth;
			ushort bgw = (ushort)LevelData.BGWidth;
			ushort fgh = (ushort)LevelData.FGHeight;
			ushort bgh = (ushort)LevelData.BGHeight;
			switch (OutFmt)
			{
				case EngineVersion.V4:
					tmp2 = new List<byte>
					{
						(byte)(LevelData.FGWidth - 1),
						(byte)(LevelData.FGHeight - 1)
					};
					for (int lr = 0; lr < LevelData.FGHeight; lr++)
						for (int lc = 0; lc < LevelData.FGWidth; lc++)
							tmp2.Add((byte)(LevelData.Scene.layout[lr][lr][lc] | (LevelData.Layout.FGLoop?[lc] ?? false ? 0x80 : 0)));
					Compression.Compress(tmp2.ToArray(), Path.Combine(OutDir, "FGLayout.bin"), CompressionType.Uncompressed);
					Level.FGLayout = "FGLayout.bin";
					tmp2 = new List<byte>
					{
						(byte)(LevelData.BGWidth - 1),
						(byte)(LevelData.BGHeight - 1)
					};
					for (int lr = 0; lr < LevelData.BGHeight; lr++)
						for (int lc = 0; lc < LevelData.BGWidth; lc++)
							tmp2.Add((byte)(LevelData.Background.layers[bglayer].layout[lr][lr][lc] | (LevelData.Layout.BGLoop?[lc] ?? false ? 0x80 : 0)));
					Compression.Compress(tmp2.ToArray(), Path.Combine(OutDir, "BGLayout.bin"), CompressionType.Uncompressed);
					Level.BGLayout = "BGLayout.bin";
					break;
				case EngineVersion.S2NA:
					tmp2 = new List<byte>
					{
						(byte)(LevelData.FGWidth - 1),
						(byte)(LevelData.FGHeight - 1)
					};
					for (int lr = 0; lr < LevelData.FGHeight; lr++)
						for (int lc = 0; lc < LevelData.FGWidth; lc++)
							tmp2.Add((byte)LevelData.Scene.layout[lr][lc]);
					Compression.Compress(tmp2.ToArray(), Path.Combine(OutDir, "FGLayout.bin"), CompressionType.Uncompressed);
					Level.FGLayout = "FGLayout.bin";
					tmp2 = new List<byte>
					{
						(byte)(LevelData.BGWidth - 1),
						(byte)(LevelData.BGHeight - 1)
					};
					for (int lr = 0; lr < LevelData.BGHeight; lr++)
						for (int lc = 0; lc < LevelData.BGWidth; lc++)
							tmp2.Add((byte)LevelData.Background.layers[bglayer].layout[lr][lc]);
					Compression.Compress(tmp2.ToArray(), Path.Combine(OutDir, "BGLayout.bin"), CompressionType.Uncompressed);
					Level.BGLayout = "BGLayout.bin";
					break;
				case EngineVersion.S2:
					tmp2 = new List<byte>();
					for (int la = 0; la < 16; la++)
					{
						if (LevelData.FGHeight > la)
							for (int laf = 0; laf < 128; laf++)
								if (LevelData.FGWidth > laf)
									tmp2.Add((byte)LevelData.Scene.layout[la][laf]);
								else
									tmp2.Add(0);
						else
							tmp2.AddRange(new byte[128]);
						if (LevelData.BGHeight > la)
							for (int lab = 0; lab < 128; lab++)
								if (LevelData.BGWidth > lab)
									tmp2.Add((byte)LevelData.Background.layers[bglayer].layout[la][lab]);
								else
									tmp2.Add(0);
						else
							tmp2.AddRange(new byte[128]);
					}
					Compression.Compress(tmp2.ToArray(), Path.Combine(OutDir, "Layout.bin"), CompressionType.Kosinski);
					Level.Layout = "Layout.bin";
					break;
				case EngineVersion.S3K:
					tmp2 = new List<byte>();
					tmp2.AddRange(ByteConverter.GetBytes(fgw));
					tmp2.AddRange(ByteConverter.GetBytes(bgw));
					tmp2.AddRange(ByteConverter.GetBytes(fgh));
					tmp2.AddRange(ByteConverter.GetBytes(bgh));
					for (int la = 0; la < 32; la++)
					{
						if (la < fgh)
							tmp2.AddRange(ByteConverter.GetBytes((ushort)(0x8088 + (la * fgw))));
						else
							tmp2.AddRange(new byte[2]);
						if (la < bgh)
							tmp2.AddRange(ByteConverter.GetBytes((ushort)(0x8088 + (fgh * fgw) + (la * bgw))));
						else
							tmp2.AddRange(new byte[2]);
					}
					for (int y = 0; y < fgh; y++)
						for (int x = 0; x < fgw; x++)
							tmp2.Add((byte)LevelData.Scene.layout[y][x]);
					for (int y = 0; y < bgh; y++)
						for (int x = 0; x < bgw; x++)
							tmp2.Add((byte)LevelData.Background.layers[bglayer].layout[y][x]);
					Compression.Compress(tmp2.ToArray(), Path.Combine(OutDir, "Layout.bin"), CompressionType.Uncompressed);
					Level.Layout = "Layout.bin";
					break;
				case EngineVersion.SKC:
					tmp2 = new List<byte>();
					tmp2.AddRange(ByteConverter.GetBytes(fgw));
					tmp2.AddRange(ByteConverter.GetBytes(bgw));
					tmp2.AddRange(ByteConverter.GetBytes(fgh));
					tmp2.AddRange(ByteConverter.GetBytes(bgh));
					for (int la = 0; la < 32; la++)
					{
						if (la < fgh)
							tmp2.AddRange(ByteConverter.GetBytes((ushort)(0x8088 + (la * fgw))));
						else
							tmp2.AddRange(new byte[2]);
						if (la < bgh)
							tmp2.AddRange(ByteConverter.GetBytes((ushort)(0x8088 + (fgh * fgw) + (la * bgw))));
						else
							tmp2.AddRange(new byte[2]);
					}
					List<byte> l = new List<byte>();
					for (int y = 0; y < fgh; y++)
						for (int x = 0; x < fgw; x++)
							l.Add((byte)LevelData.Scene.layout[y][x]);
					for (int y = 0; y < bgh; y++)
						for (int x = 0; x < bgw; x++)
							l.Add((byte)LevelData.Background.layers[bglayer].layout[y][x]);
					for (int i = 0; i < l.Count; i++)
						tmp2.Add(l[i ^ 1]);
					Compression.Compress(tmp2.ToArray(), Path.Combine(OutDir, "Layout.bin"), CompressionType.Uncompressed);
					Level.Layout = "Layout.bin";
					break;
				case EngineVersion.SCDPC:
					tmp2 = new List<byte>();
					for (int lr = 0; lr < 8; lr++)
						for (int lc = 0; lc < 64; lc++)
							if (lc < fgw & lr < fgh)
								tmp2.Add((byte)LevelData.Scene.layout[lr][lc]);
							else
								tmp2.Add(0);
					Compression.Compress(tmp2.ToArray(), Path.Combine(OutDir, "FGLayout.bin"), CompressionType.Uncompressed);
					Level.FGLayout = "FGLayout.bin";
					tmp2 = new List<byte>();
					for (int lr = 0; lr < 8; lr++)
						for (int lc = 0; lc < 64; lc++)
							if (lc < bgw & lr < bgh)
								tmp2.Add((byte)LevelData.Background.layers[bglayer].layout[lr][lc]);
							else
								tmp2.Add(0);
					Compression.Compress(tmp2.ToArray(), Path.Combine(OutDir, "BGLayout.bin"), CompressionType.Uncompressed);
					Level.BGLayout = "BGLayout.bin";
					break;
			}
			tmp2 = new List<byte>();
			if (OutFmt != EngineVersion.SCDPC)
			{
				for (int pl = 0; pl < 4; pl++)
					for (int pi = 0; pi < 16; pi++)
						tmp2.AddRange(ByteConverter.GetBytes(LevelData.Palette[0][pl, pi].MDColor));
			}
			else
			{
				for (int pl = 0; pl < 4; pl++)
					for (int pi = 0; pi < 16; pi++)
					{
						tmp2.Add(LevelData.Palette[0][pl, pi].R);
						tmp2.Add(LevelData.Palette[0][pl, pi].G);
						tmp2.Add(LevelData.Palette[0][pl, pi].B);
						tmp2.Add((byte)1);
					}
			}
			Compression.Compress(tmp2.ToArray(), Path.Combine(OutDir, "Palette.bin"), CompressionType.Uncompressed);
			Level.Palette = new PaletteList("Palette.bin:0:0:64");
			switch (LevelData.Level.ObjectFormat)
			{
				case EngineVersion.V4:
					switch (OutFmt)
					{
						case EngineVersion.S2:
						case EngineVersion.S2NA:
							ObjS1ToS2();
							break;
						case EngineVersion.S3K:
						case EngineVersion.SKC:
							ObjS1ToS3K();
							break;
						case EngineVersion.SCDPC:
							ObjS1ToSCD();
							break;
					}
					break;
				case EngineVersion.S2:
				case EngineVersion.S2NA:
					switch (OutFmt)
					{
						case EngineVersion.V4:
							ObjS2ToS1();
							break;
						case EngineVersion.S3K:
						case EngineVersion.SKC:
							ObjS2ToS3K();
							break;
						case EngineVersion.SCDPC:
							ObjS2ToSCD();
							break;
					}
					break;
				case EngineVersion.S3K:
				case EngineVersion.SKC:
					switch (OutFmt)
					{
						case EngineVersion.V4:
							ObjS3KToS1();
							break;
						case EngineVersion.S2:
						case EngineVersion.S2NA:
							ObjS3KToS2();
							break;
						case EngineVersion.SCDPC:
							ObjS3KToSCD();
							break;
					}
					break;
				case EngineVersion.SCDPC:
					switch (OutFmt)
					{
						case EngineVersion.V4:
							ObjSCDToS1();
							break;
						case EngineVersion.S2:
						case EngineVersion.S2NA:
							ObjSCDToS2();
							break;
						case EngineVersion.S3K:
						case EngineVersion.SKC:
							ObjSCDToS3K();
							break;
					}
					break;
			}
			tmp2 = new List<byte>();
			switch (OutFmt)
			{
				case EngineVersion.V4:
					for (int oi = 0; oi < LevelData.Objects.Count; oi++)
					{
						tmp2.AddRange(((S1ObjectEntry)LevelData.Objects[oi]).GetBytes());
					}
					tmp2.AddRange(new byte[] { 0xFF, 0xFF });
					while (tmp2.Count % S1ObjectEntry.Size > 0)
					{
						tmp2.Add(0);
					}
					break;
				case EngineVersion.S2:
				case EngineVersion.S2NA:
					for (int oi = 0; oi < LevelData.Objects.Count; oi++)
					{
						tmp2.AddRange(((S2ObjectEntry)LevelData.Objects[oi]).GetBytes());
					}
					tmp2.AddRange(new byte[] { 0xFF, 0xFF });
					while (tmp2.Count % S2ObjectEntry.Size > 0)
					{
						tmp2.Add(0);
					}
					break;
				case EngineVersion.S3K:
				case EngineVersion.SKC:
					for (int oi = 0; oi < LevelData.Objects.Count; oi++)
					{
						tmp2.AddRange(((S3KObjectEntry)LevelData.Objects[oi]).GetBytes());
					}
					tmp2.AddRange(new byte[] { 0xFF, 0xFF });
					while (tmp2.Count % S3KObjectEntry.Size > 0)
					{
						tmp2.Add(0);
					}
					break;
				case EngineVersion.SCDPC:
					for (int oi = 0; oi < LevelData.Objects.Count; oi++)
					{
						tmp2.AddRange(((SCDObjectEntry)LevelData.Objects[oi]).GetBytes());
					}
					tmp2.Add(0xFF);
					while (tmp2.Count % SCDObjectEntry.Size > 0)
					{
						tmp2.Add(0xFF);
					}
					break;
			}
			Compression.Compress(tmp2.ToArray(), Path.Combine(OutDir, "Objects.bin"), CompressionType.Uncompressed);
			Level.Objects = "Objects.bin";
			if (LevelData.Rings != null)
			{
				switch (OutFmt)
				{
					case EngineVersion.S2:
					case EngineVersion.S2NA:
						new API.S2.Ring().WriteLayout(LevelData.Rings, Path.Combine(OutDir, "Rings.bin"));
						Level.Rings = "Rings.bin";
						break;
					case EngineVersion.S3K:
					case EngineVersion.SKC:
						new API.S3K.Ring().WriteLayout(LevelData.Rings, Path.Combine(OutDir, "Rings.bin"));
						Level.Rings = "Rings.bin";
						break;
				}
			}
			if (LevelData.ColInds1 != null)
				switch (OutFmt)
				{
					case EngineVersion.V4:
					case EngineVersion.V3:
					case EngineVersion.SCDPC:
						Compression.Compress(LevelData.ColInds1.ToArray(), Path.Combine(OutDir, "Indexes.bin"), CompressionType.Uncompressed);
						Level.CollisionIndex = "Indexes.bin";
						break;
					case EngineVersion.S2:
					case EngineVersion.S2NA:
						Compression.Compress(LevelData.ColInds1.ToArray(), Path.Combine(OutDir, "Indexes1.bin"), CompressionType.Kosinski);
						Level.CollisionIndex1 = "Indexes1.bin";
						Compression.Compress(LevelData.ColInds2.ToArray(), Path.Combine(OutDir, "Indexes2.bin"), CompressionType.Kosinski);
						Level.CollisionIndex2 = "Indexes2.bin";
						break;
					case EngineVersion.S3K:
					case EngineVersion.SKC:
						while (LevelData.ColInds1.Count < 0x300)
							LevelData.ColInds1.Add(0);
						while (LevelData.ColInds2.Count < 0x300)
							LevelData.ColInds2.Add(0);
						tmp2 = new List<byte>();
						for (int i = 0; i < LevelData.ColInds1.Count; i++)
						{
							tmp2.Add(LevelData.ColInds1[i]);
							tmp2.Add(LevelData.ColInds2[i]);
						}
						Compression.Compress(tmp2.ToArray(), Path.Combine(OutDir, "IndexesSK.bin"), CompressionType.Uncompressed);
						tmp2 = new List<byte>();
						foreach (byte item in LevelData.ColInds1)
							tmp2.AddRange(ByteConverter.GetBytes((ushort)item));
						foreach (byte item in LevelData.ColInds2)
							tmp2.AddRange(ByteConverter.GetBytes((ushort)item));
						Compression.Compress(tmp2.ToArray(), Path.Combine(OutDir, "IndexesS3.bin"), CompressionType.Uncompressed);
						Level.CollisionIndex = "IndexesSK.bin";
						Level.CollisionIndexSize = 1;
						break;
				}
			if (LevelData.ColArr1 != null)
			{
				tmp2 = new List<byte>();
				for (int i = 0; i < 256; i++)
					for (int j = 0; j < 16; j++)
						tmp2.Add(unchecked((byte)LevelData.ColArr1[i][j]));
				Compression.Compress(tmp2.ToArray(), Path.Combine(OutDir, "Collision.bin"), CompressionType.Uncompressed);
				Level.CollisionArray1 = "Collision.bin";
				sbyte[][] rotcol = LevelData.GenerateRotatedCollision();
				tmp2 = new List<byte>();
				for (int i = 0; i < 256; i++)
					for (int j = 0; j < 16; j++)
						tmp2.Add(unchecked((byte)rotcol[i][j]));
				Compression.Compress(tmp2.ToArray(), Path.Combine(OutDir, "CollisionR.bin"), CompressionType.Uncompressed);
				Level.CollisionArray2 = "CollisionR.bin";
			}
			if (LevelData.Angles != null)
			{
				Compression.Compress(LevelData.Angles, Path.Combine(OutDir, "Angles.bin"), CompressionType.Uncompressed);
				Level.Angles = "Angles.bin";
			}
			Output.Save(Path.Combine(OutDir, OutFmt.ToString() + "LVL.ini"));
			LevelData.littleendian = LE;
		}

		private void button2_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < comboBox1.Items.Count; i++)
			{
				comboBox1.SelectedIndex = i;
				ConvertLevel();
				Application.DoEvents();
			}
			System.Diagnostics.Process.Start(Dir);
		}
	}
}
