using RSDKv3_4;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bitmap = System.Drawing.Bitmap;

namespace SonicRetro.SonLVL.API
{
	public static class LevelData
	{
		public static string EXEFile;
		public static EngineVersion RSDKVer;
		public static IDataFile DataFile;
		public static string ModFolder;
		public static GameConfig GameConfig;
		public static GameConfig.StageList.StageInfo StageInfo;
		public static StageConfig StageConfig;
		public static Color[] NewPalette = new Color[256];
		public static BitmapBits[] NewTiles;
		public static Tiles128x128 NewChunks;
		public static TileConfig Collision;
		public static Backgrounds Background;
		public static Scene Scene;
		public static List<ObjectEntry> Objects;
		public static List<ScrollData>[] BGScroll = new List<ScrollData>[8];
		public static Bitmap[] NewTileBmps;
		public static BitmapBits[][] NewColBmpBits;
		public static Bitmap[][] NewColBmps;
		public static Sprite[] ChunkSprites;
		public static Bitmap[][] ChunkBmps;
		public static Bitmap[] CompChunkBmps;
		public static ColorPalette BmpPal;
		public static Dictionary<string, ObjectData> INIObjDefs;
		public static Dictionary<byte, ObjectDefinition> ObjTypes;
		public static ObjectDefinition unkobj;
		private static Dictionary<string, BitmapBits> spriteSheets;
		public static BitmapBits[][] ChunkColBmpBits;
		public static Bitmap[][] ChunkColBmps;
		public static Bitmap UnknownImg;
		public static Sprite UnknownSprite;
		public delegate void LogEventHandler(params string[] message);
		public static event LogEventHandler LogEvent = delegate { };
		public static event Action PaletteChangedEvent = delegate { };
		internal static readonly bool IsMonoRuntime = Type.GetType("Mono.Runtime") != null;
		internal static readonly bool IsWindows = !(Environment.OSVersion.Platform == PlatformID.MacOSX | Environment.OSVersion.Platform == PlatformID.Unix | Environment.OSVersion.Platform == PlatformID.Xbox);
		private static readonly BitmapBits InvalidTile = new BitmapBits(16, 16);
		public const int ColorTransparent = 0;
		public const int ColorWhite = 252;
		public const int ColorYellow = 253;
		public const int ColorBlack = 254;

		static LevelData()
		{
			InvalidTile.DrawLine(15, 0, 0, 15, 0);
			InvalidTile.DrawLine(15, 0, 0, 0, 15);
			InvalidTile.DrawLine(15, 15, 15, 0, 15);
			InvalidTile.DrawLine(15, 15, 15, 15, 0);
			InvalidTile.DrawLine(15, 0, 0, 15, 15);
			InvalidTile.DrawLine(15, 0, 15, 15, 0);
		}

		public static void LoadGame(string filename)
		{
			Log("Opening game \"" + filename + "\"...");
			EXEFile = Path.GetFullPath(filename);
			Environment.CurrentDirectory = Path.GetDirectoryName(EXEFile);
			string fn = Path.GetFileName(EXEFile);
			if (fn.StartsWith("RSDKv3", StringComparison.OrdinalIgnoreCase))
				RSDKVer = EngineVersion.V3;
			else if (fn.StartsWith("soniccd", StringComparison.OrdinalIgnoreCase))
				RSDKVer = EngineVersion.V3;
			else if (fn.StartsWith("RSDKv4", StringComparison.OrdinalIgnoreCase))
				RSDKVer = EngineVersion.V4;
			else
				throw new NotImplementedException("Unrecognized game!");
			UnknownImg = Properties.Resources.UnknownImg.Copy();
			UnknownSprite = new Sprite(new BitmapBits(UnknownImg), true, -8, -7);
			Log("Game type is " + RSDKVer + ".");
			if (File.Exists("Data.rsdk"))
			{
				switch (RSDKVer)
				{
					case EngineVersion.V4:
						DataFile = new RSDKv4.DataFile("Data.rsdk");
						break;
					case EngineVersion.V3:
						DataFile = new RSDKv3.DataFile("Data.rsdk");
						break;
				}
			}
			else
				DataFile = null;
		}

		public static void LoadMod(string path)
		{
			ModFolder = path;
			switch (RSDKVer)
			{
				case EngineVersion.V4:
					GameConfig = ReadFile<RSDKv4.GameConfig>("Data/Game/GameConfig.bin");
					var pal = ((RSDKv4.GameConfig)GameConfig).masterPalette;
					for (int l = 0; l < pal.colors.Length; l++)
						for (int c = 0; c < pal.COLORS_PER_ROW; c++)
							if ((l * pal.COLORS_PER_ROW) + c < 256)
								NewPalette[(l * pal.COLORS_PER_ROW) + c] = Color.FromArgb(pal.colors[l][c].R, pal.colors[l][c].G, pal.colors[l][c].B);
					break;
				case EngineVersion.V3:
					GameConfig = ReadFile<RSDKv3.GameConfig>("Data/Game/GameConfig.bin");
					var mpal = ReadFileRaw("Data/Palettes/MasterPalette.act");
					for (int i = 0; i < Math.Min(mpal.Length / 3, 256); i++)
						NewPalette[i] = Color.FromArgb(mpal[i * 3], mpal[i * 3 + 1], mpal[i * 3 + 2]);
					break;
			}
		}

		public static T ReadFile<T>(string filename)
			where T : new()
		{
			string modpath = Path.Combine(ModFolder, filename);
			if (File.Exists(modpath))
				return (T)Activator.CreateInstance(typeof(T), modpath);
			if (DataFile != null && DataFile.TryGetFileData(filename, out byte[] data))
				using (MemoryStream ms = new MemoryStream(data))
					return (T)Activator.CreateInstance(typeof(T), ms);
			if (File.Exists(filename))
				return (T)Activator.CreateInstance(typeof(T), filename);
			return new T();
		}

		public static byte[] ReadFileRaw(string filename)
		{
			string modpath = Path.Combine(ModFolder, filename);
			if (File.Exists(modpath))
				return File.ReadAllBytes(modpath);
			if (DataFile != null && DataFile.TryGetFileData(filename, out byte[] data))
				return data;
			if (File.Exists(filename))
				return File.ReadAllBytes(filename);
			return null;
		}

