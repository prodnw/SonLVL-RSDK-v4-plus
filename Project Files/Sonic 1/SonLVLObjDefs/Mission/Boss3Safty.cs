using SonicRetro.SonLVL.API;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace S1ObjectDefinitions.Mission
{
	class Safty : ObjectDefinition
	{
		private Sprite sprite;
		
		public override void Init(ObjectData data)
		{
			sprite = new Sprite(LevelData.GetSpriteSheet("Global/Display.gif").GetSection(1, 143, 32, 32), -16, -16);
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); }
		}
		
		public override string SubtypeName(byte subtype)
		{
			return null;
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
			// Draw a line from this object's position to its falling bounds, and then draw a line across those falling bounds
			// (0x0580 is a value hardcoded in the script)
			int ymin = Math.Min((int)obj.Y, 0x0580);
			int ymax = Math.Max((int)obj.Y, 0x0580);
			BitmapBits bmp = new BitmapBits(257, ymax - ymin + 1);
			
			// tagging this area with LevelData.ColorWhite
			bmp.DrawLine(6, 128, obj.Y - ymin, 128, 0x0580 - ymin);
			bmp.DrawLine(6, 0, 0x0580 - ymin, 256, 0x0580 - ymin);
			
			return new Sprite(bmp, -128, ymin - obj.Y);
		}
	}
}