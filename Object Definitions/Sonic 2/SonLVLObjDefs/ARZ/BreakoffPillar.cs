using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.ARZ
{
	class BreakoffPillar : ObjectDefinition
	{
		private readonly Sprite[] sprites = new Sprite[3];
		private Sprite debug;

		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("ARZ/Objects.gif");
			sprites[0] = new Sprite(sheet.GetSection(59, 42, 56, 56), -28, -32);
			sprites[1] = new Sprite(sheet.GetSection(140, 80, 32, 8), -16, 24);
			sprites[2] = new Sprite(sheet.GetSection(173, 38, 32, 37), -16, 48);
			
			var bitmap = new BitmapBits(1, 0x40);
			bitmap.DrawLine(LevelData.ColorWhite, 0, 0x00, 0, 0x03);
			bitmap.DrawLine(LevelData.ColorWhite, 0, 0x08, 0, 0x0B);
			bitmap.DrawLine(LevelData.ColorWhite, 0, 0x30, 0, 0x33);
			bitmap.DrawLine(LevelData.ColorWhite, 0, 0x38, 0, 0x3B);
			debug = new Sprite(bitmap, 0, 33);
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); }
		}
		
		public override byte DefaultSubtype
		{
			get { return 0; }
		}

		public override string SubtypeName(byte subtype)
		{
			return subtype + "";
		}

		public override Sprite Image
		{
			get { return new Sprite(sprites); }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return new Sprite(sprites);
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return new Sprite(sprites);
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			if (obj.PropertyValue == 0)
			{
				return debug;
			}
			return new Sprite();
		}
	}
}