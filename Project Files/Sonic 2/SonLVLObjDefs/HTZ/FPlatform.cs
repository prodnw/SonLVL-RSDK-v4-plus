using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.HTZ
{
	class FPlatform : ObjectDefinition
	{
		private Sprite img;
		private Sprite debug;
		private PropertySpec[] properties;
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); }
		}

		public override void Init(ObjectData data)
		{
			img = new Sprite(LevelData.GetSpriteSheet("HTZ/Objects.gif").GetSection(191, 223, 64, 32), -32, -12);
			
			BitmapBits overlay = new BitmapBits(2, 62);
			for (int i = 0; i < 62; i += 12)
				overlay.DrawLine(LevelData.ColorWhite, 0, i, 0, i + 6);
			debug = new Sprite(overlay, 0, 0);
			
			properties = new PropertySpec[1];
			properties[0] = new PropertySpec("Behaviour", typeof(int), "Extended",
				"How this Platform should act upon player contact.", null, new Dictionary<string, int>
				{
					{ "Fall", 0 },
					{ "Static", 1 }
				},
				(obj) => (obj.PropertyValue == 0) ? 0 : 1,
				(obj, value) => obj.PropertyValue = (byte)(int)value);
		}
		
		public override byte DefaultSubtype
		{
			get { return 0; }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
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
			if (obj.PropertyValue == 1) return null;
			return debug;
		}
	}
}
