using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R4
{
	class RPlatform : ObjectDefinition
	{
		private Sprite sprite;
		private Sprite debug;
		
		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("R4/Objects.gif");
			Sprite chain = new Sprite(sheet.GetSection(180, 52, 16, 16), -8, -8);
			
			List<Sprite> sprites = new List<Sprite>();
			
			sprites.Add(new Sprite(sheet.GetSection(163, 52, 16, 16), -8, -8)); // platform post
			List<Sprite> sprs = new List<Sprite>();
			for (int i = 0; i < 5; i++)
				sprs.Add(new Sprite(chain, 0, i * 16));
			sprs.Add(new Sprite(sheet.GetSection(147, 69, 64, 16), -32, -8 + (5 * 16))); // platform
			sprs.Add(new Sprite(sheet.GetSection(130, 35, 32, 16), 0, -24 + (5 * 16)));  // button
			sprite = new Sprite(sprs.ToArray());
			
			BitmapBits overlay = new BitmapBits(161, 161);
			overlay.DrawCircle(6, 80, 80, 80); // LevelData.ColorWhite
			debug = new Sprite(overlay, -80, -80);
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {}); }
		}
		
		public override string SubtypeName(byte subtype)
		{
			return "";
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
			return debug;
		}
	}
}