		public static void LoadLevel(GameConfig.StageList.StageInfo stage)
		{
			System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
			stopwatch.Start();
			Log("Loading " + stage.name + "...");
			StageInfo = stage;
			string stgfol = $"Data/Stages/{stage.folder}/";
			switch (RSDKVer)
			{
				case EngineVersion.V4:
					StageConfig = ReadFile<RSDKv4.StageConfig>(stgfol + "StageConfig.bin");
					break;
				case EngineVersion.V3:
					StageConfig = ReadFile<RSDKv3.StageConfig>(stgfol + "StageConfig.bin");
					break;
			}
			for (int l = 0; l < StageConfig.stagePalette.colors.Length; l++)
				for (int c = 0; c < StageConfig.stagePalette.colors[l].Length; c++)
					NewPalette[(l * 16) + c + 96] = Color.FromArgb(StageConfig.stagePalette.colors[l][c].R, StageConfig.stagePalette.colors[l][c].G, StageConfig.stagePalette.colors[l][c].B);
			Gif tilebmp = ReadFile<Gif>(stgfol + "16x16Tiles.gif");
			NewTiles = new BitmapBits[tilebmp.height / 16];
			for (int i = 0; i < tilebmp.height / 16; i++)
			{
				NewTiles[i] = new BitmapBits(16, 16);
				Array.Copy(tilebmp.pixels, i * 256, NewTiles[i].Bits, 0, 256);
			}
			for (int i = 128; i < 256; i++)
				NewPalette[i] = Color.FromArgb(tilebmp.palette[i].R, tilebmp.palette[i].G, tilebmp.palette[i].B);
			NewChunks = ReadFile<Tiles128x128>(stgfol + "128x128Tiles.bin");
			Collision = ReadFile<TileConfig>(stgfol + "CollisionMasks.bin");
			switch (RSDKVer)
			{
				case EngineVersion.V4:
					Background = ReadFile<RSDKv4.Backgrounds>(stgfol + "Backgrounds.bin");
					Scene = ReadFile<RSDKv4.Scene>($"{stgfol}Act{stage.actID}.bin");
					break;
				case EngineVersion.V3:
					Background = ReadFile<RSDKv3.Backgrounds>(stgfol + "Backgrounds.bin");
					Scene = ReadFile<RSDKv3.Scene>($"{stgfol}Act{stage.actID}.bin");
					break;
			}
			Objects = new List<ObjectEntry>(Scene.entities.Count);
			foreach (var item in Scene.entities)
				Objects.Add(ObjectEntry.Create(item));
			for (int i = 0; i < 8; i++)
			{
				BGScroll[i] = new List<ScrollData>();
				switch (Background.layers[i].type)
				{
					case Backgrounds.Layer.LayerTypes.HScroll:
					case Backgrounds.Layer.LayerTypes.VScroll:
						int lastind = -1;
						for (ushort y = 0; y < Background.layers[i].lineScroll.Length; y++)
						{
							if (Background.layers[i].lineScroll[y] != lastind)
							{
								lastind = Background.layers[i].lineScroll[y];
								Backgrounds.ScrollInfo si = null;
								switch (Background.layers[i].type)
								{
									case Backgrounds.Layer.LayerTypes.HScroll:
										si = Background.hScroll[lastind];
										break;
									case Backgrounds.Layer.LayerTypes.VScroll:
										si = Background.vScroll[lastind];
										break;
								}
								BGScroll[i].Add(new ScrollData(y, si));
							}
						}
						break;
				}
			}
			using (Bitmap palbmp = new Bitmap(1, 1, PixelFormat.Format8bppIndexed))
				BmpPal = palbmp.Palette;
			NewPalette.CopyTo(BmpPal.Entries, 0);
			BmpPal.Entries[ColorTransparent] = Color.Transparent;
			BmpPal.Entries[ColorWhite] = Color.White;
			BmpPal.Entries[ColorYellow] = Color.Yellow;
			BmpPal.Entries[ColorBlack] = Color.Black;
			UnknownImg.Palette = BmpPal;
			INIObjDefs = new Dictionary<string, ObjectData>();
			ObjTypes = new Dictionary<byte, ObjectDefinition>();
			unkobj = new DefaultObjectDefinition();
			spriteSheets = new Dictionary<string, BitmapBits>();
			if (File.Exists("SonLVLObjDefs.ini"))
				LoadObjectDefinitionFile("SonLVLObjDefs.ini");
			if (File.Exists(Path.Combine(ModFolder, "SonLVLObjDefs.ini")))
				LoadObjectDefinitionFile(Path.Combine(ModFolder, "SonLVLObjDefs.ini"));
			unkobj.Init(new ObjectData());
			InitObjectDefinitions();
			foreach (ObjectEntry obj in Objects)
				obj.UpdateSprite();
			Log("Drawing tile bitmaps...");
			NewTileBmps = new Bitmap[NewTiles.Length];
			for (int bi = 0; bi < NewTiles.Length; bi++)
				RedrawBlock(bi, false);
			Log("Drawing collision bitmaps...");
			NewColBmpBits = new BitmapBits[Collision.collisionMasks[0].Length][];
			NewColBmps = new Bitmap[Collision.collisionMasks[0].Length][];
			for (int i = 0; i < Collision.collisionMasks[0].Length; i++)
			{
				NewColBmpBits[i] = new BitmapBits[2];
				NewColBmps[i] = new Bitmap[2];
				RedrawCol(i, false);
			}
			Log("Drawing chunk bitmaps...");
			ChunkSprites = new Sprite[NewChunks.chunkList.Length];
			ChunkBmps = new Bitmap[NewChunks.chunkList.Length][];
			CompChunkBmps = new Bitmap[NewChunks.chunkList.Length];
			ChunkColBmpBits = new BitmapBits[NewChunks.chunkList.Length][];
			ChunkColBmps = new Bitmap[NewChunks.chunkList.Length][];
			for (int i = 0; i < NewChunks.chunkList.Length; i++)
			{
				ChunkBmps[i] = new Bitmap[2];
				ChunkColBmpBits[i] = new BitmapBits[2];
				ChunkColBmps[i] = new Bitmap[2];
				RedrawChunk(i);
			}
			stopwatch.Stop();
			Log($"Level loaded in {stopwatch.Elapsed.TotalSeconds} second(s).");
		}

		public static void SaveLevel()
		{
			Log("Saving " + StageInfo.name + "...");
			string stgfol = Path.Combine(ModFolder, "Data\\Stages", StageInfo.folder);
			Directory.CreateDirectory(stgfol);
			for (int i = 0; i < 32; i++)
				StageConfig.stagePalette.colors[i / StageConfig.stagePalette.COLORS_PER_ROW][i % StageConfig.stagePalette.COLORS_PER_ROW] = new Palette.Color(NewPalette[i + 96].R, NewPalette[i + 96].G, NewPalette[i + 96].B);
			StageConfig.write(Path.Combine(stgfol, "StageConfig.bin"));
			BitmapBits tiles = new BitmapBits(16, NewTiles.Length * 16);
			for (int i = 0; i < NewTiles.Length; i++)
				tiles.DrawBitmap(NewTiles[i], 0, i * 16);
			using (Bitmap bmp = tiles.ToBitmap(NewPalette))
				bmp.Save(Path.Combine(stgfol, "16x16Tiles.gif"), ImageFormat.Gif);
			NewChunks.write(Path.Combine(stgfol, "128x128Tiles.bin"));
			Collision.write(Path.Combine(stgfol, "CollisionMasks.bin"));
			Background.hScroll.Clear();
			Background.vScroll.Clear();
			for (int i = 0; i < 8; i++)
			{
				int height;
				List<Backgrounds.ScrollInfo> scrlist;
				switch (Background.layers[i].type)
				{
					case Backgrounds.Layer.LayerTypes.HScroll:
						height = Background.layers[i].height * 128;
						scrlist = Background.hScroll;
						break;
					case Backgrounds.Layer.LayerTypes.VScroll:
						height = Background.layers[i].width * 128;
						scrlist = Background.vScroll;
						break;
					default:
						continue;
				}
				Background.layers[i].lineScroll = new byte[height];
				byte scrind = 0;
				int datind = 0;
				for (int y = 0; y < height; y++)
				{
					if (BGScroll[i][datind].StartPos == y)
					{
						Backgrounds.ScrollInfo si = null;
						switch (RSDKVer)
						{
							case EngineVersion.V3:
								si = BGScroll[i][datind++].GetInfoV3();
								break;
							case EngineVersion.V4:
								si = BGScroll[i][datind++].GetInfoV4();
								break;
						}
						int tmpind = scrlist.FindIndex(a => si.Equal(a));
						if (tmpind == -1)
						{
							tmpind = scrlist.Count;
							scrlist.Add(si);
						}
						scrind = (byte)tmpind;
					}
					Background.layers[i].lineScroll[y] = scrind;
				}
			}
			Background.write(Path.Combine(stgfol, "Backgrounds.bin"));
			Scene.write(Path.Combine(stgfol, $"Act{StageInfo.actID}.bin"));
		}

