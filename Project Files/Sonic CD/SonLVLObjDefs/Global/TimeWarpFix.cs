using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.Global
{
	class TimeWarpFix : ObjectDefinition
	{
		private Sprite sprite;
		private Sprite[] debug = new Sprite[3];
		private PropertySpec[] properties;

		public override void Init(ObjectData data)
		{
			sprite = new Sprite(LevelData.GetSpriteSheet("Global/Display.gif").GetSection(173, 67, 16, 16), -8, -8);
			
			properties = new PropertySpec[1];
			properties[0] = new PropertySpec("Fix Type", typeof(int), "Extended",
                "The type of \"fix\" this Object will apply.", null, new Dictionary<string, int>
				{
					{ "Erase - Touch", 0 },
					{ "Erase - Pass", 1 },
					{ "Pos Fix - Ground", 2 },
					{ "Pos Fix - Air", 3 },
					{ "Force Erase - Touch (Origins)", 4 }
				},
                (obj) => (int)obj.PropertyValue,
                (obj, value) => obj.PropertyValue = (byte)((int)value));
			
			BitmapBits bitmap = new BitmapBits(32, 32);
			bitmap.DrawRectangle(6, 0, 0, 31, 31); // LevelData.ColorWhite
			debug[0] = new Sprite(bitmap, -16, -16);
			
			bitmap = new BitmapBits(96, 96);
			bitmap.DrawRectangle(6, 0, 0, 95, 95); // LevelData.ColorWhite
			debug[1] = new Sprite(bitmap, -48, -48);
			
			bitmap = new BitmapBits(192, 192);
			bitmap.DrawRectangle(6, 0, 0, 191, 191); // LevelData.ColorWhite
			debug[2] = new Sprite(bitmap, -96, -96);
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] { 0, 1, 2, 3, 4 }); }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			switch (subtype)
			{
				case 0: return "Erase - Touch";
				case 1: return "Erase - Pass";
				case 2: return "Pos Fix - Ground";
				case 3: return "Pos Fix - Air";
				case 4: return "Force Erase - Touch (Origins)";
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
			switch (obj.PropertyValue)
			{
				case 0:
				case 4: // Origins only
					return debug[0];
				case 1:
				default:
					return null;
				case 2:
				case 3:
					int index = Math.Max(0, LevelData.Objects.IndexOf(obj) + 1);
					
					if (index > (LevelData.Objects.Count-1))
						index--; // just make it point to this object
					
					int xmin = Math.Min(obj.X, LevelData.Objects[index].X);
					int ymin = Math.Min(obj.Y, LevelData.Objects[index].Y);
					int xmax = Math.Max(obj.X, LevelData.Objects[index].X);
					int ymax = Math.Max(obj.Y, LevelData.Objects[index].Y);
					
					BitmapBits bitmap = new BitmapBits(xmax - xmin + 1, ymax - ymin + 1);
					bitmap.DrawLine(6, obj.X - xmin, obj.Y - ymin, LevelData.Objects[index].X - xmin, LevelData.Objects[index].Y - ymin); // LevelData.ColorWhite
					
					return new Sprite(debug[obj.PropertyValue-1], new Sprite(bitmap, xmin - obj.X, ymin - obj.Y));
			}
		}
	}
}