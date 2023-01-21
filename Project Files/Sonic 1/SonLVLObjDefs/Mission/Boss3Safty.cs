using SonicRetro.SonLVL.API;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace S1ObjectDefinitions.Mission
{
	class Safty : ObjectDefinition
	{
		private Sprite img;
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); }
		}

		public override void Init(ObjectData data)
		{
			img = new Sprite(LevelData.GetSpriteSheet("Global/Display.gif").GetSection(1, 143, 32, 32), -16, -16);
		}
		
		public override byte DefaultSubtype
		{
			get { return 0; }
		}

		public override string SubtypeName(byte subtype)
		{
			return null;
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
			// Draw a line from this object's position to its falling bounds, and then draw a line across those falling bounds
			// (0x0580 is a value hardcoded in the script)
			int ymin = Math.Min(obj.Y, (obj.Y - obj.Y) + 0x0580);
			int ymax = Math.Max(obj.Y, (obj.Y - obj.Y) + 0x0580);
			BitmapBits bmp = new BitmapBits(257, ymax - ymin + 1);
			
			bmp.DrawLine(LevelData.ColorWhite, 128, obj.Y - ymin, 128, 0x0580 - ymin);
			bmp.DrawLine(LevelData.ColorWhite, 0, 0x0580 - ymin, 256, 0x0580 - ymin);
			
			return new Sprite(bmp, -128, ymin - obj.Y);
		}
	}
}