		public static BitmapBits DrawForeground(Rectangle? section, bool includeObjects, bool objectsAboveHighPlane, bool lowPlane, bool highPlane, bool collisionPath1, bool collisionPath2)
		{
			Rectangle bounds;
			if (section.HasValue)
				bounds = section.Value;
			else
			{
				int xend = 0;
				int yend = 0;
				for (int y = 0; y < FGHeight; y++)
					for (int x = 0; x < FGWidth; x++)
						if (Scene.layout[y][x] > 0)
						{
							xend = Math.Max(xend, x);
							yend = Math.Max(yend, y);
						}
				xend++;
				yend++;
				bounds = new Rectangle(0, 0, xend * 128, yend * 128);
			}
			BitmapBits LevelImg8bpp = new BitmapBits(bounds.Size);
			int cl = Math.Max(bounds.X / 128, 0);
			int ct = Math.Max(bounds.Y / 128, 0);
			int cr = Math.Min((bounds.Right - 1) / 128, FGWidth - 1);
			int cb = Math.Min((bounds.Bottom - 1) / 128, FGHeight - 1);
			for (int y = ct; y <= cb; y++)
				for (int x = cl; x <= cr; x++)
					if (Scene.layout[y][x] < NewChunks.chunkList.Length)
					{
						if ((!includeObjects || objectsAboveHighPlane) && lowPlane && highPlane)
						{
							LevelImg8bpp.DrawSprite(ChunkSprites[Scene.layout[y][x]], x * 128 - bounds.X, y * 128 - bounds.Y);
							if (collisionPath1)
								LevelImg8bpp.DrawBitmapComposited(ChunkColBmpBits[Scene.layout[y][x]][0], x * 128 - bounds.X, y * 128 - bounds.Y);
							else if (collisionPath2)
								LevelImg8bpp.DrawBitmapComposited(ChunkColBmpBits[Scene.layout[y][x]][1], x * 128 - bounds.X, y * 128 - bounds.Y);
						}
						else
						{
							if (lowPlane)
								LevelImg8bpp.DrawSpriteLow(ChunkSprites[Scene.layout[y][x]], x * 128 - bounds.X, y * 128 - bounds.Y);
							if (!includeObjects || objectsAboveHighPlane)
							{
								if (highPlane)
									LevelImg8bpp.DrawSpriteHigh(ChunkSprites[Scene.layout[y][x]], x * 128 - bounds.X, y * 128 - bounds.Y);
								if (collisionPath1)
									LevelImg8bpp.DrawBitmapComposited(ChunkColBmpBits[Scene.layout[y][x]][0], x * 128 - bounds.X, y * 128 - bounds.Y);
								else if (collisionPath2)
									LevelImg8bpp.DrawBitmapComposited(ChunkColBmpBits[Scene.layout[y][x]][1], x * 128 - bounds.X, y * 128 - bounds.Y);
							}
						}
					}
			if (includeObjects)
			{
				foreach (Entry item in Objects)
					LevelImg8bpp.DrawSprite(item.Sprite, item.X - bounds.X, item.Y - bounds.Y);
				if (!objectsAboveHighPlane)
					for (int y = ct; y <= cb; y++)
						for (int x = cl; x <= cr; x++)
							if (Scene.layout[y][x] < NewChunks.chunkList.Length)
							{
								if (highPlane)
									LevelImg8bpp.DrawSpriteHigh(ChunkSprites[Scene.layout[y][x]], x * 128 - bounds.X, y * 128 - bounds.Y);
								if (collisionPath1)
									LevelImg8bpp.DrawBitmapComposited(ChunkColBmpBits[Scene.layout[y][x]][0], x * 128 - bounds.X, y * 128 - bounds.Y);
								else if (collisionPath2)
									LevelImg8bpp.DrawBitmapComposited(ChunkColBmpBits[Scene.layout[y][x]][1], x * 128 - bounds.X, y * 128 - bounds.Y);
							}
			}
			return LevelImg8bpp;
		}

		public static BitmapBits DrawBackground(int layer, Rectangle? section, bool lowPlane, bool highPlane, bool collisionPath1, bool collisionPath2)
		{
			Rectangle bounds;
			if (section.HasValue)
				bounds = section.Value;
			else
			{
				int xend = 0;
				int yend = 0;
				for (int y = 0; y < BGHeight[layer]; y++)
					for (int x = 0; x < BGWidth[layer]; x++)
						if (Background.layers[layer].layout[y][x] > 0)
						{
							xend = Math.Max(xend, x);
							yend = Math.Max(yend, y);
						}
				xend++;
				yend++;
				bounds = new Rectangle(0, 0, xend * 128, yend * 128);
			}
			BitmapBits LevelImg8bpp = new BitmapBits(bounds.Size);
			for (int y = Math.Max(bounds.Y / 128, 0); y <= Math.Min((bounds.Bottom - 1) / 128, BGHeight[layer] - 1); y++)
				for (int x = Math.Max(bounds.X / 128, 0); x <= Math.Min((bounds.Right - 1) / 128, BGWidth[layer] - 1); x++)
					if (Background.layers[layer].layout[y][x] < NewChunks.chunkList.Length)
					{
						if (lowPlane && highPlane)
							LevelImg8bpp.DrawSprite(ChunkSprites[Background.layers[layer].layout[y][x]], x * 128 - bounds.X, y * 128 - bounds.Y);
						else if (lowPlane)
							LevelImg8bpp.DrawSpriteLow(ChunkSprites[Background.layers[layer].layout[y][x]], x * 128 - bounds.X, y * 128 - bounds.Y);
						else if (highPlane)
							LevelImg8bpp.DrawSpriteHigh(ChunkSprites[Background.layers[layer].layout[y][x]], x * 128 - bounds.X, y * 128 - bounds.Y);
						if (collisionPath1)
							LevelImg8bpp.DrawBitmapComposited(ChunkColBmpBits[Background.layers[layer].layout[y][x]][0], x * 128 - bounds.X, y * 128 - bounds.Y);
						else if (collisionPath2)
							LevelImg8bpp.DrawBitmapComposited(ChunkColBmpBits[Background.layers[layer].layout[y][x]][1], x * 128 - bounds.X, y * 128 - bounds.Y);
					}
			return LevelImg8bpp;
			throw new NotImplementedException();
		}

		private static void LoadObjectDefinitionFile(string file)
		{
			Log("Loading object definition file \"" + file + "\".");
			Dictionary<string, ObjectData> obj = IniSerializer.Deserialize<Dictionary<string, ObjectData>>(file);
			foreach (KeyValuePair<string, ObjectData> group in obj)
				INIObjDefs[group.Key] = group.Value;
		}

		private static void InitObjectDefinitions()
		{
			IEnumerable<(GameConfig.ObjectInfo objinf, byte ind)> objlist;
			if (StageConfig.loadGlobalObjects)
				objlist = GameConfig.objects.Concat(StageConfig.objects).Select((o, i) => (o, (byte)(i + 1)));
			else
				objlist = StageConfig.objects.Select((o, i) => (o, (byte)(i + 1)));
			List<KeyValuePair<byte, ObjectDefinition>> objdefs = new List<KeyValuePair<byte, ObjectDefinition>>();
			ObjectData emptydata = new ObjectData();
#if !DEBUG
			Parallel.ForEach(objlist, group =>
#else
			foreach (var group in objlist)
#endif
			{
				ObjectData data;
				lock (objdefs)
					data = INIObjDefs.GetValueOrDefault(group.objinf.script, emptydata);
				ObjectDefinition def = null;
				if (data.CodeFile != null)
				{
					string fulltypename = data.CodeType;
					string dllfile = Path.Combine("dllcache", fulltypename + ".dll");
					DateTime modDate = DateTime.MinValue;
					if (File.Exists(dllfile))
						modDate = File.GetLastWriteTime(dllfile);
					string fp = data.CodeFile.Replace('/', Path.DirectorySeparatorChar);
					Log("Loading ObjectDefinition type " + fulltypename + " from \"" + fp + "\"...");
					if (modDate >= File.GetLastWriteTime(fp) & modDate > File.GetLastWriteTime(Application.ExecutablePath))
					{
						Log("Loading type from cached assembly \"" + dllfile + "\"...");
						def = (ObjectDefinition)Activator.CreateInstance(System.Reflection.Assembly.LoadFile(Path.Combine(Environment.CurrentDirectory, dllfile)).GetType(fulltypename));
					}
					else
					{
						Log("Compiling code file...");
						string ext = Path.GetExtension(fp);
						CodeDomProvider pr = null;
						switch (ext.ToLowerInvariant())
						{
							case ".cs":
								pr = new Microsoft.CSharp.CSharpCodeProvider();
								break;
							case ".vb":
								pr = new Microsoft.VisualBasic.VBCodeProvider();
								break;
						}
						if (pr != null)
						{
							CompilerParameters para = new CompilerParameters(new string[] { "System.dll", "System.Core.dll", "System.Drawing.dll", System.Reflection.Assembly.GetExecutingAssembly().Location })
							{
								GenerateExecutable = false,
								GenerateInMemory = false,
								IncludeDebugInformation = true,
								OutputAssembly = Path.Combine(Environment.CurrentDirectory, dllfile)
							};
							CompilerResults res = pr.CompileAssemblyFromFile(para, fp);
							if (res.Errors.HasErrors)
							{
								Log("Compile failed.", "Errors:");
								foreach (CompilerError item in res.Errors)
									Log(item.ToString());
								Log(string.Empty);
								def = new DefaultObjectDefinition();
							}
							else
							{
								Log("Compile succeeded.");
								def = (ObjectDefinition)Activator.CreateInstance(res.CompiledAssembly.GetType(fulltypename));
							}
						}
						else
							def = new DefaultObjectDefinition();
					}
				}
				else if (data.XMLFile != null)
					def = new XMLObjectDefinition();
				else
					def = new DefaultObjectDefinition();
				lock (objdefs)
					objdefs.Add(new KeyValuePair<byte, ObjectDefinition>(group.ind, def));
				def.Init(group.objinf);
				def.Init(data);
#if !DEBUG
			});
#else
			}
#endif
			foreach (var item in objdefs.OrderBy(a => a.Key))
				ObjTypes[item.Key] = item.Value;
		}

