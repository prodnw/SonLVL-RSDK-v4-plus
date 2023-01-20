using RSDKv5;
using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace v5ImageImport
{
	class Program
	{
		static void Main(string[] args)
		{
			if (args.Length < 5)
			{
				Console.WriteLine("Usage: v5ImageImport image scene layer x y");
				return;
			}
			System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
			sw.Start();
			Bitmap bmp = new Bitmap(args[0]);
			int w = bmp.Width;
			int h = bmp.Height;
			BitmapInfo bmpi = new BitmapInfo(bmp);
			bmp.Dispose();
			string stgfol = Path.GetDirectoryName(Path.GetFullPath(args[1]));
			Gif tilebmp = new Gif(Path.Combine(stgfol, "16x16Tiles.gif"));
			if (tilebmp.width >= 16 && tilebmp.height >= 16)
			{
				LevelData.NewTiles = new BitmapBits[tilebmp.height / 16];
				for (int i = 0; i < tilebmp.height / 16; i++)
				{
					LevelData.NewTiles[i] = new BitmapBits(16, 16);
					Array.Copy(tilebmp.pixels, i * 256, LevelData.NewTiles[i].Bits, 0, 256);
				}
				for (int i = 0; i < 256; i++)
					LevelData.NewPalette[i] = tilebmp.palette[i].ToSystemColor();
			}
			else
			{
				LevelData.NewTiles = new BitmapBits[0x400];
				for (int i = 0; i < 0x400; i++)
					LevelData.NewTiles[i] = new BitmapBits(16, 16);
			}
			StageConfig stageConfig = new StageConfig(Path.Combine(stgfol, "StageConfig.bin"));
			for (int r = 0; r < stageConfig.palettes[0].colors.Length; r++)
				if (stageConfig.palettes[0].activeRows[r])
					for (int c = 0; c < stageConfig.palettes[0].colors[r].Length; c++)
						LevelData.NewPalette[(r * 16) + c] = stageConfig.palettes[0].colors[r][c].ToSystemColor();
			Scene scene = new Scene(args[1]);
			SceneLayer layer = scene.layers.FirstOrDefault(a => a.name == args[2]);
			if (layer == null)
			{
				Console.WriteLine("No layer with name \"{0}\" was found.", args[2]);
				return;
			}
			List<BitmapBits> tiles = new List<BitmapBits>(LevelData.NewTiles);
			ImportResult ir = BitmapToTiles(bmpi, tiles, true);
			List<ushort> freetiles = Enumerable.Range(0, LevelData.NewTiles.Length).Select(a => (ushort)a).Except(scene.layers.SelectMany(a => a.layout.SelectMany(b => b).Select(c => c.tileIndex)))
				.Where(c => LevelData.NewTiles[c].Bits.FastArrayEqual(0)).ToList();
			if (ir.Art.Count > freetiles.Count)
			{
				Console.WriteLine("There are " + (ir.Art.Count - freetiles.Count) + " tiles over the limit.");
				Console.WriteLine("Import cannot proceed.");
				return;
			}
			if (ir.Art.Count > 0)
			{
				for (int y = 0; y < ir.Mappings.GetLength(1); y++)
					for (int x = 0; x < ir.Mappings.GetLength(0); x++)
						if (ir.Mappings[x, y].tileIndex >= LevelData.NewTiles.Length)
							ir.Mappings[x, y].tileIndex = freetiles[ir.Mappings[x, y].tileIndex - LevelData.NewTiles.Length];
				for (int i = 0; i < ir.Art.Count; i++)
					LevelData.NewTiles[freetiles[i]] = ir.Art[i];
			}
			int xstart = int.Parse(args[3]);
			int ystart = int.Parse(args[4]);
			if (xstart < layer.width && ystart < layer.height)
			{
				int width = Math.Min(ir.Mappings.GetLength(0), layer.width - xstart);
				int height = Math.Min(ir.Mappings.GetLength(1), layer.height - ystart);
				for (int y = 0; y < height; y++)
					for (int x = 0; x < width; x++)
						layer.layout[y + ystart][x + xstart] = ir.Mappings[x, y];
			}
			scene.Write(args[1]);
			BitmapBits tilebmp2 = new BitmapBits(16, LevelData.NewTiles.Length * 16);
			for (int i = 0; i < LevelData.NewTiles.Length; i++)
				tilebmp2.DrawBitmap(LevelData.NewTiles[i], 0, i * 16);
			using (Bitmap bmp2 = tilebmp2.ToBitmap(LevelData.NewPalette))
				bmp2.Save(Path.Combine(stgfol, "16x16Tiles.gif"), System.Drawing.Imaging.ImageFormat.Gif);
			sw.Stop();
			Console.WriteLine("New tiles: {0:X}", ir.Art.Count);
			Console.WriteLine();
			Console.Write("Completed in ");
			if (sw.Elapsed.Hours > 0)
				Console.Write("{0}:{1:00}:{2:00}", sw.Elapsed.Hours, sw.Elapsed.Minutes, sw.Elapsed.Seconds);
			else if (sw.Elapsed.Minutes > 0)
				Console.Write("{0}:{1:00}", sw.Elapsed.Minutes, sw.Elapsed.Seconds);
			else
				Console.Write(sw.Elapsed.Seconds);
			if (sw.Elapsed.Milliseconds > 0)
				Console.Write(".{000}", sw.Elapsed.Milliseconds);
			Console.WriteLine();
		}

		public static ImportResult BitmapToTiles(BitmapInfo bmpi, List<BitmapBits> tiles, bool optimize)
		{
			int w = bmpi.Width / 16;
			int h = bmpi.Height / 16;
			ImportResult result = new ImportResult(w, h);
			for (int y = 0; y < h; y++)
				for (int x = 0; x < w; x++)
				{
					SceneLayer.Tile map = 0;
					BitmapBits tile = LevelData.BmpToTile(new BitmapInfo(bmpi, x * 16, y * 16, 16, 16), null);
					if (tile.Bits.FastArrayEqual(0))
					{
						map = ushort.MaxValue;
					}
					else
					{
						bool match = false;
						if (optimize)
						{
							BitmapBits tileh = new BitmapBits(tile);
							tileh.Flip(true, false);
							BitmapBits tilev = new BitmapBits(tile);
							tilev.Flip(false, true);
							BitmapBits tilehv = new BitmapBits(tilev);
							tilehv.Flip(true, false);
							for (int i = 0; i < tiles.Count; i++)
							{
								if (tiles[i].Bits.FastArrayEqual(tile.Bits))
								{
									match = true;
									map.tileIndex = (ushort)i;
									break;
								}
								if (tiles[i].Bits.FastArrayEqual(tileh.Bits))
								{
									match = true;
									map.tileIndex = (ushort)i;
									map.direction = SceneLayer.Tile.Directions.FlipX;
									break;
								}
								if (tiles[i].Bits.FastArrayEqual(tilev.Bits))
								{
									match = true;
									map.tileIndex = (ushort)i;
									map.direction = SceneLayer.Tile.Directions.FlipY;
									break;
								}
								if (tiles[i].Bits.FastArrayEqual(tilehv.Bits))
								{
									match = true;
									map.tileIndex = (ushort)i;
									map.direction = SceneLayer.Tile.Directions.FlipXY;
									break;
								}
							}
						}
						if (!match)
						{
							tiles.Add(tile);
							result.Art.Add(tile);
							map.tileIndex = (ushort)(tiles.Count - 1);
						}
					}
					result.Mappings[x, y] = map;
				}
			return result;
		}
	}

	public class ImportResult
	{
		public SceneLayer.Tile[,] Mappings { get; }
		public List<BitmapBits> Art { get; }

		public ImportResult(int width, int height)
		{
			Mappings = new SceneLayer.Tile[width, height];
			Art = new List<BitmapBits>();
		}
	}
}
