using SonicRetro.SonLVL.API;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace S2ObjectDefinitions.MPZ
{
	class Transporter : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[2];
		private Sprite sprite;

		public override void Init(ObjectData data)
		{
			sprite = new Sprite(LevelData.GetSpriteSheet("Global/Display.gif").GetSection(168, 18, 16, 16), -8, -8);
			
			// we can't make debug visualisations on startup because the start is dependant on the transporter's starting position, so
			
			properties[0] = new PropertySpec("Path", typeof(int), "Extended",
				"Which path this Transporter should lead the player. Note that positions are coded within the script and are not relative to the object's position.", null, new Dictionary<string, int>
				{
					{ "Left, Down, Right", 0 },
					{ "Right", 1 },
					{ "Left, Down, Right (Alt)", 2 },
					{ "Type 3", 3 },
					{ "Type 4", 4 },
					{ "Right, Up, Left", 5 },
					{ "Left (Long), Up, Right", 6 },
					{ "Right, Up, Left (Alt 1)", 7 },
					{ "Right, Down, Left", 8 },
					{ "Type 9", 9 },
					{ "Type 10", 10 },
					{ "Left, Up, Right", 11 },
					{ "Right, Up, Left (Alt 2)", 12 }
				},
				(obj) => obj.PropertyValue & 15,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~15) | (int)value));
			
			properties[1] = new PropertySpec("Exit Type", typeof(int), "Extended",
				"How the player should exit this Transporter.", null, new Dictionary<string, int>
				{
					{ "Transporter Exit", 0 },
					{ "Shoot Out Exit", 16 }
				},
				(obj) => obj.PropertyValue & 16,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~16) | (int)value));
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 }); }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			switch (subtype)
			{
				case 0: return "Left, Down, Right";
				case 1: return "Right";
				case 2: return "Left, Down, Right (Alt)";
				case 3: return "Type 3";
				case 4: return "Type 4";
				case 5: return "Right, Up, Left";
				case 6: return "Left (Long), Up, Right";
				case 7: return "Right, Up, Left (Alt 1)";
				case 8: return "Right, Down, Left";
				case 9: return "Type 9";
				case 10: return  "Type 10";
				case 11: return "Left, Up, Right";
				case 12: return "Right, Up, Left (Alt 2)";
				
				default: return "Unknown";
			}
		}

		public override Sprite Image
		{
			get { return sprite; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprite;
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprite;
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			// this sucks btw
			
			// Copied from the original script
			int[][] movementTables = new int[][] {
				  new int[] { 0x7A80000, 0x2700000,
				  0x7500000, 0x2700000,
				  0x7400000, 0x2800000,
				  0x7400000, 0x3E00000,
				  0x7500000, 0x3F00000,
				  0x7A80000, 0x3F00000 },
				  new int[] { 0xC580000, 0x5F00000,
				  0xE280000, 0x5F00000 },
				  new int[] { 0x18280000, 0x6B00000,
				  0x17D00000, 0x6B00000,
				  0x17C00000, 0x6C00000,
				  0x17C00000, 0x7E00000,
				  0x17B00000, 0x7F00000,
				  0x17580000, 0x7F00000 },
				  new int[] { 0x5D80000, 0x3700000,
				  0x7800000, 0x3700000 },
				  new int[] { 0x5D80000, 0x5F00000,
				  0x7000000, 0x5F00000 },
				  new int[] { 0xBD80000, 0x1F00000,
				  0xC300000, 0x1F00000,
				  0xC400000, 0x1E00000,
				  0xC400000, 0xC00000,
				  0xC500000, 0xB00000,
				  0xCA80000, 0xB00000 },
				  new int[] { 0x17280000, 0x3300000,
				  0x15D00000, 0x3300000,
				  0x15C00000, 0x3200000,
				  0x15C00000, 0x2400000,
				  0x15D00000, 0x2300000,
				  0x16280000, 0x2300000 },
				  new int[] { 0x6D80000, 0x1F00000,
				  0x7300000, 0x1F00000,
				  0x7400000, 0x1E00000,
				  0x7400000, 0x1000000,
				  0x7500000, 0xF00000,
				  0x7A80000, 0xF00000 },
				  new int[] { 0x7D80000, 0x3300000,
				  0x8280000, 0x3300000,
				  0x8400000, 0x3400000,
				  0x8400000, 0x4580000,
				  0x8280000, 0x4700000,
				  0x7D80000, 0x4700000 },
				  new int[] { 0xFD80000, 0x3B00000,
				  0x10280000, 0x3B00000,
				  0x10400000, 0x3980000,
				  0x10400000, 0x2C40000,
				  0x10580000, 0x2B00000,
				  0x10A80000, 0x2B00000 },
				  new int[] { 0xFD80000, 0x4B00000,
				  0x10280000, 0x4B00000,
				  0x10400000, 0x4C00000,
				  0x10400000, 0x5D80000,
				  0x10580000, 0x5F00000,
				  0x10A80000, 0x5F00000 },
				  new int[] { 0x20580000, 0x4300000,
				  0x20A80000, 0x4300000,
				  0x20C00000, 0x4180000,
				  0x20C00000, 0x2C00000,
				  0x20D00000, 0x2B00000,
				  0x21280000, 0x2B00000 },
				  new int[] { 0x23280000, 0x5B00000,
				  0x22D00000, 0x5B00000,
				  0x22C00000, 0x5A00000,
				  0x22C00000, 0x4C00000,
				  0x22D00000, 0x4B00000,
				  0x23280000, 0x4B00000 }
			};
			
			int index = obj.PropertyValue & 15;
			
			if (index >= movementTables.Length)
				return null;
			
			int xmin = obj.X;
			int ymin = obj.Y;
			int xmax = obj.X;
			int ymax = obj.Y;
			
			for (int i = 0; i < movementTables[index].Length; i += 2)
			{
				xmin = Math.Min(xmin, movementTables[index][i] >> 16);
				ymin = Math.Min(ymin, movementTables[index][i+1] >> 16);
				xmax = Math.Max(xmax, movementTables[index][i] >> 16);
				ymax = Math.Max(ymax, movementTables[index][i+1] >> 16);
			}
			
			BitmapBits bmp = new BitmapBits(xmax - xmin + 1, ymax - ymin + 1);
			
			bmp.DrawLine(6, obj.X - xmin, obj.Y - ymin, (movementTables[index][0] >> 16) - xmin, (movementTables[index][1] >> 16) - ymin); // LevelData.ColorWhite
			
			for (int i = 2; i < movementTables[index].Length; i += 2)
			{
				bmp.DrawLine(6, (movementTables[index][i-2] >> 16) - xmin, (movementTables[index][i-1] >> 16) - ymin, (movementTables[index][i] >> 16) - xmin, (movementTables[index][i+1] >> 16) - ymin); // LevelData.ColorWhite
			}
			
			return new Sprite(bmp, xmin - obj.X, ymin - obj.Y);
		}
	}
}