		internal static string ExpandTypeName(string type)
		{
			switch (type)
			{
				case "bool":
					return typeof(bool).FullName;
				case "byte":
					return typeof(byte).FullName;
				case "char":
					return typeof(char).FullName;
				case "decimal":
					return typeof(decimal).FullName;
				case "double":
					return typeof(double).FullName;
				case "float":
					return typeof(float).FullName;
				case "int":
					return typeof(int).FullName;
				case "long":
					return typeof(long).FullName;
				case "object":
					return typeof(object).FullName;
				case "sbyte":
					return typeof(sbyte).FullName;
				case "short":
					return typeof(short).FullName;
				case "string":
					return typeof(string).FullName;
				case "uint":
					return typeof(uint).FullName;
				case "ulong":
					return typeof(ulong).FullName;
				case "ushort":
					return typeof(ushort).FullName;
				default:
					return type;
			}
		}

		public static void RedrawBlocks(IEnumerable<int> blocks, bool drawChunks)
		{
			List<int> chunks = new List<int>();
			foreach (int block in blocks)
			{
				RedrawBlock(block, false);
				if (drawChunks)
					for (int i = 0; i < NewChunks.chunkList.Length; i++)
					{
						if (chunks.Contains(i)) continue;
						for (int k = 0; k < 128 / 16; k++)
							for (int j = 0; j < 128 / 16; j++)
								if (NewChunks.chunkList[i].tiles[j][k].tileIndex == block)
								{
									chunks.Add(i);
									goto nextchunk;
								}
						nextchunk:;
					}
			}
			if (drawChunks)
				RedrawChunks(chunks);
		}

		public static void RedrawBlock(int block, bool drawChunks)
		{
			NewTileBmps[block] = NewTiles[block].ToBitmap(BmpPal);
			if (drawChunks)
				for (int i = 0; i < NewChunks.chunkList.Length; i++)
				{
					for (int k = 0; k < 8; k++)
						for (int j = 0; j < 8; j++)
							if (NewChunks.chunkList[i].tiles[j][k].tileIndex == block)
								goto draw;
					continue;
					draw:
					RedrawChunk(i);
				}
		}

		public static void RedrawChunks(IEnumerable<int> chunks)
		{
			foreach (int chunk in chunks)
				RedrawChunk(chunk);
		}

		static readonly Dictionary<Tiles128x128.Block.Tile.Solidities, int> solidcolormap = new Dictionary<Tiles128x128.Block.Tile.Solidities, int>()
		{
			{ Tiles128x128.Block.Tile.Solidities.SolidTop, 0 },
			{ Tiles128x128.Block.Tile.Solidities.SolidAllButTop, 1 },
			{ Tiles128x128.Block.Tile.Solidities.SolidAll, 2 },
			{ (Tiles128x128.Block.Tile.Solidities)4, 0 }
		};
		public static void RedrawChunk(int chunk)
		{
			BitmapBits tmplow = new BitmapBits(128, 128);
			BitmapBits tmphigh = new BitmapBits(128, 128);
			ChunkColBmpBits[chunk][0] = new BitmapBits(128, 128);
			ChunkColBmpBits[chunk][1] = new BitmapBits(128, 128);
			for (int by = 0; by < 8; by++)
				for (int bx = 0; bx < 8; bx++)
				{
					Tiles128x128.Block.Tile blk = NewChunks.chunkList[chunk].tiles[by][bx];
					BitmapBits blkbmp;
					if (blk.tileIndex < NewTiles.Length)
						blkbmp = NewTiles[blk.tileIndex];
					else
						blkbmp = InvalidTile;
					BitmapBits bmp = new BitmapBits(blkbmp);
					bmp.Flip(blk.direction);
					switch (blk.visualPlane)
					{
						case Tiles128x128.Block.Tile.VisualPlanes.Low:
							tmplow.DrawBitmap(bmp, bx * 16, by * 16);
							break;
						case Tiles128x128.Block.Tile.VisualPlanes.High:
							tmphigh.DrawBitmap(bmp, bx * 16, by * 16);
							break;
					}
					if (blk.solidityA != Tiles128x128.Block.Tile.Solidities.SolidNone)
					{
						bmp = new BitmapBits(NewColBmpBits[blk.tileIndex][0]);
						bmp.IncrementIndexes(solidcolormap[blk.solidityA]);
						bmp.Flip(blk.direction);
						ChunkColBmpBits[chunk][0].DrawBitmap(bmp, bx * 16, by * 16);
					}
					if (blk.solidityB != Tiles128x128.Block.Tile.Solidities.SolidNone)
					{
						bmp = new BitmapBits(NewColBmpBits[blk.tileIndex][1]);
						bmp.IncrementIndexes(solidcolormap[blk.solidityB]);
						bmp.Flip(blk.direction);
						ChunkColBmpBits[chunk][1].DrawBitmap(bmp, bx * 16, by * 16);
					}
				}
			ChunkSprites[chunk] = new Sprite(tmplow, tmphigh);
			ChunkBmps[chunk][0] = tmplow.ToBitmap(BmpPal);
			ChunkBmps[chunk][1] = tmphigh.ToBitmap(BmpPal);
			ChunkColBmps[chunk][0] = ChunkColBmpBits[chunk][0].ToBitmap(Color.Transparent, Color.White, Color.Yellow, Color.Black);
			ChunkColBmps[chunk][1] = ChunkColBmpBits[chunk][1].ToBitmap(Color.Transparent, Color.White, Color.Yellow, Color.Black);
			ChunkColBmpBits[chunk][0].FixUIColors();
			ChunkColBmpBits[chunk][1].FixUIColors();
			tmplow.DrawBitmapComposited(tmphigh, 0, 0);
			CompChunkBmps[chunk] = tmplow.ToBitmap(BmpPal);
		}

		public static ObjectDefinition GetObjectDefinition(byte ID)
		{
			if (ObjTypes != null && ObjTypes.ContainsKey(ID))
				return ObjTypes[ID];
			else
				return unkobj;
		}

		public static ObjectEntry CreateObject(byte ID)
		{
			if (Scene.entities.Count < Scene.ENTITY_LIST_SIZE)
			{
				Scene.Entity ent;
				switch (RSDKVer)
				{
					case EngineVersion.V4:
						ent = new RSDKv4.Scene.Entity() { type = ID };
						break;
					case EngineVersion.V3:
						ent = new RSDKv3.Scene.Entity() { type = ID };
						break;
					default:
						return null;
				}
				Scene.entities.Add(ent);
				ObjectEntry obj = ObjectEntry.Create(ent);
				Objects.Add(obj);
				return obj;
			}
			return null;
		}

		public static void AddObject(ObjectEntry obj)
		{
			if (Scene.entities.Count < Scene.ENTITY_LIST_SIZE)
			{
				Objects.Add(obj);
				Scene.entities.Add(obj.Entity);
			}
		}

		public static void DeleteObject(ObjectEntry obj)
		{
			Objects.Remove(obj);
			Scene.entities.Remove(obj.Entity);
		}

		public static void PaletteChanged()
		{
			NewPalette.CopyTo(BmpPal.Entries, 0);
			BmpPal.Entries[ColorTransparent] = Color.Transparent;
			BmpPal.Entries[ColorWhite] = Color.White;
			BmpPal.Entries[ColorYellow] = Color.Yellow;
			BmpPal.Entries[ColorBlack] = Color.Black;
			foreach (Bitmap item in NewTileBmps)
				item.Palette = BmpPal;
			foreach (Bitmap[] item in ChunkBmps)
			{
				item[0].Palette = BmpPal;
				item[1].Palette = BmpPal;
			}
			foreach (Bitmap[] item in ChunkColBmps)
			{
				item[0].Palette = BmpPal;
				item[1].Palette = BmpPal;
			}
			foreach (Bitmap item in CompChunkBmps)
				item.Palette = BmpPal;
			PaletteChangedEvent();
		}

		public static void RedrawCol(int block, bool drawChunks)
		{
			for (int i = 0; i < 2; i++)
			{
				NewColBmpBits[block][i] = new BitmapBits(16, 16);
				TileConfig.CollisionMask mask = Collision.collisionMasks[i][block];
				for (int x = 0; x < 16; x++)
					if (mask.heightMasks[x].solid)
						if (mask.flipY)
						{
							for (int y = 0; y < mask.heightMasks[x].height; y++)
								NewColBmpBits[block][i][x, y] = 1;
						}
						else
						{
							for (int y = 15; y >= mask.heightMasks[x].height; y--)
								NewColBmpBits[block][i][x, y] = 1;
						}
				NewColBmps[block][i] = NewColBmpBits[block][i].ToBitmap(Color.Transparent, Color.White);
			}
			if (drawChunks)
			{
				for (int i = 0; i < NewChunks.chunkList.Length; i++)
				{
					for (int k = 0; k < 8; k++)
						for (int j = 0; j < 8; j++)
							if (NewChunks.chunkList[i].tiles[k][j].tileIndex == block)
								goto draw;
					continue;
					draw:
					RedrawChunk(i);
				}
			}
		}

