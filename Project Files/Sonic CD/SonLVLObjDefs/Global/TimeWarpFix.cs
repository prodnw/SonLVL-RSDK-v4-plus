using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.Global
{
	class TimeWarpFix : ObjectDefinition
	{
		private Sprite img;
		private PropertySpec[] properties;

		public override void Init(ObjectData data)
		{
			img = new Sprite(LevelData.GetSpriteSheet("Global/Display.gif").GetSection(173, 67, 16, 16), -8, -8);
			
			properties = new PropertySpec[1];
			properties[0] = new PropertySpec("Fix Type", typeof(int), "Extended",
                "The type of \"fix\" this Object will apply.", null, new Dictionary<string, int>
				{
					{ "Erase - Touch", 0 },
					{ "Erase - Pass", 1 },
					{ "Pos Fix - Ground", 2 },
					{ "Pos Fix - Air", 3 }
				},
                (obj) => obj.PropertyValue & 3,
                (obj, value) => obj.PropertyValue = (byte)((int)value));
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); }
		}
		
		public override byte DefaultSubtype
		{
			get { return 2; }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			return (subtype) + "";
		}

		public override Sprite Image
		{
			get { return img; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return img;
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return img;
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			var bitmap = new BitmapBits(1, 1);
			switch (obj.PropertyValue)
			{
				case 0:
					bitmap = new BitmapBits(33, 33);
					bitmap.DrawRectangle(LevelData.ColorWhite, 0, 0, 32, 32);
					return new Sprite(bitmap, -16, -16);
				case 1:
				default:
					return null;
				case 2:
					List<Sprite> sprs = new List<Sprite>();
					
					bitmap = new BitmapBits(97, 97);
					bitmap.DrawRectangle(LevelData.ColorWhite, 0, 0, 96, 96);
					sprs.Add(new Sprite(bitmap, -48, -48));
					
					int index = Math.Max(0, LevelData.Objects.IndexOf(obj) + 1);
					int xdiff = (LevelData.Objects[index].X - obj.X);
					int ydiff = (LevelData.Objects[index].Y - obj.Y);
					
					int xmin = Math.Min(obj.X, LevelData.Objects[index].X);
					int ymin = Math.Min(obj.Y, LevelData.Objects[index].Y);
					int xmax = Math.Max(obj.X, LevelData.Objects[index].X);
					int ymax = Math.Max(obj.Y, LevelData.Objects[index].Y);
					
					bitmap = new BitmapBits(xmax - xmin + 1, ymax - ymin + 1);
					bitmap.DrawLine(LevelData.ColorWhite, obj.X - xmin, obj.Y - ymin, LevelData.Objects[index].X - xmin, LevelData.Objects[index].Y - ymin);
					sprs.Add(new Sprite(bitmap, xmin - obj.X, ymin - obj.Y));
					
					return new Sprite(sprs.ToArray());
					break;
				case 3:
					sprs = new List<Sprite>();
					
					bitmap = new BitmapBits(193, 193);
					bitmap.DrawRectangle(LevelData.ColorWhite, 0, 0, 192, 192);
					sprs.Add(new Sprite(bitmap, -96, -96));
					
					index = Math.Max(0, LevelData.Objects.IndexOf(obj) + 1);
					xdiff = (LevelData.Objects[index].X - obj.X);
					ydiff = (LevelData.Objects[index].Y - obj.Y);
					
					xmin = Math.Min(obj.X, LevelData.Objects[index].X);
					ymin = Math.Min(obj.Y, LevelData.Objects[index].Y);
					xmax = Math.Max(obj.X, LevelData.Objects[index].X);
					ymax = Math.Max(obj.Y, LevelData.Objects[index].Y);
					
					bitmap = new BitmapBits(xmax - xmin + 1, ymax - ymin + 1);
					bitmap.DrawLine(LevelData.ColorWhite, obj.X - xmin, obj.Y - ymin, LevelData.Objects[index].X - xmin, LevelData.Objects[index].Y - ymin);
					sprs.Add(new Sprite(bitmap, xmin - obj.X, ymin - obj.Y));
					
					return new Sprite(sprs.ToArray());
			}
		}
	}
}