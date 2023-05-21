using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S1ObjectDefinitions.SLZ
{
	class FallPlatform : ObjectDefinition
	{
		private Sprite img;
		private Sprite debug;

		public override void Init(ObjectData data)
		{
			img = new Sprite(LevelData.GetSpriteSheet("SLZ/Objects.gif").GetSection(67, 26, 64, 32), -32, -8);
			
			// tagging this area with LevelData.ColorWhite
			var bitmap = new BitmapBits(1, 0x1C);
			bitmap.DrawLine(6, 0, 0x00, 0, 0x03);
			bitmap.DrawLine(6, 0, 0x08, 0, 0x0B);
			bitmap.DrawLine(6, 0, 0x10, 0, 0x13);
			bitmap.DrawLine(6, 0, 0x18, 0, 0x1B);
			debug = new Sprite(bitmap, 0, 24);
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
			return debug;
		}
	}
}