		public static ImportResult BitmapToTiles(BitmapInfo bmpi, bool[,] priority, ColInfo[][,] collision, byte? forcepal, List<BitmapBits> tiles, List<TileConfig.CollisionMask>[] cols, bool optimize, Action updateProgress = null)
		{
			int w = bmpi.Width / 16;
			int h = bmpi.Height / 16;
			ImportResult result = new ImportResult(w, h, collision != null, collision?[1] != null);
			for (int y = 0; y < h; y++)
				for (int x = 0; x < w; x++)
				{
					Tiles128x128.Block.Tile map = new Tiles128x128.Block.Tile() { visualPlane = priority[x, y] ? Tiles128x128.Block.Tile.VisualPlanes.High : Tiles128x128.Block.Tile.VisualPlanes.Low };
					if (collision != null)
					{
						map.solidityA = collision[0][x, y].Solidity;
						if (collision[1] != null)
							map.solidityB = collision[1][x, y].Solidity;
					}
					BitmapBits tile = BmpToTile(new BitmapInfo(bmpi, x * 16, y * 16, 16, 16), forcepal);
					TileConfig.CollisionMask mask1 = null;
					TileConfig.CollisionMask mask2 = null;
					if (collision != null)
					{
						mask1 = collision[0][x, y].CollisionMask;
						if (collision[1] != null)
							mask2 = collision[1][x, y].CollisionMask;
					}
					bool match = false;
					if (optimize)
					{
						BitmapBits tileh = new BitmapBits(tile);
						tileh.Flip(true, false);
						TileConfig.CollisionMask mask1h = null;
						TileConfig.CollisionMask mask2h = null;
						if (mask1 != null)
						{
							mask1h = mask1.Clone();
							mask1h.Flip(true, false);
							if (mask2 != null)
							{
								mask2h = mask2.Clone();
								mask2h.Flip(true, false);
							}
						}
						BitmapBits tilev = new BitmapBits(tile);
						tileh.Flip(false, true);
						TileConfig.CollisionMask mask1v = null;
						TileConfig.CollisionMask mask2v = null;
						if (mask1 != null)
						{
							mask1v = mask1.Clone();
							mask1v.Flip(false, true);
							if (mask2 != null)
							{
								mask2v = mask2.Clone();
								mask2v.Flip(false, true);
							}
						}
						BitmapBits tilehv = new BitmapBits(tilev);
						tileh.Flip(true, false);
						TileConfig.CollisionMask mask1hv = null;
						TileConfig.CollisionMask mask2hv = null;
						if (mask1v != null)
						{
							mask1hv = mask1v.Clone();
							mask1hv.Flip(true, false);
							if (mask2v != null)
							{
								mask2hv = mask2v.Clone();
								mask2hv.Flip(true, false);
							}
						}
						for (int i = 0; i < tiles.Count; i++)
						{
							if (tiles[i].Bits.FastArrayEqual(tile.Bits)
								&& (mask1 == null || mask1.Equal(cols[0][i]))
								&& (mask2 == null || mask2.Equal(cols[0][i])))
							{
								match = true;
								map.tileIndex = (ushort)i;
								break;
							}
							if (tiles[i].Bits.FastArrayEqual(tileh.Bits)
								&& (mask1h == null || mask1h.Equal(cols[0][i]))
								&& (mask2h == null || mask2h.Equal(cols[0][i])))
							{
								match = true;
								map.tileIndex = (ushort)i;
								map.direction = Tiles128x128.Block.Tile.Directions.FlipX;
								break;
							}
							if (tiles[i].Bits.FastArrayEqual(tilev.Bits)
								&& (mask1v == null || mask1v.Equal(cols[0][i]))
								&& (mask2v == null || mask2v.Equal(cols[0][i])))
							{
								match = true;
								map.tileIndex = (ushort)i;
								map.direction = Tiles128x128.Block.Tile.Directions.FlipY;
								break;
							}
							if (tiles[i].Bits.FastArrayEqual(tilehv.Bits)
								&& (mask1hv == null || mask1hv.Equal(cols[0][i]))
								&& (mask2hv == null || mask2hv.Equal(cols[0][i])))
							{
								match = true;
								map.tileIndex = (ushort)i;
								map.direction = Tiles128x128.Block.Tile.Directions.FlipXY;
								break;
							}
						}
					}
					if (!match)
					{
						tiles.Add(tile);
						result.Art.Add(tile);
						if (mask1 != null)
							result.Collision1.Add(mask1);
						if (mask2 != null)
							result.Collision2.Add(mask2);
						map.tileIndex = (ushort)(tiles.Count - 1);
					}
					result.Mappings[x, y] = map;
					updateProgress?.Invoke();
				}
			return result;
		}

		public static BitmapBits BmpToTile(BitmapInfo bmp, byte? forcepal)
		{
			BitmapBits bmpbits = new BitmapBits(16, 16);
			switch (bmp.PixelFormat)
			{
				case PixelFormat.Format1bppIndexed:
					LoadBitmap1BppIndexed(bmpbits, bmp.Pixels, bmp.Stride);
					if (forcepal.HasValue)
						bmpbits.IncrementIndexes(forcepal.Value * 16);
					break;
				case PixelFormat.Format32bppArgb:
					LoadBitmap32BppArgb(bmpbits, bmp.Pixels, bmp.Stride, NewPalette);
					break;
				case PixelFormat.Format4bppIndexed:
					LoadBitmap4BppIndexed(bmpbits, bmp.Pixels, bmp.Stride);
					if (forcepal.HasValue)
						bmpbits.IncrementIndexes(forcepal.Value * 16);
					break;
				case PixelFormat.Format8bppIndexed:
					LoadBitmap8BppIndexed(bmpbits, bmp.Pixels, bmp.Stride);
					break;
				default:
					throw new Exception("wat");
			}
			return bmpbits;
		}

		public static ColInfo[,] GetColMap(Bitmap bmp)
		{
			if (bmp.PixelFormat != PixelFormat.Format32bppArgb)
				bmp = bmp.To32bpp();
			BitmapBits bmpbits = new BitmapBits(bmp.Width, bmp.Height);
			BitmapData bmpd = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, bmp.PixelFormat);
			int stride = bmpd.Stride;
			byte[] Bits = new byte[Math.Abs(stride) * bmpd.Height];
			System.Runtime.InteropServices.Marshal.Copy(bmpd.Scan0, Bits, 0, Bits.Length);
			bmp.UnlockBits(bmpd);
			LoadBitmap32BppArgb(bmpbits, Bits, stride, new Color[] { Color.Magenta, Color.White, Color.Yellow, Color.Black });
			ColInfo[,] result = new ColInfo[bmpbits.Width / 16, bmpbits.Height / 16];
			Parallel.For(0, bmpbits.Height / 16, by =>
			{
				for (int bx = 0; bx < bmpbits.Width / 16; bx++)
				{
					ushort[] coltypes = new ushort[3];
					TileConfig.CollisionMask mask = new TileConfig.CollisionMask();
					int fcnt = 0;
					int ccnt = 0;
					for (int x = 0; x < 16; x++)
					{
						if (bmpbits[bx * 16 + x, by * 16] != 0)
						{
							if (bmpbits[bx * 16 + x, by * 16 + 15] == 0)
								++ccnt;
						}
						else if (bmpbits[bx * 16 + x, by * 16 + 15] != 0)
							++fcnt;
					}
					if (ccnt > fcnt)
						mask.flipY = true;
					Point? start = null;
					Point? end = null;
					for (int x = 0; x < 16; x++)
					{
						if (mask.flipY)
						{
							for (int y = 0; y < 16; y++)
							{
								if (bmpbits[bx * 16 + x, by * 16 + y] != 0)
								{
									coltypes[bmpbits[bx * 16 + x, by * 16 + y] - 1]++;
									mask.heightMasks[x].solid = true;
									mask.heightMasks[x].height = (byte)y;
								}
								else
								{
									if (mask.heightMasks[x].solid)
									{
										if (!start.HasValue)
											start = new Point(x, y - 1);
										end = new Point(x, y - 1);
									}
									break;
								}
							}
							if (mask.heightMasks[x].solid && mask.heightMasks[x].height == 15)
							{
								if (!start.HasValue || start.Value.Y == 15)
									start = new Point(x, 15);
								if (!end.HasValue || end.Value.Y != 15)
									end = new Point(x, 15);
							}
						}
						else
						{
							for (int y = 15; y >= 0; y--)
							{
								if (bmpbits[bx * 16 + x, by * 16 + y] != 0)
								{
									coltypes[bmpbits[bx * 16 + x, by * 16 + y] - 1]++;
									mask.heightMasks[x].solid = true;
									mask.heightMasks[x].height = (byte)y;
								}
								else
								{
									if (mask.heightMasks[x].solid)
									{
										if (!start.HasValue)
											start = new Point(x, y + 1);
										end = new Point(x, y + 1);
									}
									break;
								}
							}
							if (mask.heightMasks[x].solid && mask.heightMasks[x].height == 0)
							{
								if (!start.HasValue || start.Value.Y == 0)
									start = new Point(x, 0);
								if (!end.HasValue || end.Value.Y != 0)
									end = new Point(x, 0);
							}
						}
					}
					Tiles128x128.Block.Tile.Solidities solid = Tiles128x128.Block.Tile.Solidities.SolidNone;
					if (start.HasValue)
					{
						solid = Tiles128x128.Block.Tile.Solidities.SolidTop;
						ushort max = coltypes[0];
						if (coltypes[1] > max)
						{
							solid = Tiles128x128.Block.Tile.Solidities.SolidAllButTop;
							max = coltypes[1];
						}
						if (coltypes[2] > max)
							solid = Tiles128x128.Block.Tile.Solidities.SolidAll;
						if (start.Value.Y != end.Value.Y)
							if (mask.flipY)
							{
								mask.roofAngle = (byte)(Math.Atan2(end.Value.Y - start.Value.Y, end.Value.X - start.Value.X) * (256 / (2 * Math.PI)) + 0x80);
								switch (mask.roofAngle.CompareTo(0x80))
								{
									case -1:
										mask.rWallAngle = mask.roofAngle;
										break;
									case 1:
										mask.lWallAngle = mask.roofAngle;
										break;
								}
							}
							else
							{
								mask.floorAngle = (byte)(Math.Atan2(end.Value.Y - start.Value.Y, end.Value.X - start.Value.X) * (256 / (2 * Math.PI)));
								switch (mask.floorAngle.CompareTo(0x80))
								{
									case -1:
										mask.rWallAngle = mask.floorAngle;
										break;
									case 1:
										mask.lWallAngle = mask.floorAngle;
										break;
								}
							}
					}
					result[bx, by] = new ColInfo(solid, mask);
				}
			});
			return result;
		}

		public static void GetPriMap(Bitmap bmp, bool[,] primap)
		{
			if (bmp.PixelFormat != PixelFormat.Format32bppArgb)
				bmp = bmp.To32bpp();
			BitmapBits bmpbits = new BitmapBits(bmp.Width, bmp.Height);
			BitmapData bmpd = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, bmp.PixelFormat);
			int stride = bmpd.Stride;
			byte[] Bits = new byte[Math.Abs(stride) * bmpd.Height];
			System.Runtime.InteropServices.Marshal.Copy(bmpd.Scan0, Bits, 0, Bits.Length);
			bmp.UnlockBits(bmpd);
			LoadBitmap32BppArgb(bmpbits, Bits, stride, new Color[] { Color.Black, Color.White });
			int w = Math.Min(primap.GetLength(0), bmpbits.Width / 16);
			int h = Math.Min(primap.GetLength(1), bmpbits.Height / 16);
			Parallel.For(0, h, ty =>
			{
				for (int tx = 0; tx < w; tx++)
				{
					ushort[] cnt = new ushort[2];
					for (int y = 0; y < 16; y++)
						for (int x = 0; x < 16; x++)
							cnt[bmpbits[(tx * 16) + x, (ty * 16) + y]]++;
					primap[tx, ty] = cnt[0] < cnt[1];
				}
			});
		}

		private static void LoadBitmap1BppIndexed(BitmapBits bmp, byte[] Bits, int Stride)
		{
			int dstaddr = 0;
			for (int y = 0; y < bmp.Height; y++)
			{
				int srcaddr = y * Math.Abs(Stride);
				for (int x = 0; x < bmp.Width; x += 8)
				{
					byte b = Bits[srcaddr++];
					bmp.Bits[dstaddr++] = (byte)(b >> 7 & 1);
					bmp.Bits[dstaddr++] = (byte)(b >> 6 & 1);
					bmp.Bits[dstaddr++] = (byte)(b >> 5 & 1);
					bmp.Bits[dstaddr++] = (byte)(b >> 4 & 1);
					bmp.Bits[dstaddr++] = (byte)(b >> 3 & 1);
					bmp.Bits[dstaddr++] = (byte)(b >> 2 & 1);
					bmp.Bits[dstaddr++] = (byte)(b >> 1 & 1);
					bmp.Bits[dstaddr++] = (byte)(b & 1);
				}
			}
		}

		private static void LoadBitmap32BppArgb(BitmapBits bmp, byte[] Bits, int Stride, Color[] palette)
		{
			int dstaddr = 0;
			for (int y = 0; y < bmp.Height; y++)
			{
				int srcaddr = y * Math.Abs(Stride);
				for (int x = 0; x < bmp.Width; x++)
				{
					Color col = Color.FromArgb(BitConverter.ToInt32(Bits, srcaddr + (x * 4)));
					if (col.A >= 128)
						bmp.Bits[dstaddr++] = (byte)col.FindNearestMatch(palette);
				}
			}
		}

		private static void LoadBitmap4BppIndexed(BitmapBits bmp, byte[] Bits, int Stride)
		{
			int dstaddr = 0;
			for (int y = 0; y < bmp.Height; y++)
			{
				int srcaddr = y * Math.Abs(Stride);
				for (int x = 0; x < bmp.Width; x += 2)
				{
					byte b = Bits[srcaddr++];
					bmp.Bits[dstaddr++] = (byte)(b >> 4);
					bmp.Bits[dstaddr++] = (byte)(b & 0xF);
				}
			}
		}

		private static void LoadBitmap8BppIndexed(BitmapBits bmp, byte[] Bits, int Stride)
		{
			int dstaddr = 0;
			for (int y = 0; y < bmp.Height; y++)
			{
				int srcaddr = y * Math.Abs(Stride);
				for (int x = 0; x < bmp.Width; x++)
					bmp.Bits[dstaddr++] = Bits[srcaddr++];
			}
		}

		public static void Log(params string[] message) { LogEvent(message); }

		public static Bitmap BitmapBitsToBitmap(BitmapBits bmp)
		{
			return bmp.ToBitmap(BmpPal);
		}

		public static BitmapBits GetSpriteSheet(string sheetname)
		{
			lock (spriteSheets)
			{
				sheetname = "Data/Sprites/" + sheetname;
				if (spriteSheets.TryGetValue(sheetname, out BitmapBits bits))
					return bits;
				BitmapBits img = new BitmapBits(LevelData.ReadFile<Gif>(sheetname));
				spriteSheets.Add(sheetname, img);
				return img;
			}
		}

		public static Size FGSize { get { return new Size(FGWidth, FGHeight); } }

		public static int FGWidth { get { return Scene.width; } }
		public static int FGHeight { get { return Scene.height; } }

		public static readonly Indexer<int, Size> BGSize = new Indexer<int, Size>(ind => new Size(BGWidth[ind], BGHeight[ind]));

		public static readonly Indexer<int, int> BGWidth = new Indexer<int, int>(ind => Background.layers[ind].width);
		public static readonly Indexer<int, int> BGHeight = new Indexer<int, int>(ind => Background.layers[ind].height);

		public static void ResizeFG(Size newSize) { ResizeFG(newSize.Width, newSize.Height); }

		public static void ResizeFG(int width, int height)
		{
			Scene.resize((byte)width, (byte)height);
		}

		public static void ResizeBG(int layer, Size newSize) { ResizeBG(layer, newSize.Width, newSize.Height); }

		public static void ResizeBG(int layer, int width, int height)
		{
			Background.layers[layer].resize((byte)width, (byte)height);
		}

		public static void RemapLayouts(Action<ushort[][], int, int> func)
		{
			for (int y = 0; y < FGHeight; y++)
				for (int x = 0; x < FGWidth; x++)
					func(Scene.layout, x, y);
			for (int i = 0; i < 8; i++)
				for (int y = 0; y < BGHeight[i]; y++)
					for (int x = 0; x < BGWidth[i]; x++)
						func(Background.layers[i].layout, x, y);
		}

		public static Tiles128x128.Block Flip(this Tiles128x128.Block chunk, bool xflip, bool yflip)
		{
			Tiles128x128.Block result = new Tiles128x128.Block();
			if (xflip)
			{
				if (yflip)
				{
					for (int y = 0; y < 8; y++)
						for (int x = 0; x < 8; x++)
						{
							result.tiles[y][x] = chunk.tiles[7 - y][7 - x].Clone();
							result.tiles[y][x].direction ^= Tiles128x128.Block.Tile.Directions.FlipXY;
						}
				}
				else
				{
					for (int y = 0; y < 8; y++)
						for (int x = 0; x < 8; x++)
						{
							result.tiles[y][x] = chunk.tiles[y][7 - x].Clone();
							result.tiles[y][x].direction ^= Tiles128x128.Block.Tile.Directions.FlipX;
						}
				}
			}
			else if (yflip)
			{
				for (int y = 0; y < 8; y++)
					for (int x = 0; x < 8; x++)
					{
						result.tiles[y][x] = chunk.tiles[7 - y][x].Clone();
						result.tiles[y][x].direction ^= Tiles128x128.Block.Tile.Directions.FlipY;
					}
			}
			else
				result = chunk.Clone();
			return result;
		}

		public static Tiles128x128.Block Clone(this Tiles128x128.Block src)
		{
			Tiles128x128.Block result = new Tiles128x128.Block();
			for (int y = 0; y < 8; y++)
				for (int x = 0; x < 8; x++)
					result.tiles[y][x] = src.tiles[y][x].Clone();
			return result;
		}

		public static bool Equal(this Tiles128x128.Block src, Tiles128x128.Block other)
		{
			for (int y = 0; y < 8; y++)
				for (int x = 0; x < 8; x++)
					if (!src.tiles[y][x].Equal(other.tiles[y][x]))
						return false;
			return true;
		}

		public static Tiles128x128.Block.Tile Clone(this Tiles128x128.Block.Tile src) => new Tiles128x128.Block.Tile()
		{
			direction = src.direction,
			solidityA = src.solidityA,
			solidityB = src.solidityB,
			tileIndex = src.tileIndex,
			visualPlane = src.visualPlane
		};

		public static bool Equal(this Tiles128x128.Block.Tile src, Tiles128x128.Block.Tile other)
		{
			return src.direction == other.direction
				&& src.solidityA == other.solidityA
				&& src.solidityB == other.solidityB
				&& src.tileIndex == other.tileIndex
				&& src.visualPlane == other.visualPlane;
		}

		public static bool HasFreeTiles()
		{
			return Enumerable.Range(0, NewTiles.Length).Except(NewChunks.chunkList.SelectMany(a => a.tiles.SelectMany(b => b).Select(c => c.tileIndex)).Select(a => (int)a))
				.Any(c => NewTiles[c].Bits.FastArrayEqual(0));
		}

		public static bool HasFreeChunks()
		{
			return Enumerable.Range(0, NewChunks.chunkList.Length).Except(Scene.layout.SelectMany(a => a).Union(Background.layers.SelectMany(a => a.layout.SelectMany(b => b))).Select(a => (int)a))
				.Any(c => NewChunks.chunkList[c].tiles.SelectMany(a => a).All(b =>
				  b.direction == Tiles128x128.Block.Tile.Directions.FlipNone && b.solidityA == Tiles128x128.Block.Tile.Solidities.SolidNone
				  && b.solidityB == Tiles128x128.Block.Tile.Solidities.SolidNone && b.tileIndex == 0 && b.visualPlane == Tiles128x128.Block.Tile.VisualPlanes.Low
			));
		}

		public static IEnumerable<ushort> GetFreeTiles()
		{
			return Enumerable.Range(0, NewTiles.Length).Select(a => (ushort)a).Except(NewChunks.chunkList.SelectMany(a => a.tiles.SelectMany(b => b).Select(c => c.tileIndex)))
				.Where(c => NewTiles[c].Bits.FastArrayEqual(0));
		}

		public static IEnumerable<ushort> GetFreeChunks()
		{
			return Enumerable.Range(0, NewChunks.chunkList.Length).Select(a => (ushort)a).Except(Scene.layout.SelectMany(a => a).Union(Background.layers.SelectMany(a => a.layout.SelectMany(b => b))))
				.Where(c => NewChunks.chunkList[c].tiles.SelectMany(a => a).All(b =>
				  b.direction == Tiles128x128.Block.Tile.Directions.FlipNone && b.solidityA == Tiles128x128.Block.Tile.Solidities.SolidNone
				  && b.solidityB == Tiles128x128.Block.Tile.Solidities.SolidNone && b.tileIndex == 0 && b.visualPlane == Tiles128x128.Block.Tile.VisualPlanes.Low
			));
		}

		public static void CalcAngles(this TileConfig.CollisionMask mask)
		{
			mask.floorAngle = 0;
			mask.rWallAngle = 0x40;
			mask.roofAngle = 0x80;
			mask.lWallAngle = 0xC0;
			Point? start = null;
			Point? end = null;
			for (int x = 0; x < 16; x++)
			{
				if (mask.heightMasks[x].solid)
				{
					if (!start.HasValue)
						start = new Point(x, mask.heightMasks[x].height);
					if (mask.flipY)
					{
						if (mask.heightMasks[x].height == 15 && start.Value.Y == 15)
							start = new Point(x, 15);
						if (!end.HasValue || end.Value.Y != 15)
							end = new Point(x, mask.heightMasks[x].height);
					}
					else
					{
						if (mask.heightMasks[x].height == 0 && start.Value.Y == 0)
							start = new Point(x, 0);
						if (!end.HasValue || end.Value.Y != 0)
							end = new Point(x, mask.heightMasks[x].height);
					}
				}
			}
			if (start.HasValue && start.Value.Y != end.Value.Y)
				if (mask.flipY)
				{
					mask.roofAngle = (byte)(Math.Atan2(end.Value.Y - start.Value.Y, end.Value.X - start.Value.X) * (256 / (2 * Math.PI)) + 0x80);
					switch (mask.roofAngle.CompareTo(0x80))
					{
						case -1:
							mask.rWallAngle = mask.roofAngle;
							break;
						case 1:
							mask.lWallAngle = mask.roofAngle;
							break;
					}
				}
				else
				{
					mask.floorAngle = (byte)(Math.Atan2(end.Value.Y - start.Value.Y, end.Value.X - start.Value.X) * (256 / (2 * Math.PI)));
					switch (mask.floorAngle.CompareTo(0x80))
					{
						case -1:
							mask.rWallAngle = mask.floorAngle;
							break;
						case 1:
							mask.lWallAngle = mask.floorAngle;
							break;
					}
				}
		}

		public static void Flip(this TileConfig.CollisionMask mask, bool xflip, bool yflip)
		{
			if (xflip)
			{
				Array.Reverse(mask.heightMasks);
				if (yflip)
				{
					mask.flipY = !mask.flipY;
					for (int i = 0; i < 16; i++)
						if (mask.heightMasks[i].solid)
							mask.heightMasks[i].height = (byte)(15 - mask.heightMasks[i].height);
					if (!mask.flipY)
					{
						if (mask.roofAngle != 0x80)
						{
							mask.floorAngle = (byte)((mask.roofAngle + 0x80) & 0xFF);
							switch (mask.floorAngle.CompareTo(0x80))
							{
								case -1:
									mask.rWallAngle = mask.floorAngle;
									mask.lWallAngle = 0xC0;
									break;
								case 1:
									mask.lWallAngle = mask.floorAngle;
									mask.rWallAngle = 0x40;
									break;
							}
						}
					}
					else
					{
						if (mask.floorAngle != 0)
						{
							mask.roofAngle = (byte)((mask.floorAngle + 0x80) & 0xFF);
							switch (mask.roofAngle.CompareTo(0x80))
							{
								case -1:
									mask.rWallAngle = mask.roofAngle;
									mask.lWallAngle = 0xC0;
									break;
								case 1:
									mask.lWallAngle = mask.roofAngle;
									mask.rWallAngle = 0x40;
									break;
							}
						}
					}
				}
				else if (mask.flipY)
				{
					if (mask.roofAngle != 0x80)
					{
						mask.roofAngle = (byte)(-mask.roofAngle & 0xFF);
						switch (mask.roofAngle.CompareTo(0x80))
						{
							case -1:
								mask.rWallAngle = mask.roofAngle;
								mask.lWallAngle = 0xC0;
								break;
							case 1:
								mask.lWallAngle = mask.roofAngle;
								mask.rWallAngle = 0x40;
								break;
						}
					}
				}
				else
				{
					if (mask.floorAngle != 0)
					{
						mask.floorAngle = (byte)(-mask.floorAngle & 0xFF);
						switch (mask.floorAngle.CompareTo(0x80))
						{
							case -1:
								mask.rWallAngle = mask.floorAngle;
								mask.lWallAngle = 0xC0;
								break;
							case 1:
								mask.lWallAngle = mask.floorAngle;
								mask.rWallAngle = 0x40;
								break;
						}
					}
				}
			}
			else if (yflip)
			{
				mask.flipY = !mask.flipY;
				for (int i = 0; i < 16; i++)
					if (mask.heightMasks[i].solid)
						mask.heightMasks[i].height = (byte)(15 - mask.heightMasks[i].height);
				if (!mask.flipY)
				{
					if (mask.roofAngle != 0x80)
					{
						mask.floorAngle = (byte)((-(mask.roofAngle + 0x40) - 0x40) & 0xFF);
						switch (mask.floorAngle.CompareTo(0x80))
						{
							case -1:
								mask.rWallAngle = mask.floorAngle;
								mask.lWallAngle = 0xC0;
								break;
							case 1:
								mask.lWallAngle = mask.floorAngle;
								mask.rWallAngle = 0x40;
								break;
						}
					}
				}
				else
				{
					if (mask.floorAngle != 0)
					{
						mask.roofAngle = (byte)((-(mask.floorAngle + 0x40) - 0x40) & 0xFF);
						switch (mask.roofAngle.CompareTo(0x80))
						{
							case -1:
								mask.rWallAngle = mask.roofAngle;
								mask.lWallAngle = 0xC0;
								break;
							case 1:
								mask.lWallAngle = mask.roofAngle;
								mask.rWallAngle = 0x40;
								break;
						}
					}
				}
			}
		}

		public static TileConfig.CollisionMask Clone(this TileConfig.CollisionMask src)
		{
			TileConfig.CollisionMask result = new TileConfig.CollisionMask()
			{
				flags = src.flags,
				flipY = src.flipY,
				floorAngle = src.floorAngle,
				rWallAngle = src.rWallAngle,
				roofAngle = src.roofAngle,
				lWallAngle = src.lWallAngle
			};
			for (int i = 0; i < 16; i++)
				if (result.heightMasks[i].solid = src.heightMasks[i].solid)
					result.heightMasks[i].height = src.heightMasks[i].height;
			return result;
		}

		public static bool Equal(this TileConfig.CollisionMask src, TileConfig.CollisionMask other)
		{
			if (src.flags == other.flags
				&& src.flipY == other.flipY
				&& src.floorAngle == other.floorAngle
				&& src.rWallAngle == other.rWallAngle
				&& src.roofAngle == other.roofAngle
				&& src.lWallAngle == other.lWallAngle)
			{
				for (int i = 0; i < 16; i++)
				{
					if (src.heightMasks[i].solid != other.heightMasks[i].solid)
						return false;
					if (src.heightMasks[i].solid)
						if (src.heightMasks[i].height != other.heightMasks[i].height)
							return false;
				}
				return true;
			}
			return false;
		}

		public static bool Equal(this Backgrounds.ScrollInfo src, Backgrounds.ScrollInfo other) => src.deform == other.deform && src.parallaxFactor == other.parallaxFactor && src.scrollSpeed == other.scrollSpeed;

		public static RSDKv3.Scene.Entity Clone(this RSDKv3.Scene.Entity src) => new RSDKv3.Scene.Entity(src.type, src.propertyValue, src.xpos, src.ypos);

		public static RSDKv4.Scene.Entity Clone(this RSDKv4.Scene.Entity src) => new RSDKv4.Scene.Entity(src.type, src.propertyValue, src.xpos, src.ypos)
		{
			alpha = src.alpha,
			animation = src.animation,
			animationSpeed = src.animationSpeed,
			direction = src.direction,
			drawOrder = src.drawOrder,
			frame = src.frame,
			inkEffect = src.inkEffect,
			priority = src.priority,
			rotation = src.rotation,
			scale = src.scale,
			state = src.state,
			value0 = src.value0,
			value1 = src.value1,
			value2 = src.value2,
			value3 = src.value3
		};
	}

	public enum EngineVersion
	{
		Invalid,
		V3,
		V4
	}

	[Serializable]
	public class ColInfo
	{
		public Tiles128x128.Block.Tile.Solidities Solidity { get; }
		public TileConfig.CollisionMask CollisionMask { get; }

		public ColInfo()
		{
			Solidity = Tiles128x128.Block.Tile.Solidities.SolidNone;
			CollisionMask = new TileConfig.CollisionMask();
		}

		public ColInfo(Tiles128x128.Block.Tile.Solidities solidity, TileConfig.CollisionMask mask)
		{
			Solidity = solidity;
			CollisionMask = mask;
		}
	}

	public class BitmapInfo
	{
		public int Width { get; }
		public int Height { get; }
		public Size Size => new Size(Width, Height);
		public PixelFormat PixelFormat { get; }
		public int Stride { get; }
		public byte[] Pixels { get; }

		public BitmapInfo(Bitmap bitmap)
		{
			Width = bitmap.Width;
			Height = bitmap.Height;
			switch (bitmap.PixelFormat)
			{
				case PixelFormat.Format1bppIndexed:
				case PixelFormat.Format32bppArgb:
				case PixelFormat.Format4bppIndexed:
				case PixelFormat.Format8bppIndexed:
					PixelFormat = bitmap.PixelFormat;
					break;
				default:
					bitmap = bitmap.To32bpp();
					PixelFormat = PixelFormat.Format32bppArgb;
					break;
			}
			BitmapData bmpd = bitmap.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.ReadOnly, PixelFormat);
			Stride = Math.Abs(bmpd.Stride);
			Pixels = new byte[Stride * Height];
			System.Runtime.InteropServices.Marshal.Copy(bmpd.Scan0, Pixels, 0, Pixels.Length);
			bitmap.UnlockBits(bmpd);
		}

		public BitmapInfo(BitmapInfo source, int x, int y, int width, int height)
		{
			switch (source.PixelFormat)
			{
				case PixelFormat.Format1bppIndexed:
					if (x % 8 != 0)
						throw new FormatException("X coordinate of 1bpp image section must be multiple of 8.");
					if (width % 8 != 0)
						throw new FormatException("Width of 1bpp image section must be multiple of 8.");
					break;
				case PixelFormat.Format4bppIndexed:
					if (x % 2 != 0)
						throw new FormatException("X coordinate of 4bpp image section must be multiple of 2.");
					if (width % 2 != 0)
						throw new FormatException("Width of 4bpp image section must be multiple of 2.");
					break;
			}
			Width = width;
			Height = height;
			PixelFormat = source.PixelFormat;
			switch (PixelFormat)
			{
				case PixelFormat.Format1bppIndexed:
					Stride = width / 8;
					x /= 8;
					break;
				case PixelFormat.Format4bppIndexed:
					Stride = width / 2;
					x /= 2;
					break;
				case PixelFormat.Format8bppIndexed:
					Stride = width;
					break;
				case PixelFormat.Format32bppArgb:
					Stride = width * 4;
					x *= 4;
					break;
			}
			Pixels = new byte[height * Stride];
			for (int v = 0; v < height; v++)
				Array.Copy(source.Pixels, ((y + v) * source.Stride) + x, Pixels, v * Stride, Stride);
		}

		public Bitmap ToBitmap()
		{
			Bitmap bitmap = new Bitmap(Width, Height, PixelFormat);
			BitmapData bmpd = bitmap.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.WriteOnly, PixelFormat);
			byte[] bmpbits = new byte[Height * Math.Abs(bmpd.Stride)];
			for (int y = 0; y < Height; y++)
				Array.Copy(Pixels, y * Stride, bmpbits, y * Math.Abs(bmpd.Stride), Width);
			System.Runtime.InteropServices.Marshal.Copy(bmpbits, 0, bmpd.Scan0, bmpbits.Length);
			bitmap.UnlockBits(bmpd);
			return bitmap;
		}
	}

	public class ImportResult
	{
		public Tiles128x128.Block.Tile[,] Mappings { get; }
		public List<BitmapBits> Art { get; }
		public List<TileConfig.CollisionMask> Collision1 { get; }
		public List<TileConfig.CollisionMask> Collision2 { get; }

		public ImportResult(int width, int height, bool col1, bool col2)
		{
			Mappings = new Tiles128x128.Block.Tile[width, height];
			Art = new List<BitmapBits>();
			if (col1)
				Collision1 = new List<TileConfig.CollisionMask>();
			if (col2)
				Collision2 = new List<TileConfig.CollisionMask>();
		}
	}

	public class Indexer<TIndex, TResult>
	{
		readonly Func<TIndex, TResult> func;

		public Indexer(Func<TIndex, TResult> func)
		{
			this.func = func;
		}

		public TResult this[TIndex index]
		{
			get => func(index);
		}
	}
}
